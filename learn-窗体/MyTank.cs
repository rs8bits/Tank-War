using learn_窗体.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace learn_窗体
{
    class MyTank:Movething
    {
        public int hp = 20;
        public bool IsMoving { get; set; }
        public MyTank(int x,int y,int speed,Direction dir)
        {
            this.IsMoving = false;
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            
            BitmapUp = Resources.MyTankUp;
            BitmapDown = Resources.MyTankDown;
            BitmapLeft = Resources.MyTankLeft;
            BitmapRight = Resources.MyTankRight;
            this.Dir = Direction.Up;
        }

        public override void update()
        {
            MoveCheck();
            Move();
            base.update();

        }

        private void MoveCheck()
        {
            if(Dir == Direction.Up)
            {
                if (Y - Speed < 0)
                {
                    IsMoving = false;return;
                }
            }
            if (Dir == Direction.Down)
            {
                if (Y + Speed +Height>390 )
                {
                    IsMoving = false; return;
                }
            }
            if (Dir == Direction.Left)
            {
                if (X - Speed < 0)
                {
                    IsMoving = false; return;
                }
            }
            if (Dir == Direction.Right)
            {
                if (X + Speed + Width > 390)
                {
                    IsMoving = false; return;
                }
            }

            Rectangle rect = GetRectangle();
            switch (Dir)
            {
                case Direction.Up:
                    rect.Y -= Speed;break;
                case Direction.Down:
                    rect.Y += Speed; break;
                case Direction.Left:
                    rect.X -= Speed; break;
                case Direction.Right:
                    rect.X += Speed; break;
            }
            if (GameObjectManager.IsCollidedWall(rect) != null)
            {
                IsMoving = false;return;
            }
            if (GameObjectManager.IsCollidedSteel(rect) != null)
            {
                IsMoving = false; return;
            }
            if (GameObjectManager.IsCollidedBoss(rect))
            {
                IsMoving = false; return;
            }
        }

        private void Move()
        {
            if (IsMoving == false) return;
            switch (Dir)
            {
                case Direction.Up:
                    Y-= Speed;
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
        public void keydown(KeyEventArgs args)
        {
            switch (args.KeyCode)
            {
                case Keys.W:
                    Dir = Direction.Up;
                    IsMoving = true;
                    break;
                case Keys.A:
                    Dir = Direction.Left;
                    IsMoving = true;
                    break;
                case Keys.S:
                    Dir = Direction.Down;
                    IsMoving = true;
                    break;
                case Keys.D:
                    Dir = Direction.Right;
                    IsMoving = true;
                    break;
                case Keys.Space:
                    attack();
                    break;
            }
        }
        private void attack()
        {
            int x = this.X;
            int y = this.Y;
            switch (Dir)
            {
                case Direction.Up:
                    x = x + Width / 2;break;
                case Direction.Down:
                    x = x + Width / 2;y += Height; break;
                case Direction.Left:
                    y = y + Height / 2; break;
                case Direction.Right:
                    x = x + Width ; y =y+ Height/2; break;
            }

            GameObjectManager.CreateBullet(x, y, Tag.mt, Dir);
        }
        public void keyup(KeyEventArgs args)
        {
            switch (args.KeyCode)
            {
                case Keys.W:                 
                    IsMoving = false;
                    break;
                case Keys.A:                
                    IsMoving = false;
                    break;
                case Keys.S:               
                    IsMoving = false;
                    break;
                case Keys.D:               
                    IsMoving = false;
                    break;
            }
        }
    }
    
}
