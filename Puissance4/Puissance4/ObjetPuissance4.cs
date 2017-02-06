using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Puissance4
{
    public class ObjetPuissance4
    {
        private Texture2D _texture;//texture de l'objet
        private Vector2 _position;//position de l'objet
        private Vector2 _size;//taille de l'objet

        //preperties
        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }


        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Vector2 Size
        {
            get { return _size; }
            set { _size = value; }
        }

        //constructeur
        public ObjetPuissance4(Texture2D texture, Vector2 position, Vector2 size)
        {
            this._texture = texture;
            this._position = position;
            this._size = size;
        }
    }
}
