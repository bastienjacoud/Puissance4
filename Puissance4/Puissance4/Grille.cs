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


        public Case[,] map
        {
            get
            {
                return _map;
            }
            set
            {
                _map = map;
            }
        }


        public Grille(Game game, double maxX, double maxY) : base(game)
        {

            _map = new Case[6, 7];
            double posDepartX;
            double posDepartY = maxY-(6*100);
            for(int i=0;i<6;i++)
            {
                posDepartX = (maxX-(7*100)) / 2;
                for (int j=0;j<7;j++)
                {
                    _map[i, j] = new Case(game,posDepartX,posDepartY);
                    posDepartX += 100;
                }
                posDepartY += 100;
            }

            this.Game.Components.Add(this);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }


        public void placerPion(Game game, int numJ, int numCol){
            int place = deciderPlace(numCol);
            //_map[place, numCol].pion = new Pion(game, _map[place,numCol].case1.Position.X, _map[place, numCol].case1.Position.Y, numJ);
            _map[place, numCol] = new Case(game, _map[place, numCol].case1.Position.X, _map[place, numCol].case1.Position.Y, _map[place, numCol].case1.Position.X, _map[place, numCol].case1.Position.Y, numJ);

        }

        public int deciderPlace(int numCol)     // 7 col 6 lig
        {
            int i = 5;
            while(i>=0)
            {
                if (_map[i, numCol].pion == null)
                    return i;
                else
                    i--;
            }
            return i;
        }

        public Boolean verifHorizontale()
        {
            Boolean gagne = true;                                               //true si partie gagnée, false sinon
            int numJ;
            for (int i_ligne = 0; i_ligne < 6; i_ligne++)                       //parcourt les lignes
            {
                for (int i_col = 0; i_col < 4; i_col++)                         //parcout les colonnes
                {
                    gagne = true;                                               //temporairement à true
                    if (_map[i_ligne, i_col] != null)                           //si on tombe sur un pion
                    {
                        numJ = _map[i_ligne, i_col].pion.numJ;
                        for (int i = i_col; i < i_col+4; i++)                   //on parcourt les 3 cases de droite
                        {
                            if (_map[i_ligne, i].pion.numJ != numJ)             //si un des pions est différent du premier
                            {
                                gagne = false;                                  //gagne passe à false
                            }
                        }
                        if (gagne)                                              //si 4 pions alignés alors on renvoie true
                        {
                            return gagne;
                        }
                    }
                }
            }
            return gagne;
        }

        public Boolean verifVerticale(){
            Boolean gagne = true;                                               //true si partie gagnée, false sinon
            int numJ;
            for (int i_col = 0; i_col < 6; i_col++)                             //parcourt les colonnes
            {
                for (int i_ligne = 0; i_ligne < 3; i_ligne++)                   //parcourt les lignes
                {
                    gagne = true;                                               //temporairement à true
                    if (_map[i_ligne, i_col] != null)                           //si on tombe sur un pion
                    {
                        numJ = _map[i_ligne, i_col].pion.numJ;
                        for (int i = i_ligne; i < i_ligne + 4; i++)             //on parcourt les 3 cases en-dessous
                        {
                            if (_map[i, i_col].pion.numJ != numJ)               //si un des pions est différent du premier
                            {
                                gagne = false;                                  //gagne passe à false
                            }
                        }
                        if (gagne)
                        {
                            return gagne;
                        }
                    }
                }
            }
            return gagne;
        }
        /*
        public Boolean verifDiagonale()
        {
            Boolean gagne = true;
            int numJ;
        }
        */
    }
}
