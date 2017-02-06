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
        private SpriteBatch spriteBatch;//spritebatch utilisé pour l'affichage
        private SpriteFont _font;//police utilisée pour le texte
        private String _texte;//texte affiché durant le jeu
        private String _msgGagnant;//texte affiché en plus s'il y a un gagnant
        private Menu _menu;//menu utilisé dans le jeu
        private bool _premierCoup;//booléen vérifiant s'il s'agit du premier coup joué
                                  //utilisé pour faire la transition menu/jeu sans que la balle ne se lance automatiquement
        private bool _reinitialise;//vaut 1 ssi une partie vient d'être relancée.
                                   //Pour éviter de réinitialiser le jeu en boucle

        KeyboardState oldKey;//Ancien keyboardState contenant la dernière action de l'utilisateur(dernière touche appuyée)

        private Pion p1;//pion jaune servant au choix de la colonne du joueur 1 
        private Pion p2;//pion rouge servant au choix de la colonne du joueur 2
        private Grille g;//grille utilisée

        private int colonne;//colonne choisie par le joueur pour placer le pion(de 0 à 6)
        private int _joueurActuel;//joueur qui est en train de jouer

        //constructeur du jeu
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
            this.graphics.PreferredBackBufferWidth = 1000;
            this.graphics.PreferredBackBufferHeight = 700;
            this.graphics.ApplyChanges();

            //empeche l'utilisateur de redimentionner la fenêtre
            this.Window.AllowUserResizing = false;

            // on définit les coordonnées de l'éran de sortie
            maxX = this.GraphicsDevice.Viewport.Width;
            maxY = this.GraphicsDevice.Viewport.Height;

            //initialise les attributs du jeu
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

        //fonction de réinitialisation si l'utilisateur relance une autre partie après la première
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
            //Si l'on a choisi de jouer dans le menu
            if (_menu.jeuActif)
            {
                if(_reinitialise)//si le jeu n'a pas été réinitialisé
                {
                    int maxX = this.GraphicsDevice.Viewport.Width;
                    int maxY = this.GraphicsDevice.Viewport.Height;
                    this.ReInitialise(maxX, maxY);
                    _reinitialise = false;//on passe le booléen à false pour éviter de boucler
                }
                int b = ActionClavier();//actions de l'utilisateur
                if (b >= 0 && b <= 6)//vérification de validité de la colonne choisie
                {
                    double maxY = this.GraphicsDevice.Viewport.Height;
                    ChangementJoueur(g.placerPion(this, _joueurActuel, b, (maxY - (6 * 100) - 100) / 2));//on place le pion et change de joueur
                }
                int gagnant = g.verifGrille();//vérification qu'on n'a pas de gagnant
                if (gagnant != 0)//si on a un gagnant
                {
                    if (_joueurActuel != 0)
                        _texte = "Le joueur " + gagnant + " a gagné!";//on affiche un message indiquant le gagnant
                    _joueurActuel = 0;
                    _menu.retourMenu = true;//on retourne au menu
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
            
            if(_menu.quitterActif)//si l'on choisit de quitter via le menu
            {
                this.Exit();
            }
            else if(!_menu.jeuActif)//si l'on est dans le menu et pas dans le jeu
            {
                _menu.Draw(gameTime);//on affiche le menu
            }
            else//si l'on est dans le jeu
            {
                spriteBatch.Begin();
                if(_joueurActuel == 0)// si l'on a un gagnant, on affiche un message de redirection vers le menu
                {
                    Vector2 texteSize = _font.MeasureString(_texte);
                    float maxX = this.GraphicsDevice.Viewport.Width;
                    float maxY = this.GraphicsDevice.Viewport.Height;
                    spriteBatch.DrawString(_font, _texte, new Vector2((maxX - texteSize.X) / 2, 0), Color.Black);
                    Vector2 msgSize = _font.MeasureString(_msgGagnant);
                    spriteBatch.DrawString(_font, _msgGagnant, new Vector2( (maxX - msgSize.X) / 2, texteSize.Y + maxY/14 ), Color.Black);
                }
                else// sinon, on affiche un message indiquant qui joue
                {
                    spriteBatch.DrawString(_font, _texte, new Vector2(0, 0), Color.Black);
                }
                    
                //affichage de la grille
                g.Draw(gameTime);

                //affiche du pion au dessus de la grille correspondant au joueur en cours de jeu
                if (_joueurActuel == 1)
                    p1.Draw(gameTime);
                else if (_joueurActuel == 2)
                    p2.Draw(gameTime);
                spriteBatch.End();
            }       
        }

        //fonction pour changer de joueur
        private void ChangementJoueur(bool b)
        {
            if(b)// Si le changement de joueur a bien lieu.
            {
                if (_joueurActuel == 1)// si le joueur 1 vient de jouer
                {
                    float maxX = this.GraphicsDevice.Viewport.Width;
                    p1.pion.Position = new Vector2((maxX - (1 * 100)) / 2, p1.pion.Position.Y);//on réinitialise la position initiale du pion
                    _joueurActuel = 2;//le joueur qui joue est donc le joueur 2
                }
                    

                else if (_joueurActuel == 2)// si le joueur 2 vient de jouer
                {
                    float maxX = this.GraphicsDevice.Viewport.Width;
                    p2.pion.Position = new Vector2((maxX - (1 * 100)) / 2, p2.pion.Position.Y);
                    _joueurActuel = 1;
                }

                _texte = "C'est au Joueur " + _joueurActuel + " de jouer.";// on change le message affiché

                colonne = 3;//réinitialisation de la colonne de base
            }        
        }

        //fonction qui gère les actions de l'utilisateur
        private int ActionClavier()
        {
            KeyboardState keyboard = Keyboard.GetState();//keyboardState actuel
            if (keyboard.IsKeyDown(Keys.Right))//si la touche droite est appuyée
            {
                //on vérifie s’il est possible de se déplacer et si elle ne vient pas d'être appuyée(pour se déplacer uniquement d'une case)
                if((!oldKey.IsKeyDown(Keys.Right)) && (colonne != 6))
                {
                    //on bouge le pion correspondant
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
            else if (keyboard.IsKeyDown(Keys.Left))//idem pour la gauche
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
            else if (keyboard.IsKeyDown(Keys.Enter))//idem pour entrer(lacher un pion)
            {
                //on vérifie s’il est possible de se déplacer et si ce n'est pas le premier coup joué(transition depuis le menu)
                if(!oldKey.IsKeyDown(Keys.Enter) && !_premierCoup)
                {
                    if(_joueurActuel == 0)//si fin de partie (entrée sert aussi de retour au menu)
                    {
                        _reinitialise = true;
                        _menu.jeuActif = false;
                    }
                    else//sinon on retourne la colonne choisie
                    {
                        oldKey = keyboard;
                        return colonne;
                    }      
                }
                _premierCoup = false;
            }

            
            oldKey = keyboard;//le choix rélisé va donc devenir l'ancien choix 
            return -1;// retourne -1 si aucune action
        }
    }
}
