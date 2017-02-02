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
        SpriteBatch _spriteBatch;

        private int _numJ;//0 si appartient à aucun joueur(ie pas de pion dans la case), 1 si pion du joueur 1, 2 si pion du joueur 2
        private Vector2 _posInitiale;//Position du pion avant déplacement
        private ObjetPuissance4 _pion;//position du pion à n'importe quel moment + taille + texture

        public int numJ
        {
            get
            {
                return _numJ;
            }
            set
            {
                _numJ = numJ;
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
                _posInitiale = posInitiale;
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
                _pion = pion;
            }
        }

        public Pion(Game game, double maxX, double maxY) : base(game)
        {
            _numJ = 1;
            //Position par défaut
            _posInitiale.X = (float)maxX / 15;
            _posInitiale.Y = (float)maxY / 20;

            this.Game.Components.Add(this);
        }

        public Pion(Game game, double maxX, double maxY, int numJ, double posX, double posY) : this(game, maxX, maxY)
        {
            _numJ = numJ;
            //Position initiale du pion
            _posInitiale.X = (float)posX;
            _posInitiale.Y = (float)posY;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Vector2 taille;
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            if(_numJ == 1)
            {
                _pion = new ObjetPuissance4(Game.Content.Load<Texture2D>(@"images\jaune"),
                _posInitiale, Vector2.Zero);
            }
            else if(_numJ == 2)
            {
                _pion = new ObjetPuissance4(Game.Content.Load<Texture2D>(@"images\rouge"),
                _posInitiale, Vector2.Zero);
            }

            taille.X = _pion.Texture.Width;
            taille.Y = _pion.Texture.Height;
            _pion.Size = taille;

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_pion.Texture, _pion.Position, Color.Azure);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            /*
            float posY = _pion.Position.Y;
            posY--;
            _pion.Position = new Vector2(_pion.Position.X, posY);
            */
            base.Update(gameTime);
        }
    }
}
