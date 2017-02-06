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
        private Case[,] _map;//tableau de case constituant la grille

        //properties 
        public Case[,] map
        {
            get
            {
                return _map;
            }
            set
            {
                _map = value;
            }
        }

        //constructeur de la grille
        public Grille(Game game, double maxX, double maxY) : base(game)
        {

            _map = new Case[6, 7];
            double posDepartX;
            double posDepartY;
            posDepartX = (maxX - (7 * 100)) / 2;
            for (int i=0;i<7;i++)
            {
                posDepartY = maxY - (6 * 100);
                for (int j=0;j<6;j++)
                {
                    _map[j, i] = new Case(game,posDepartX,posDepartY);
                    posDepartY += 100;
                }
                posDepartX += 100;
            }
            this.Game.Components.Add(this);
        }

        //fonction d'initialisation
        public override void Initialize()
        {
            base.Initialize();
        }

        //fonction de chargement
        protected override void LoadContent()
        {
            base.LoadContent();
        }

        //fonction d'affichage de toutes les cases de la grille
        public override void Draw(GameTime gameTime)
        {
            for(int i=0;i<7;i++)
            {
                for(int j=0;j<6;j++)
                {
                    _map[j, i].Draw(gameTime);
                }
            }
            base.Draw(gameTime);
        }

        //fonction d'actualisation
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        //fonction qui place un pion sur le jeu en fonction de la colonne et du joueur
        public bool placerPion(Game game, int numJ, int numCol, double posPY)
        {
            int place = deciderPlace(numCol);
            if(place>=0 && place <=6)
            {
                _map[place, numCol] = new Case(game, _map[place, numCol].case1.Position.X, _map[place, numCol].case1.Position.Y, posPY, numJ);
                return true;
            }
               
            else
            {
                return false;
            }
        }

        //fonction qui choisit à quelle case du tableau va se positionner le pion, en fonction de la colonne
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

        //vérifie si 4 pions du même joueur sont alignés horizontalement
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

        //vérifie si 4 pions du même joueur sont alignés verticalement
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

        //vérifie si 4 pions du même joueur sont alignés en diagonale
        //pour limiter les vérifications, on teste, pour chaque pion placé dans le carré 4x4 
        //situé en haut à gauche toutes les diagonales. Dans un second temps, on 
        //teste les diagonales hors du carré restantes
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
                    if (_map[i_ligne, i_col].pion != null)                           //si on rencontre un jeton
                    {                                  
                        numJ = _map[i_ligne, i_col].pion.numJ;
                        for (int diag = 1; diag < 4; diag++)                    //parcourt les jetons sur les diagonales du jeton concerné
                        {
                            if ((i_ligne - 3) >= 0 && (i_col - 3) >= 0)                             //si on peut aligner 4 (autres) jetons en haut à gauche du jeton de base
                            {
                                if (_map[i_ligne - diag, i_col - diag].pion != null)                //si on rencontre un jeton
                                {
                                    if (_map[i_ligne - diag, i_col - diag].pion.numJ != numJ)       //si le jeton rencontré est de la même couleur que celui de base
                                    {
                                        gagneDiagHG = false;
                                    }
                                    else
                                    {
                                        if (diag == 1)
                                            gagneDiagHG = true;
                                        if ((diag == 3) && gagneDiagHG)
                                            return numJ;
                                    }
                                }
                                else
                                {
                                    gagneDiagHG = false;
                                }
                            }
                            else
                            {
                                gagneDiagHG = false;
                            }
                            if ((i_ligne - 3) >= 0 && (i_col + 3) <= 6)                             //si on peut aligner 4 (autres) jetons en haut à droite du jeton de base
                            {
                                if (_map[i_ligne - diag, i_col + diag].pion != null)                //si on rencontre un jeton
                                {
                                    if (_map[i_ligne - diag, i_col + diag].pion.numJ != numJ)       //si le jeton rencontré est de la même couleur que celui de base
                                    {
                                        gagneDiagHD = false;
                                    }
                                    else
                                    {
                                        if (diag == 1)
                                            gagneDiagHD = true;
                                        if ((diag == 3) && gagneDiagHD)
                                            return numJ;
                                    }
                                }
                                else
                                {
                                    gagneDiagHD = false;
                                }
                            }
                            else
                            {
                                gagneDiagHD = false; 
                            }
                            if ((i_ligne + 3) < 6 && (i_col + 3) <= 6)                             //si on peut aligner 4 (autres) jetons en bas à droite du jeton de base
                            {
                                if (_map[i_ligne + diag, i_col + diag].pion != null)                //si on rencontre un jeton
                                {
                                    if (_map[i_ligne + diag, i_col + diag].pion.numJ != numJ)       //si le jeton rencontré est de la même couleur que celui de base
                                    {
                                        gagneDiagBD = false;
                                    }
                                    else
                                    {
                                        if (diag == 1)
                                            gagneDiagBD = true;
                                        if ((diag == 3) && gagneDiagBD)
                                            return numJ;
                                    }
                                }
                                else
                                {
                                    gagneDiagBD = false;
                                }
                            }
                            else
                            {
                                gagneDiagBD = false;
                            }
                            if ((i_ligne + 3) < 6 && (i_col - 3) >= 0)                             //si on peut aligner 4 (autres) jetons en bas à gauche du jeton de base
                            {
                                if (_map[i_ligne + diag, i_col - diag].pion != null)                //si on rencontre un jeton
                                {
                                    if (_map[i_ligne + diag, i_col - diag].pion.numJ != numJ)       //si le jeton rencontré est de la même couleur que celui de base
                                    {
                                        gagneDiagBG = false;
                                    }
                                    else
                                    {
                                        if(diag == 1)
                                            gagneDiagBG = true;
                                        if ((diag == 3) && gagneDiagBG)
                                            return numJ;
                                    }
                                }
                                else
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

            //IL RESTE 4 DIAGONALES HORS DU PETIT CARRE A TESTER
            if (_map[4, 1].pion != null)                                    //première diagonale
            {
                numJ = _map[4, 1].pion.numJ;
                for (int diag1 = 1; diag1 < 4; diag1++)
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
                for (int diag2 = 1; diag2 < 4; diag2++)
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
                for (int diag2 = 1; diag2 < 4; diag2++)
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
                for (int diag3 = 1; diag3 < 4; diag3++)
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
                for (int diag3 = 1; diag3 < 4; diag3++)
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
                for (int diag4 = 1; diag4 < 4; diag4++)
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

        //fontion qui vérifie qu'un joueur n'a pas gagné. Retourne 1 si le joueur 1 a gagné et 2 si le 2 a gagné
        public int verifGrille()
        {
            int d = verifDiagonale();
            int h = verifHorizontale();
            int v = verifVerticale();
            if (d != 0)
                return d;
            else if (h != 0)
                return h;
            else
                return v;
        }
    }
}
