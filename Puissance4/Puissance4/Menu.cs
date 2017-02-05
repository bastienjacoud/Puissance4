using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Puissance4
{
    class Menu : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private KeyboardState oldKey;

        private bool _jeuActif;//permet de savoir si le menu est actif ou si l'on est dans le jeu
        private bool _touchesActif;//permet de savoir si l'on veut avoir accès aux touches
        private bool _quitterActif;//permet de savoir si l'on veut quitter le jeu
        private int _sectionMenu;//1 pour jouer, 2 pour touches et 3 pour quitter(permet le changement de couleur)
        private bool _retourMenu;

        private double _maxX;
        private double _maxY;

        private String _titre;
        private String _presentation1;
        private String _presentation2;
        private String _jouer;
        private String _touches;
        private String _quitter;

        private String _explicationTouches;
        private String _msgTouches;

        public bool jeuActif
        {
            get
            {
                return _jeuActif;
            }
            set
            {
                _jeuActif = value;
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

        public bool quitterActif
        {
            get
            {
                return _quitterActif;
            }
            set
            {
                _quitterActif = value;
            }
        }

        public bool retourMenu
        {
            get
            {
                return _retourMenu;
            }
            set
            {
                _retourMenu = value;
            }
        }

        public Menu(Game game, double maxX, double maxY) : base(game)
        {
            _jeuActif = false;
            _touchesActif = false;
            _quitterActif = false;
            _sectionMenu = 1;
            _retourMenu = false;
            _maxX = maxX;
            _maxY = maxY;
            _titre = "Bienvenue dans le jeu du Puissance 4 !!!";
            _presentation1 = "Le jeu se joue à deux joueurs.";
            _presentation2 = "Pour gagner, il faut arriver à aligner 4 pions de la même couleur.";

            _jouer = "Jouer";
            _touches = "Touches";
            _quitter = "Quitter";

            _explicationTouches = "Déplacez-vous avec les flèches puis lachez le pion avce entrée.";
            _msgTouches = "Appuyez sur entrée pour revenir au menu principal.";
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
            if(!_jeuActif && !_touchesActif && !_quitterActif)
            {
                Vector2 titreSize = _font.MeasureString(_titre);
                _spriteBatch.DrawString(_font, _titre, new Vector2(((float)_maxX - titreSize.X) / 2, 0), Color.Black);
                Vector2 pres1Size = _font.MeasureString(_presentation1);
                _spriteBatch.DrawString(_font, _presentation1, new Vector2(((float)_maxX - pres1Size.X) / 2, 1 * (float)_maxY / 10), Color.Black);
                Vector2 pres2Size = _font.MeasureString(_presentation2);
                _spriteBatch.DrawString(_font, _presentation2, new Vector2(((float)_maxX - pres2Size.X) / 2, 2 * (float)_maxY / 10), Color.Black);
                Vector2 jouerSize = _font.MeasureString(_jouer);
                if (_sectionMenu == 1)
                    _spriteBatch.DrawString(_font, _jouer, new Vector2(((float)_maxX - jouerSize.X) / 2, 4 * (float)_maxY / 10), Color.Red);
                else
                    _spriteBatch.DrawString(_font, _jouer, new Vector2(((float)_maxX - jouerSize.X) / 2, 4 * (float)_maxY / 10), Color.Blue);
                Vector2 touchesSize = _font.MeasureString(_touches);
                if (_sectionMenu == 2)
                    _spriteBatch.DrawString(_font, _touches, new Vector2(((float)_maxX - touchesSize.X) / 2, 6 * (float)_maxY / 10), Color.Red);
                else
                    _spriteBatch.DrawString(_font, _touches, new Vector2(((float)_maxX - touchesSize.X) / 2, 6 * (float)_maxY / 10), Color.Blue);
                Vector2 quitterSize = _font.MeasureString(_quitter);
                if (_sectionMenu == 3)
                    _spriteBatch.DrawString(_font, _quitter, new Vector2(((float)_maxX - quitterSize.X) / 2, 8 * (float)_maxY / 10), Color.Red);
                else
                    _spriteBatch.DrawString(_font, _quitter, new Vector2(((float)_maxX - quitterSize.X) / 2, 8 * (float)_maxY / 10), Color.Blue);
            }
            else if(_touchesActif)
            {
                Vector2 explicationSize = _font.MeasureString(_explicationTouches);
                _spriteBatch.DrawString(_font, _explicationTouches, new Vector2(((float)_maxX - explicationSize.X) / 2, 4 * (float)_maxY / 10), Color.Black);
                Vector2 msgTouchesSize = _font.MeasureString(_msgTouches);
                _spriteBatch.DrawString(_font, _msgTouches, new Vector2(((float)_maxX - msgTouchesSize.X) / 2, 7 * (float)_maxY / 10), Color.Black);
            }
            

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            ActionMenu();
            base.Update(gameTime);
        }

        private void ActionMenu()
        {
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Down))
            {
                if(!oldKey.IsKeyDown(Keys.Down))
                {
                    if (_sectionMenu == 1)
                        _sectionMenu = 2;
                    else if (_sectionMenu == 2)
                        _sectionMenu = 3;
                }
            }
            if (keyboard.IsKeyDown(Keys.Up))
            {
                if (!oldKey.IsKeyDown(Keys.Up))
                {
                    if (_sectionMenu == 3)
                        _sectionMenu = 2;
                    else if (_sectionMenu == 2)
                        _sectionMenu = 1;
                }
            }
            if (keyboard.IsKeyDown(Keys.Enter))
            {
                if (!oldKey.IsKeyDown(Keys.Enter))
                {
                    if(!_retourMenu)
                    {
                        if (_touchesActif)
                        {
                            _touchesActif = false;
                        }
                        else
                        {
                            if (_sectionMenu == 1)
                            {
                                _jeuActif = true;
                                _touchesActif = false;
                                _quitterActif = false;
                            }
                            else if (_sectionMenu == 2)
                            {
                                _jeuActif = false;
                                _touchesActif = true;
                                _quitterActif = false;
                            }
                            else if (_sectionMenu == 3)
                            {
                                _jeuActif = false;
                                _touchesActif = false;
                                _quitterActif = true;
                            }
                        }
                    }
                    _retourMenu = false;
                    
                }
            }
            oldKey = keyboard;

        }
    }
}
