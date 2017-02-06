using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Puissance4
{
    public class Pion : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch _spriteBatch;//Spritebatch uilisée dans l'affichage du pion.

        private int _numJ;//0 si appartient à aucun joueur(ie pas de pion dans la case), 1 si pion du joueur 1, 2 si pion du joueur 2
        private Vector2 _posInitiale;//Position du pion au niveau de la case
        private double _posYDep; //Position verticale du pion pendant l'animation de chute
        private ObjetPuissance4 _pion;//position du pion lors de l'affichage

        //properties
        public int numJ
        {
            get
            {
                return _numJ;
            }
            set
            {
                _numJ = value;
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

        public double posYDep
        {
            get
            {
                return _posYDep;
            }
            set
            {
                _posYDep = value;
            }
        }

        public ObjetPuissance4 pion
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

        //constructeur du pion de base.
        public Pion(Game game, double posX, double posY) : base(game)
        {
            _numJ = 1;//Par défaut, le pion appartient au joueur 1
            //Position par défaut
            _posInitiale.X = (float)posX;
            _posInitiale.Y = (float)posY;

            this.Game.Components.Add(this);
        }

        //Constructeur surchargé.
        public Pion(Game game, double posX, double posY,double posYDep, int numJ) : base(game)
        {
            _numJ = numJ;
            //Position initiale du pion
            _posInitiale.X = (float)posX;
            _posInitiale.Y = (float)posY;
            _posYDep = posYDep;

            this.Game.Components.Add(this);
        }

        //fonction xna d'initialisation
        public override void Initialize()
        {
            base.Initialize();
        }

        //Charge le spritebatch et la texture, la taille et la position du pion.
        protected override void LoadContent()
        {
                Vector2 taille;
                _spriteBatch = new SpriteBatch(GraphicsDevice);
                if (_numJ == 1)
                {
                    _pion = new ObjetPuissance4(Game.Content.Load<Texture2D>(@"images\jaune"),
                    new Vector2(_posInitiale.X,(float)_posYDep), Vector2.Zero);
                }
                else if (_numJ == 2)
                {
                    _pion = new ObjetPuissance4(Game.Content.Load<Texture2D>(@"images\rouge"),
                    new Vector2(_posInitiale.X, (float)_posYDep), Vector2.Zero);
                }
                taille.X = _pion.Texture.Width;
                taille.Y = _pion.Texture.Height;
                _pion.Size = taille;
            
            

            base.LoadContent();
        }

        //affiche un pion à l'écran
        public override void Draw(GameTime gameTime)
        {
                _spriteBatch.Begin();
                _spriteBatch.Draw(_pion.Texture, _pion.Position, Color.Azure);
                _spriteBatch.End();
            
            base.Draw(gameTime);
        }

        //rafraichit l'affichage, et actualise les changements
        public override void Update(GameTime gameTime)
        {

            diminuePosVerticale();

            base.Update(gameTime);
        }

        //diminue la position verticale du pion (lors de l'animation de chute)
        private void diminuePosVerticale()
        {
            if (_posYDep < _posInitiale.Y)
            {
                _posYDep+=5;
                _pion.Position = new Vector2(_pion.Position.X, (float)_posYDep);
            }              
        }
    }
}
