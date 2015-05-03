using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectRPG
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Core : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera camera;
        SpriteFont fontArial;
        World world;
        Player player;
        Cursor cursor;
        Vector2 lastViewportPosition;
        

        public Core()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Настройка разрешения, вертикальной синхронизации и тп

            if (Options.WindowMode == 0) graphics.IsFullScreen = true;
            else if (Options.WindowMode == 1)
            {
                graphics.HardwareModeSwitch = false;
                graphics.IsFullScreen = true;
            }
            else graphics.IsFullScreen = false;

            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1920;

            IsMouseVisible = Options.HardwareMouse;
            if (Options.VerticalSynchronize == false)
            {
                graphics.SynchronizeWithVerticalRetrace = false;
                IsFixedTimeStep = false;
            }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            world = new World();

            player = new Player();            

            camera = new Camera(GraphicsDevice.Viewport, player);

            cursor = new Cursor(GraphicsDevice.Viewport, camera);
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            player.Load(Content);

            world.Load(Content);

            cursor.Load(Content);

            fontArial = Content.Load<SpriteFont>(@"Fonts\Arial");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>

        protected override void Update(GameTime gameTime)
        {
            switch (cursor.mouseWheel)
            {
                case 0:
                    break;
                case 1:
                    camera.Zoom += 0.1f;
                    break;
                case -1:
                    camera.Zoom -= 0.1f;
                    break;
                default:
                    break;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.T))
                camera.Zoom += 0.01f;
            else if (Keyboard.GetState().IsKeyDown(Keys.G))
                camera.Zoom -= 0.01f;
            if (Keyboard.GetState().IsKeyDown(Keys.F))
                camera.Rotation += 0.01f;
            else if (Keyboard.GetState().IsKeyDown(Keys.H))
                camera.Rotation -= 0.01f;


            


            player.Update();

            world.Update();

            player.Rotation = (float)Math.Atan2(cursor.Position.Y - GraphicsDevice.Viewport.Height / 2, cursor.Position.X - GraphicsDevice.Viewport.Width / 2);

            cursor.Update();

            if (Options.CursorViewportLock)
            {
                lastViewportPosition = cursor.Position;

                if (cursor.Position.X > GraphicsDevice.Viewport.Width * 0.8f) lastViewportPosition.X = GraphicsDevice.Viewport.Width * 0.8f;
                else if (cursor.Position.X < GraphicsDevice.Viewport.Width * 0.2f) lastViewportPosition.X = GraphicsDevice.Viewport.Width * 0.2f;
                if (cursor.Position.Y > GraphicsDevice.Viewport.Height * 0.8f) lastViewportPosition.Y = GraphicsDevice.Viewport.Height * 0.8f;
                else if (cursor.Position.Y < GraphicsDevice.Viewport.Height * 0.2f) lastViewportPosition.Y = GraphicsDevice.Viewport.Height * 0.2f;

                camera.Update(lastViewportPosition, player.Position);
            }
            else camera.Update(player.Position);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {


            GraphicsDevice.Clear(Color.CornflowerBlue);
            

            

            spriteBatch.Begin(SpriteSortMode.BackToFront,
                BlendState.AlphaBlend,
                SamplerState.PointWrap,
                null, null, null,
                camera.Transform);

            player.Draw(spriteBatch);
            world.Draw(spriteBatch);

            spriteBatch.End();


            //////////////////////////////// Debug информация /////////////////////////////////

            if (Options.Debug)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(fontArial, "Mouse position: X = " + Mouse.GetState().X + ", Y = " + Mouse.GetState().Y, new Vector2(50, 50), Color.White);
                spriteBatch.DrawString(fontArial,
                    "Viewport: Width = " + GraphicsDevice.Viewport.Width +
                    ", Height = " + GraphicsDevice.Viewport.Height +
                    ", CenterX = " + GraphicsDevice.Viewport.Width / 2 +
                    ", CenterY = " + GraphicsDevice.Viewport.Height / 2
                , new Vector2(50, 80), Color.White);
                spriteBatch.DrawString(fontArial, "Player Position: X = " + player.Position.X + ", Y = " + player.Position.Y, new Vector2(50, 110), Color.White);
                spriteBatch.DrawString(fontArial, "Mouse global X = " + cursor.PositionGlobal.X + ", Y = " + cursor.PositionGlobal.Y, new Vector2(50, 140), Color.White);

                spriteBatch.DrawString(fontArial, "Player rotation = " + player.Rotation, new Vector2(50, 170), Color.White);
                spriteBatch.DrawString(fontArial, "Matrix = " + camera.Transform.M11 + ", " + camera.Transform.M12 + ", " + camera.Transform.M13 + ", " + camera.Transform.M14, new Vector2(50, 200), Color.White);
                spriteBatch.DrawString(fontArial, "Matrix = " + camera.Transform.M21 + ", " + camera.Transform.M22 + ", " + camera.Transform.M23 + ", " + camera.Transform.M24, new Vector2(50, 230), Color.White);
                spriteBatch.DrawString(fontArial, "Matrix = " + camera.Transform.M31 + ", " + camera.Transform.M32 + ", " + camera.Transform.M33 + ", " + camera.Transform.M34, new Vector2(50, 260), Color.White);
                spriteBatch.DrawString(fontArial, "Matrix = " + camera.Transform.M41 + ", " + camera.Transform.M42 + ", " + camera.Transform.M43 + ", " + camera.Transform.M44, new Vector2(50, 290), Color.White);
                //spriteBatch.DrawString(fontArial, "Mouse inverse X = " + mousePos.X + ", Y = " + mousePos.Y, new Vector2(50, 320), Color.White);
                spriteBatch.DrawString(fontArial, "X = " + (cursor.Position.X - GraphicsDevice.Viewport.Width / 2) + ", Y = " + (cursor.Position.Y - GraphicsDevice.Viewport.Height / 2), new Vector2(50, 320), Color.White);
                //spriteBatch.DrawString(fontArial, "Rotation = " + Rotation, new Vector2(50, 350), Color.White);
                //spriteBatch.DrawString(fontArial, " = " + camera.Rotation, new Vector2(50, 350), Color.White);
                spriteBatch.End();
            }
            
            ////////////////////////////////////////////////////////////////////////////////////
            
            
            // Рисуем курсор
            spriteBatch.Begin();
            cursor.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
