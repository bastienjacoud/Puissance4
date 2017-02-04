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
            for(int i=0;i<6;i++)
            {
                for(int j=0;j<7;j++)
                {
                    _map[i, j].Draw(gameTime);
                }
            }
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }


        public bool placerPion(Game game, int numJ, int numCol)
        {
            int place = deciderPlace(numCol);
            if(place>=0 && place <=6)
            {
                _map[place, numCol] = new Case(game, _map[place, numCol].case1.Position.X, _map[place, numCol].case1.Position.Y, _map[place, numCol].case1.Position.X, _map[place, numCol].case1.Position.Y, numJ);
                return true;
            }
               
            else
            {
                return false;
            }
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

        public int verifHorizontale()                                           //0 si personne gagne, 1 si jaune, 2 si rouge
        {
            Boolean gagne = true;                                               //true si partie gagnée, false sinon
            int numJ;
            for (int i_ligne = 0; i_ligne < 6; i_ligne++)                       //parcourt les lignes
            {
                for (int i_col = 0; i_col < 4; i_col++)                         //parcourt les colonnes
                {
                    gagne = true;                                               //temporairement à true
                    if (_map[i_ligne, i_col].pion != null)                           //si on tombe sur un pion
                    {
                        numJ = _map[i_ligne, i_col].pion.numJ;
                        for (int i = i_col; i < i_col+4; i++)                   //on parcourt les 3 cases de droite
                        {
                            if (_map[i_ligne, i].pion != null)                  
                            {
                                if (_map[i_ligne, i].pion.numJ != numJ)         //si un des pions est différent du premier
                                    gagne = false;                                  //gagne passe à false
                            }
                            else
                            {
                                gagne = false;
                            }
                        }
                        if (gagne)                                              //si 4 pions alignés alors on renvoie true
                        {
                            return numJ;
                        }
                    }
                    else
                    {
                        gagne = false;
                    }
                }
            }
            return 0;
        }

        public int verifVerticale(){                                            //0 si personne gagne, 1 si jaune, 2 si rouge
            Boolean gagne = true;                                               //true si partie gagnée, false sinon
            int numJ;
            for (int i_col = 0; i_col <= 6; i_col++)                             //parcourt les colonnes
            {
                for (int i_ligne = 0; i_ligne < 3; i_ligne++)                   //parcourt les lignes
                {
                    gagne = true;                                               //temporairement à true
                    if (_map[i_ligne, i_col].pion != null)                           //si on tombe sur un pion
                    {
                        numJ = _map[i_ligne, i_col].pion.numJ;
                        for (int i = i_ligne; i < i_ligne + 4; i++)             //on parcourt les 3 cases en-dessous
                        {
                            if (_map[i, i_col].pion != null)
                            {
                                if (_map[i, i_col].pion.numJ != numJ)               //si un des pions est différent du premier
                                {
                                    gagne = false;                                  //gagne passe à false
                                }
                            }
                            else
                            {
                                gagne = false;
                            }
                        }
                        if (gagne)
                        {
                            return numJ;
                        }
                    }
                    else
                    {
                        gagne = false;
                    }
                }
            }
            return 0;
        }

        public int verifDiagonale()                                             //0 si personne gagne, 1 si jaune, 2 si rouge
        {
            Boolean gagneDiagHG = true;
            Boolean gagneDiagHD = true;
            Boolean gagneDiagBD = true;
            Boolean gagneDiagBG = true;
            Boolean gagneDiag1 = true;                                          //concerne les  diagonales
            Boolean gagneDiag2 = true;
            Boolean gagneDiag3 = true;
            Boolean gagneDiag4 = true;                                          //hors du petit carré
            int numJ = 0;
            for (int i_ligne = 0; i_ligne < 4; i_ligne++)                       //parcourt les lignes du petit carré à vérifier
            {
                for (int i_col = 0; i_col < 4; i_col++)                         //parcourt les colonnes du petit carré à vérifier
                {
                    gagneDiagBD = true;
                    gagneDiagBG = true;
                    gagneDiagHD = true;
                    gagneDiagHG = true;
                    if (_map[i_ligne, i_col].pion != null)                           //si on rencontre un jeton
                    {
                        numJ = _map[i_ligne, i_col].pion.numJ;
                        for (int diag = 0; diag < 3; diag++)                    //parcourt les jetons sur les diagonales du jeton concerné
                        {
                            if ((i_ligne - 3) >= 0 && (i_col - 3) >= 0)                             //si on peut aligner 4 (autres) jetons en haut à gauche du jeton de base
                            {
                                if (_map[i_ligne - diag, i_col - diag].pion != null)                //si on rencontre un jeton
                                {
                                    if (_map[i_ligne - diag, i_col - diag].pion.numJ != numJ)       //si le jeton rencontré est de la même couleur que celui de base
                                    {
                                        gagneDiagHG = false;
                                    }
                                }
                                else
                                {
                                    gagneDiagHG = false;
                                }
                            }
                            if ((i_ligne - 3) >= 0 && (i_col + 3) <= 6)                             //si on peut aligner 4 (autres) jetons en haut à droite du jeton de base
                            {
                                if (_map[i_ligne - diag, i_col + diag].pion != null)                //si on rencontre un jeton
                                {
                                    if (_map[i_ligne - diag, i_col + diag].pion.numJ != numJ)       //si le jeton rencontré est de la même couleur que celui de base
                                    {
                                        gagneDiagHD = false;
                                    }
                                }
                                else
                                {
                                    gagneDiagHD = false;
                                }
                            }
                            if ((i_ligne + 3) <= 6 && (i_col + 3) <= 6)                             //si on peut aligner 4 (autres) jetons en bas à droite du jeton de base
                            {
                                if (_map[i_ligne + diag, i_col + diag].pion != null)                //si on rencontre un jeton
                                {
                                    if (_map[i_ligne + diag, i_col + diag].pion.numJ != numJ)       //si le jeton rencontré est de la même couleur que celui de base
                                    {
                                        gagneDiagBD = false;
                                    }
                                }
                                else
                                {
                                    gagneDiagBD = false;
                                }
                            }
                            if ((i_ligne + 3) >= 6 && (i_col - 3) <= 0)                             //si on peut aligner 4 (autres) jetons en bas à gauche du jeton de base
                            {
                                if (_map[i_ligne + diag, i_col - diag].pion != null)                //si on rencontre un jeton
                                {
                                    if (_map[i_ligne + diag, i_col - diag].pion.numJ != numJ)       //si le jeton rencontré est de la même couleur que celui de base
                                    {
                                        gagneDiagBG = false;
                                    }
                                }
                                else
                                {
                                    gagneDiagBG = false;
                                }
                            }
                        }
                    }
                }

            }
            if (gagneDiagHG || gagneDiagHD || gagneDiagBG || gagneDiagBD)               //si une des diagonales du petit carré est valide
            {
                return numJ;
            }
            //IL RESTE 4 DIAGONALES HORS DU PETIT CARRE A TESTER
            if (_map[4, 1].pion != null)                                    //première diagonale
            {
                numJ = _map[4, 1].pion.numJ;
                for (int diag1 = 0; diag1 < 3; diag1++)
                {
                    if (_map[4 - diag1, 1 + diag1].pion != null)
                    {
                        if (_map[4 - diag1, 1 + diag1].pion.numJ != numJ)
                        {
                            gagneDiag1 = false;
                        }
                    }
                    else
                    {
                        gagneDiag1 = false;
                    }
                }
                if (gagneDiag1)
                {
                    return numJ;
                }
            }

            if (_map[5, 1].pion != null)                                         //deuxième diago
            {
                numJ = _map[5, 1].pion.numJ;
                for (int diag2 = 0; diag2 < 3; diag2++)
                {
                    if (_map[5 - diag2, 1 + diag2].pion != null)
                    {
                        if (_map[5 - diag2, 1 + diag2].pion.numJ != numJ)
                        {
                            gagneDiag2 = false;
                        }
                    }
                    else
                    {
                        gagneDiag2 = false;
                    }
                    
                }
                if (gagneDiag2)
                {
                    return numJ;
                }
                else
                {
                    gagneDiag2 = true;
                }
            }
            if (_map[4, 2].pion != null)                                         //deuxième diagonale
            {
                numJ = _map[4, 2].pion.numJ;
                for (int diag2 = 0; diag2 < 3; diag2++)
                {
                    if (_map[4 - diag2, 2 + diag2].pion != null)
                    {
                        if (_map[4 - diag2, 2 + diag2].pion.numJ != numJ)
                        {
                            gagneDiag2 = false;
                        }
                    }
                    else
                    {
                        gagneDiag2 = false;
                    }
                }
                if (gagneDiag2)
                {
                    return numJ;
                }
            }


            if (_map[5, 2].pion != null)                                         //troisième diagonale
            {
                numJ = _map[5, 2].pion.numJ;
                for (int diag3 = 0; diag3 < 3; diag3++)
                {
                    if (_map[5 - diag3, 2 + diag3].pion != null)
                    {
                        if (_map[5 - diag3, 2 + diag3].pion.numJ != numJ)
                        {
                            gagneDiag3 = false;
                        }
                    }
                    else
                    {
                        gagneDiag3 = false;
                    }
                }
                if (gagneDiag3)
                {
                    return numJ;
                }
                else
                {
                    gagneDiag3 = true;
                }
            }
            if (_map[4, 3].pion != null)                                         //troisième diagonale
            {
                numJ = _map[4, 3].pion.numJ;
                for (int diag3 = 0; diag3 < 3; diag3++)
                {
                    if (_map[4 - diag3, 3 + diag3].pion != null)
                    {
                        if (_map[4 - diag3, 3 + diag3].pion.numJ != numJ)
                        {
                            gagneDiag3 = false;
                        }
                    }
                    else
                    {
                        gagneDiag3 = false;
                    }
                }
                if (gagneDiag3)
                {
                    return numJ;
                }
            }

            if (_map[5, 3].pion != null)                                            //quatrième diagonale
            {
                numJ = _map[5, 3].pion.numJ;
                for (int diag4 = 0; diag4 < 3; diag4++)
                {
                    if (_map[5 - diag4, 3 + diag4].pion != null)
                    {
                        if (_map[5 - diag4, 3 + diag4].pion.numJ != numJ)
                        {
                            gagneDiag4 = false;
                        }
                    }
                    else
                    {
                        gagneDiag4 = false;
                    }
                }
                if (gagneDiag4)
                {
                    return numJ;
                }
            }
            return 0;
        }
    }
}
