using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Puissance4
{
    public class Case : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch _spriteBatch;//Spritebatch utilisée pour l'affichage d'une case

        private ObjetPuissance4 _case;//objet puissance 4 contenant la texture, la position et la taille de la case.
        private Vector2 _posInitiale;//position initiale de la case
        private Pion _pion;//pion contenu dans la case

        //properties
        public Pion pion
        {
            get
            {
                return _pion;
            }
            set
            {
                _pion = value;
            }
        }

        public Vector2 posInitiale
        {
            get
            {
                return _posInitiale;
            }
            set
            {
                _posInitiale = value;
            }
        }
        
        public ObjetPuissance4 case1
        {
            get
            {
                return _case;
            }
            set
            {
                _case = value;
            }
        }

        //constructeur de base de la case(sans le pion)
        public Case(Game game,double posX,double posY) : base(game)
        {
            _pion = null;//Par défaut(lors de la construction de la grille vide) le pion est à null

            //Position initiale de la case
            _posInitiale.X = (float)posX;
            _posInitiale.Y = (float)posY;

            this.Game.Components.Add(this);
        }

        //constructeur surchargé de la case(avec le pion)
        public Case(Game game, double posX, double posY, double posPY, int numJ) : this(game,posX,posY)
        {
            //Construction du pion dans la case
            _pion = new Pion(game, posX, posY, posPY, numJ);
        }

        //fonction d'initialisation 
        public override void Initialize()
        {      
            base.Initialize();
        }

        //charge le contenu de l'objet puissance 4 case
        protected override void LoadContent()
        {
            Vector2 taille;
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _case = new ObjetPuissance4(Game.Content.Load<Texture2D>(@"images\cadre"),
                _posInitiale, Vector2.Zero);

            taille.X = _case.Texture.Width;
            taille.Y = _case.Texture.Height;
            _case.Size = taille;

            base.LoadContent();
        }

        //affiche une case à l'écran
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_case.Texture, _case.Position, Color.Azure);
            _spriteBatch.End();
            
            //on affiche le pion uniquement s'il est défini
            if(_pion != null)
                _pion.Draw(gameTime);

            base.Draw(gameTime);
        }

        //mise à jour de l'affichage
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }


    }
}
