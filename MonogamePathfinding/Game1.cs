using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.InputListeners;
using MonogamePathfinding.AI.Pathfinding;
using MonogamePathfinding.AI.Pathfinding.Grid;
using MonogamePathfinding.AI.Pathfinding.Heuristics;
using System;
using System.Diagnostics;

namespace MonogamePathfinding
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Random random = new Random();
        InputListenerManager _inputManager;
        Texture2D blankTexture;

        public const int GRID_CELL_WIDTH = 10;
        public const int GRID_CELL_HEIGHT = 10;
        public const int GRID_WIDTH = 80;
        public const int GRID_HEIGHT = 80;
        public const int RANDOM_MAZE_VALUE = 43;

        public IPathfindingEngine PathfindingEngine;
        public IPathfindingGrid Grid;
        public IGridNode StartGridCell;
        public IGridNode EndGridCell;
        public IPathfindingNode Path;

        #region Initialization
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();

            _inputManager = new InputListenerManager();
            var keyboardListener = _inputManager.AddListener(new KeyboardListenerSettings());
            keyboardListener.KeyTyped += KeyboardListener_KeyTyped;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            blankTexture = new Texture2D(GraphicsDevice, 1, 1);
            blankTexture.SetData(new[] { Color.White });
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        #endregion

        #region Update Draw
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Grid == null) ResetGrid();

            _inputManager.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            var entireGrid = Grid.GetEntireGrid();
            spriteBatch.Begin();

            foreach (var cell in entireGrid)
            {
                Color colour = Color.White;
                if (cell.Navigatable == TraversalSettings.Unpassable)
                    colour = Color.Black;

                Rectangle position = new Rectangle(cell.Position.X * GRID_CELL_WIDTH,
                                                   cell.Position.Y * GRID_CELL_HEIGHT,
                                                   GRID_CELL_WIDTH,
                                                   GRID_CELL_HEIGHT);

                spriteBatch.Draw(blankTexture, position, colour);
            }

            if (Path != null)
            {
                IPathfindingNode currentNode = Path;
                while (currentNode != null)
                {
                    spriteBatch.Draw(blankTexture,
                                     new Rectangle(currentNode.GridNode.Position.X * GRID_CELL_WIDTH,
                                                   currentNode.GridNode.Position.Y * GRID_CELL_HEIGHT,
                                                   GRID_CELL_WIDTH,
                                                   GRID_CELL_HEIGHT),
                                     Color.Yellow);

                    currentNode = currentNode.Parent;
                }

                spriteBatch.Draw(blankTexture,
                                 new Rectangle(StartGridCell.Position.X * GRID_CELL_WIDTH,
                                               StartGridCell.Position.Y * GRID_CELL_HEIGHT,
                                               GRID_CELL_WIDTH,
                                               GRID_CELL_HEIGHT),
                                 Color.Green);
                spriteBatch.Draw(blankTexture,
                                 new Rectangle(EndGridCell.Position.X * GRID_CELL_WIDTH,
                                               EndGridCell.Position.Y * GRID_CELL_HEIGHT,
                                               GRID_CELL_WIDTH,
                                               GRID_CELL_HEIGHT),
                                 Color.Red);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion

        private void ResetGrid()
        {
            Debug.WriteLine("Resetting Grid");
            Grid = new PathfindingGrid(GRID_WIDTH, GRID_HEIGHT);
            var entireGrid = Grid.GetEntireGrid();
            foreach (var gridCell in entireGrid)
            {
                if (gridCell.Position.X == 0 || gridCell.Position.Y == 0 ||
                    gridCell.Position.X == (Grid.Width - 1) ||
                    gridCell.Position.Y == (Grid.Height - 1))
                {
                    gridCell.Navigatable = TraversalSettings.Unpassable;
                }
                else
                {
                    int i = random.Next(0, 50);
                    if (i < RANDOM_MAZE_VALUE)
                        gridCell.Navigatable = TraversalSettings.Passable;
                    else
                        gridCell.Navigatable = TraversalSettings.Unpassable;
                }
            }

            RandomizeStartEndPositions();
        }

        private void RandomizeStartEndPositions()
        {
            Debug.WriteLine("Randomize Start End Positions");
            var entireGrid = Grid.GetEntireGrid();
            if (entireGrid.Count == 0) return;

            //Debug.WriteLine("While(true): Randomizing Start Position");
            while (true)
            {
                var gridCell = entireGrid[random.Next(entireGrid.Count)];
                if (gridCell?.Navigatable == TraversalSettings.Passable)
                {
                    StartGridCell = gridCell;
                    break;
                }
            }
            //Debug.WriteLine("End While(true)");

            //Debug.WriteLine("While(true): Randomizing End Position");
            while (true)
            {
                var gridCell = entireGrid[random.Next(entireGrid.Count)];
                if (gridCell?.Navigatable == TraversalSettings.Passable)
                {
                    EndGridCell = gridCell;
                    break;
                }
            }
            //Debug.WriteLine("End While(true)");

            Pathfind();
        }

        private void Pathfind()
        {
            Debug.WriteLine($"Begin Pathfind");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            PathfindingEngine = new AStarPathfindingEngine(10, Grid, new ManhattonDistance());
            Path = PathfindingEngine.FindPath(StartGridCell.Position, EndGridCell.Position);

            stopwatch.Stop();
            Debug.WriteLine($"Total Time: {stopwatch.ElapsedMilliseconds}ms | {stopwatch.Elapsed.TotalSeconds}s");
        }


        private void KeyboardListener_KeyTyped(object sender, KeyboardEventArgs e)
        {
            if (e.Key == Keys.Escape)
            {
                Debug.WriteLine("Exit");
                base.Exit();
            }

            if (e.Key == Keys.R)
            {
                ResetGrid();
            }

            if (e.Key == Keys.Space)
            {
                RandomizeStartEndPositions();
            }
        }
    }
}
