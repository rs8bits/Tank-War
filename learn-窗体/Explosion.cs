using learn_窗体.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learn_窗体
{
    class Explosion:GameObject
    {
        public bool isdes = false;
        private int playSpeed = 4;
        private int playCount = 0;
        private int index = 0; 
        private Bitmap[] bmpArr = new Bitmap[]
        {
            Resources.EXP1,
             Resources.EXP2,
              Resources.EXP3,
               Resources.EXP4,
               Resources.EXP5,
        };
        public Explosion(int x,int y)
        {
            foreach(Bitmap bmp in bmpArr)
            {
                bmp.MakeTransparent();

            }
            this.X = x - bmpArr[0].Width / 2;
            this.Y = y - bmpArr[0].Height / 2;
        }
        protected override Image GetImage()
        {
           
           
          
            return bmpArr[index];
        }
        public void Drawself()
        {
            base.DrawSelf();
           
        }
        public override void update()
        {
            playCount++;
            index = (playCount - 1) / playSpeed;
            index %= 5;
            if (index == 4)
            {
                this.isdes = true;
            }
            base.update();
        }
    }
}
