using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Puissance4
{
    public class Grille : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Case[,] _map;



        public Grille(Game game) : base(game)
        {
            /*
            _map = new Case[,]
            {
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0}
            };
            */
            this.Game.Components.Add(this);
            base.Initialize();
        }

        public void placerPion(Game game, double maxX, double maxY, double minX, double minY, int numJ, int numCol){
            for (int i = 0; i < 5; i++)
            {
                if (_map[i + 1, numCol].pion == null)
                {
                    _map[i, numCol].pion = new Pion(game, maxX, maxY, minX, minY, numJ);
                }
            }
        }
    }
}
