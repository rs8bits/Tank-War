using learn_窗体.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learn_窗体
{
    enum Tag
    {
        mt,et
    }
    class Bullet:Movething
    {
        public Tag tag { get; set; }
        public bool isDes { get; set; }
        public Bullet(int x, int y, int speed, Direction dir,Tag tag)
        {
            
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            this.isDes = false;
            BitmapUp = Resources.BulletUp;
            BitmapDown = Resources.BulletDown;
            BitmapLeft = Resources.BulletLeft;
            BitmapRight = Resources.BulletRight;
            this.Dir = dir;
            this.tag = tag;
            this.X -= Width / 2;
            this.Y -= Height / 2;
        }
        public override void update()
        {
            MoveCheck();
            Move();
            base.update();

        }

        private void MoveCheck()
        {
            if (Dir == Direction.Up)
            {
                if (Y+Height/2 +3< 0)
                {
                    isDes = true; return;
                }
            }
            if (Dir == Direction.Down)
            {
                if (Y +Height/2-3 > 390)
                {
                    isDes = true; return;
                }
            }
            if (Dir == Direction.Left)
            {
                if (X +Width/2 -3  < 0)
                {
                    isDes = true; return;
                }
            }
            if (Dir == Direction.Right)
            {
                if (X +Width/2+3 > 390)
                {
                    isDes = true; return;
                }
            }

            Rectangle rect = GetRectangle();
            rect.X = X + Width / 2 - 3;
            rect.Y = Y + Height / 2 - 3;
            rect.Width = 3;rect.Height = 3;

            int ex = this.X+Width/2;
            int ey = this.Y + Height / 2;

            NotMovething wall = null;
            if ((wall=GameObjectManager.IsCollidedWall(rect)) != null)
            {
                isDes = true;
                GameObjectManager.desWall(wall);
                GameObjectManager.CreateExp(ex, ey);
                return;
            }
            if (GameObjectManager.IsCollidedSteel(rect) != null)
            {
                GameObjectManager.CreateExp(ex, ey);
                isDes = true; return;
            }
            if (GameObjectManager.IsCollidedBoss(rect))
            {
                GameObjectManager.CreateExp(ex, ey);
                GameFrameWork.changeState();
                isDes = true; return;
            }
            if(tag == Tag.mt)
            {
                EnemyTank et = null;
                if ((et=GameObjectManager.IsCollidedEnemyTank(rect)) != null)
                {
                    isDes = true;
                    GameObjectManager.desTank(et);
                    GameObjectManager.CreateExp(ex, ey);
                    return;
                }
            }
            if (tag == Tag.et)
            {
                MyTank mt = null;
                if ((mt = GameObjectManager.IsCollidedMytank(rect)) != null)
                {
                    isDes = true;
                    mt.hp--;
                    if (mt.hp < 0)
                    {
                        mt.X = 4 * 30;
                        mt.Y = 12 * 30;
                        mt.hp = 20;
                    }
                  
                    GameObjectManager.CreateExp(ex, ey);
                    return;
                }
            }
        }

        
        private void Move()
        {

            switch (Dir)
            {
                case Direction.Up:
                    Y -= Speed;
                    break;
                case Direction.Down:
                    Y += Speed;
                    break;
                case Direction.Left:
                    X -= Speed;
                    break;
                case Direction.Right:
                    X += Speed;
                    break;
            }
        }
       
    }
}
