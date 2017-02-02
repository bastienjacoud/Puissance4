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
            int place = deciderPlace(numCol);
            _map[place, numCol].pion = new Pion(game, maxX, maxY, minX, minY, numJ);


        }

        public int deciderPlace(int numCol)     // 7 col 6 lig
        {
            for (int i = 1; i < 6; i++)     //démarre à 1 car considère que première case forcément vide (à vérifier dans appel fonction)
            {
                if (_map[i, numCol] != null)
                {
                    return i - 1;
                }
            }
            return 5;
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

        public Boolean verifDiagonale()
        {
            Boolean gagne = true;
            int numJ;
        }
    }
}
