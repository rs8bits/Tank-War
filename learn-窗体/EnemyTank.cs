using learn_窗体.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learn_窗体
{
    class EnemyTank:Movething
    {
        public bool IsMoving { get; set; }
        public int attckSpeed = 100;
        private int attackCount = 0;
        public int changeDirSpeed = 100;
        private int changeDirCount = 0;
        private Random r = new Random();
        public EnemyTank(int x, int y, int speed, Bitmap bd, Bitmap bu, Bitmap br, Bitmap bl )
        {
            this.IsMoving = true;
            this.X = x;
            this.Y = y;
            this.Speed = speed;

            BitmapUp = bu;
            BitmapDown = bd;
            BitmapLeft = bl;
            BitmapRight = br;
            this.Dir = Direction.Down;
        }
        private void AutoChangeDir()
        {
            changeDirCount++;
            if (changeDirCount % changeDirSpeed == 0)
            {
                ChangeDir();
                changeDirCount = 0;
            }
        }
        public override void update()
        {
            MoveCheck();
            Move();
            attackCheck();
            AutoChangeDir();
            base.update();

        }
        private void attackCheck()
        {
            attackCount++;
            if (attackCount % attckSpeed == 0)
            {
                attack();
                attackCount = 0;
            }
        }
        private void attack()
        {
            int x = this.X;
            int y = this.Y;
            switch (Dir)
            {
                case Direction.Up:
                    x = x + Width / 2; break;
                case Direction.Down:
                    x = x + Width / 2; y += Height; break;
                case Direction.Left:
                    y = y + Height / 2; break;
                case Direction.Right:
                    x = x + Width; y = y + Height / 2; break;
            }

            GameObjectManager.CreateBullet(x, y, Tag.et, Dir);
        }
        private void MoveCheck()
        {
            if (Dir == Direction.Up)
            {
                if (Y - Speed < 0)
                {
                    ChangeDir(); return;
                }
            }
            if (Dir == Direction.Down)
            {
                if (Y + Speed + Height > 390)
                {
                    ChangeDir(); return;
                }
            }
            if (Dir == Direction.Left)
            {
                if (X - Speed < 0)
                {
                    ChangeDir(); return;
                }
            }
            if (Dir == Direction.Right)
            {
                if (X + Speed + Width > 390)
                {
                    ChangeDir(); return;
                }
            }

            Rectangle rect = GetRectangle();
            switch (Dir)
            {
                case Direction.Up:
                    rect.Y -= Speed; break;
                case Direction.Down:
                    rect.Y += Speed; break;
                case Direction.Left:
                    rect.X -= Speed; break;
                case Direction.Right:
                    rect.X += Speed; break;
            }
            if (GameObjectManager.IsCollidedWall(rect) != null)
            {
               ChangeDir(); return;
            }
            if (GameObjectManager.IsCollidedSteel(rect) != null)
            {
                ChangeDir(); return;
            }
            if (GameObjectManager.IsCollidedBoss(rect))
            {
                ChangeDir(); return;
            }
        }

        private void ChangeDir()
        {
            while (true)
            {
                Direction dir = (Direction)r.Next(0, 4);
                if (dir == Dir)
                {
                    continue;
                }
                {
                    Dir = dir;break;
                }
               
            }
            MoveCheck();
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
