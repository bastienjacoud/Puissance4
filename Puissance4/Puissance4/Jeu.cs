using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Puissance4
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Jeu : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteFont _font;
        private String _texte;
        private String _msgGagnant;
        private Menu _menu;
        private bool _premierCoup;
        private bool _reinitialise;//vaut 1 ssi une partie vient d'être relancée.

        KeyboardState oldKey;

        private Pion p1;
        private Pion p2;
        private Grille g;

        private int colonne;//colonne choisie par le joueur pour placer le pion(de 0 à 6)
        private int _joueurActuel;

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

            this.Window.AllowUserResizing = false;

            // on définit les coordonnées de l'éran de sortie
            maxX = this.GraphicsDevice.Viewport.Width;
            maxY = this.GraphicsDevice.Viewport.Height;

            _reinitialise = false;
            _premierCoup = true;
            colonne = 3; // placement par défaut
            _joueurActuel = 1;
            _texte = "C'est au Joueur " + _joueurActuel + " de jouer.";
            _msgGagnant = "Appuyez sur Entrée pour revenir au menu.";

            
            p1 = new Pion(this, (maxX - (1 * 100)) / 2, (maxY-(6*100)-100)/2, (maxY - (6 * 100) - 100) / 2, 1);
            p2 = new Pion(this, (maxX - (1 * 100)) / 2, (maxY - (6 * 100) - 100) / 2, (maxY - (6 * 100) - 100) / 2, 2);

            g = new Grille(this, maxX, maxY);

            _menu = new Menu(this, maxX, maxY);
            base.Initialize();
        }

        private void ReInitialise(int maxX, int maxY)
        {
            _premierCoup = true;
            colonne = 3; // placement par défaut
            _joueurActuel = 1;
            _texte = "C'est au Joueur " + _joueurActuel + " de jouer.";
            _msgGagnant = "Appuyez sur Entrée pour revenir au menu.";


            p1 = new Pion(this, (maxX - (1 * 100)) / 2, (maxY - (6 * 100) - 100) / 2, (maxY - (6 * 100) - 100) / 2, 1);
            p2 = new Pion(this, (maxX - (1 * 100)) / 2, (maxY - (6 * 100) - 100) / 2, (maxY - (6 * 100) - 100) / 2, 2);

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
            _font = Content.Load<SpriteFont>("police");
            _menu.LoadContent(this.Content);

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
            if (_menu.jeuActif)
            {
                if(_reinitialise)
                {
                    int maxX = this.GraphicsDevice.Viewport.Width;
                    int maxY = this.GraphicsDevice.Viewport.Height;
                    this.ReInitialise(maxX, maxY);
                    _reinitialise = false;
                }
                int b = ActionClavier();
                if (b >= 0 && b <= 6)
                {
                    double maxY = this.GraphicsDevice.Viewport.Height;
                    ChangementJoueur(g.placerPion(this, _joueurActuel, b, (maxY - (6 * 100) - 100) / 2));
                }
                int gagnant = g.verifGrille();
                if (gagnant != 0)
                {
                    if (_joueurActuel != 0)
                        _texte = "Le joueur " + gagnant + " a gagné!";
                    _joueurActuel = 0;
                    _menu.retourMenu = true;
                }
            }
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
            
            if(_menu.quitterActif)
            {
                this.Exit();
            }
            else if(!_menu.jeuActif)
            {
                _menu.Draw(gameTime);
            }
            else
            {
                spriteBatch.Begin();
                if(_joueurActuel == 0)
                {
                    Vector2 texteSize = _font.MeasureString(_texte);
                    float maxX = this.GraphicsDevice.Viewport.Width;
                    float maxY = this.GraphicsDevice.Viewport.Height;
                    spriteBatch.DrawString(_font, _texte, new Vector2((maxX - texteSize.X) / 2, 0), Color.Black);
                    Vector2 msgSize = _font.MeasureString(_msgGagnant);
                    spriteBatch.DrawString(_font, _msgGagnant, new Vector2( (maxX - msgSize.X) / 2, texteSize.Y + maxY/14 ), Color.Black);
                }
                else
                {
                    spriteBatch.DrawString(_font, _texte, new Vector2(0, 0), Color.Black);
                }
                    

                g.Draw(gameTime);

                if (_joueurActuel == 1)
                    p1.Draw(gameTime);
                else if (_joueurActuel == 2)
                    p2.Draw(gameTime);
                spriteBatch.End();
            }       
        }

        private void ChangementJoueur(bool b)
        {
            if(b)// Si le changement de joueur a bien lieu.
            {
                if (_joueurActuel == 1)
                {
                    float maxX = this.GraphicsDevice.Viewport.Width;
                    p1.pion.Position = new Vector2((maxX - (1 * 100)) / 2, p1.pion.Position.Y);
                    _joueurActuel = 2;
                }
                    

                else if (_joueurActuel == 2)
                {
                    float maxX = this.GraphicsDevice.Viewport.Width;
                    p2.pion.Position = new Vector2((maxX - (1 * 100)) / 2, p2.pion.Position.Y);
                    _joueurActuel = 1;
                }

                _texte = "C'est au Joueur " + _joueurActuel + " de jouer.";

                colonne = 3;
            }        
        }

        private int ActionClavier()
        {
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Right))
            {
                //on vérifie s’il est possible de se déplacer
                if((!oldKey.IsKeyDown(Keys.Right)) && (colonne != 6))
                {
                    if (_joueurActuel == 1)
                    {
                        float posPX = p1.pion.Position.X;
                        posPX += 100;
                        p1.pion.Position = new Vector2(posPX, p1.pion.Position.Y);
                        
                    }
                    else if (_joueurActuel == 2)
                    {
                        float posPX = p2.pion.Position.X;
                        posPX += 100;
                        p2.pion.Position = new Vector2(posPX, p2.pion.Position.Y);
                    }
                    colonne ++;
                }
            }
            else if (keyboard.IsKeyDown(Keys.Left))
            {
                //on vérifie s’il est possible de se déplacer
                if((!oldKey.IsKeyDown(Keys.Left)) && (colonne != 0))
                {
                    if(_joueurActuel == 1)
                    {
                        float posPX = p1.pion.Position.X;
                        posPX -= 100;
                        p1.pion.Position = new Vector2(posPX, p1.pion.Position.Y);
                    }
                    else if(_joueurActuel == 2)
                    {
                        float posPX = p2.pion.Position.X;
                        posPX -= 100;
                        p2.pion.Position = new Vector2(posPX, p2.pion.Position.Y);
                    }
                    
                    colonne --;
                }
            }
            else if (keyboard.IsKeyDown(Keys.Enter))
            {
                //on vérifie s’il est possible de se déplacer
                if(!oldKey.IsKeyDown(Keys.Enter) && !_premierCoup)
                {
                    if(_joueurActuel == 0)
                    {
                        _reinitialise = true;
                        _menu.jeuActif = false;
                    }
                    else
                    {
                        oldKey = keyboard;
                        return colonne;
                    }      
                }
                _premierCoup = false;
            }

            
            oldKey = keyboard;
            return -1;
        }
    }
}
