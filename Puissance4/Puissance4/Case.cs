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
        SpriteBatch _spriteBatch;

        private ObjetPuissance4 _case;
        private Vector2 _posInitiale;
        private Pion _pion;

        public Pion pion
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
        
        public ObjetPuissance4 case1
        {
            get
            {
                return _case;
            }
            set
            {
                _case = case1;
            }
        }

        public Case(Game game,double maxX,double maxY) : base(game)
        {
            _pion = null;

            _posInitiale.X = (float)maxX / 15;
            _posInitiale.Y = (float)maxY / 2;

            this.Game.Components.Add(this);
        }

        public Case(Game game, double maxX, double maxY, int numJ) : this(game,maxX,maxY)
        {
            _pion = new Pion(game, maxX, maxY, numJ, _posInitiale.X, _posInitiale.Y);
        }

        public override void Initialize()
        {      
            base.Initialize();
        }

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

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_case.Texture, _case.Position, Color.Azure);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }


    }
}
