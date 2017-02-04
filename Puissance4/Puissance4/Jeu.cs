using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Threading;

namespace Puissance4
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Jeu : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState oldKey;
        private Pion p;
        private Grille g;
        private int colonne;

        public Jeu()
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
            int maxX, maxY;
            // TODO: Add your initialization logic here
            //this.graphics.IsFullScreen = false;
            this.graphics.PreferredBackBufferWidth = 1000;
            this.graphics.PreferredBackBufferHeight = 700;
            this.graphics.ApplyChanges();

            this.Window.AllowUserResizing = true;

            spriteBatch = new SpriteBatch(GraphicsDevice);
            // on définit les coordonnées de l'éran de sortie
            maxX = this.GraphicsDevice.Viewport.Width;
            maxY = this.GraphicsDevice.Viewport.Height;


            colonne = 3; // placement par défaut
            p = new Pion(this, (maxX - (1 * 100)) / 2, (maxY-(6*100)-100)/2);
            g = new Grille(this, maxX, maxY);

            


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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            if (ActionClavier() != 0)
                g.placerPion(this, 1, ActionClavier());

            


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private int ActionClavier()
        {
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Right))
            {
                //on vérifie s’il est possible de se déplacer
                if((!oldKey.IsKeyDown(Keys.Right)) && (colonne != 6))
                {
                    float posPX = p.pion.Position.X;
                    posPX += 100;
                    p.pion.Position = new Vector2(posPX, p.pion.Position.Y);
                    colonne ++;
                }
            }
            else if (keyboard.IsKeyDown(Keys.Left))
            {
                //on vérifie s’il est possible de se déplacer
                if((!oldKey.IsKeyDown(Keys.Left)) && (colonne != 0))
                {
                    float posPX = p.pion.Position.X;
                    posPX -= 100;
                    p.pion.Position = new Vector2(posPX, p.pion.Position.Y);
                    colonne --;
                }
            }
            else if (keyboard.IsKeyDown(Keys.Enter))
            {
                //on vérifie s’il est possible de se déplacer
                if(!oldKey.IsKeyDown(Keys.Enter))
                {
                    oldKey = keyboard;
                    return colonne;
                }
            }

            
            oldKey = keyboard;
            return 0;
        }
    }
}
