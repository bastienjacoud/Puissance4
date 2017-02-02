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

        public Case(Game game) :base(game)
        {

        }
    }
}
