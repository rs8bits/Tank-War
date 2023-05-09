using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learn_窗体
{
    abstract class GameObject
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        protected abstract Image GetImage();
        public void DrawSelf()
        {
            Graphics g = GameFrameWork.g;
            g.DrawImage(GetImage(), X, Y);
        }
        public virtual void update()
        {
            DrawSelf();
        }
        public Rectangle GetRectangle()
        {
            Rectangle rec = new Rectangle(X,Y,Width,Height);
            return rec;
        }
    }
}
