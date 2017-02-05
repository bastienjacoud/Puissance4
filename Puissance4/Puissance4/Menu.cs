using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Puissance4
{
    class Menu : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch _spriteBatch;
        private SpriteFont _font;

        private bool _actif;//permet de savoir si le menu est actif ou si l'on est dans le jeu
        private double _maxX;
        private double _maxY;

        private String _titre;
        private String _presentation1;
        private String _presentation2;
        private String _jouer;
        private String _touches;
        private String _quitter;

        public bool actif
        {
            get
            {
                return _actif;
            }
            set
            {
                _actif = value;
            }
        }

        public double maxX
        {
            get
            {
                return _maxX;
            }
            set
            {
                _maxX = value;
            }
        }

        public double maxY
        {
            get
            {
                return _maxY;
            }
            set
            {
                _maxY = value;
            }
        }

        public Menu(Game game, double maxX, double maxY) : base(game)
        {
            _actif = true;
            _maxX = maxX;
            _maxY = maxY;
            _titre = "Bienvenue dans le jeu du Puissance 4 !!!";
            _presentation1 = "Le jeu se joue à deux joueurs.";
            _presentation2 = "Pour gagner, il faut arriver à aligner 4 pions de la même couleur.";

            _jouer = "Jouer";
            _touches = "Touches";
            _quitter = "Quitter";
            this.Game.Components.Add(this);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public void LoadContent(ContentManager Content)
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("police");


            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            Vector2 titreSize = _font.MeasureString(_titre);
            _spriteBatch.DrawString(_font, _titre, new Vector2( ((float)_maxX - titreSize.X) /2, 0), Color.Black);
            Vector2 pres1Size = _font.MeasureString(_presentation1);
            _spriteBatch.DrawString(_font, _presentation1, new Vector2(((float)_maxX - pres1Size.X) / 2, 1*(float)_maxY/10 ), Color.Black);
            Vector2 pres2Size = _font.MeasureString(_presentation2);
            _spriteBatch.DrawString(_font, _presentation2, new Vector2(((float)_maxX - pres2Size.X) / 2, 2 * (float)_maxY / 10), Color.Black);
            Vector2 jouerSize = _font.MeasureString(_jouer);
            _spriteBatch.DrawString(_font, _jouer, new Vector2(((float)_maxX - jouerSize.X) / 2, 4 * (float)_maxY / 10), Color.Black);
            Vector2 touchesSize = _font.MeasureString(_touches);
            _spriteBatch.DrawString(_font, _touches, new Vector2(((float)_maxX - touchesSize.X) / 2, 6 * (float)_maxY / 10), Color.Black);
            Vector2 quitterSize = _font.MeasureString(_quitter);
            _spriteBatch.DrawString(_font, _quitter, new Vector2(((float)_maxX - quitterSize.X) / 2, 8 * (float)_maxY / 10), Color.Black);

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
