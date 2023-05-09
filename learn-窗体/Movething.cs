using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learn_窗体
{
    enum Direction
    {
        Up,Down,Left,Right
    }
    class Movething : GameObject
    {
       
        public Bitmap BitmapUp { get; set; }
        public Bitmap BitmapDown { get; set; }
        public Bitmap BitmapLeft { get; set; }
        public Bitmap BitmapRight { get; set; }
        public int Speed { get; set; }
        private Direction dir;
        public Direction Dir { get { return dir; }
            set {
                dir = value;
                Bitmap bp = null;
                switch (dir)
                {
                    case Direction.Up:
                        bp = BitmapUp;break;
                    case Direction.Down:
                        bp = BitmapDown; break;
                    case Direction.Left:
                        bp = BitmapLeft; break;
                    case Direction.Right:
                        bp = BitmapRight; break;
                }
                Width = bp.Width;
                Height = bp.Height;
            } 
        }

        
        protected override Image GetImage()
        {
            Bitmap b = BitmapUp;
            switch (Dir)
            {
                
                case Direction.Up:
                    b =  BitmapUp;
                    break;
                case Direction.Down:
                    b = BitmapDown;
                    break;
                case Direction.Left:
                    b = BitmapLeft;
                    break;
                case Direction.Right:
                    b = BitmapRight;
                    break;
            }
            b.MakeTransparent(Color.Black);
            return b;
        }
    }


}
