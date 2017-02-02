﻿using System;
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
        private double _maxX;
        private double _maxY;
        private double _minX;
        private double _minY;

        private int _numJ;//1 si pion du joueur 1, 2 si pion du joueur 2
        private Vector2 _posInitiale;
        private ObjetPuissance4 _pion;

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

        public Pion(Game game, double maxX, double maxY, double minX, double minY) : base(game)
        {
            _numJ = 1;
            _maxX = maxX;
            _minX = minX;
            _maxY = maxY;
            _minY = minY;

            this.Game.Components.Add(this);
        }

        public override void Initialize()
        {
            _posInitiale.X = (float)_maxX / 2;
            _posInitiale.Y = (float)_maxY / 2;

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

            base.Update(gameTime);
        }
    }
}
