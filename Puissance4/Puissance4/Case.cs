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
        private double _maxX;
        private double _maxY;
        private double _minX;
        private double _minY;

        private ObjetPuissance4 _case;
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

        public Case(Game game,double maxX,double maxY,double minX,double minY) : base(game)
        {

        }
    }
}
