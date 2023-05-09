using learn_窗体.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace learn_窗体
{
    class GameObjectManager
    {
        private static List<NotMovething> walllist = new List<NotMovething>();
        private static List<NotMovething> steellist = new List<NotMovething>();
        private static NotMovething boss;
        private static MyTank mt;
        private static List<EnemyTank> tl = new List<EnemyTank>();
        public  static List<Explosion> expList = new List<Explosion>();
        private static List<Bullet> Bulletlist = new List<Bullet>();
        private static int enemyBornSpeed = 60;
        private static int enemyBornCount = 60;
        private static Point[] points = new Point[3];
        private static object _bulletlock = new object();

        public static void CreateBullet(int x, int y, Tag tag, Direction dir)
        {
            Bullet bullet = new Bullet(x, y, 5, dir, tag);
            
                Bulletlist.Add(bullet);
            
        }
      
        public static void checkDes()
        {
            List<Bullet> bdes = new List<Bullet>();
            for (int i = 0; i < Bulletlist.Count; i++)
            {

                if (Bulletlist[i].isDes == true)
                {
                    bdes.Add(Bulletlist[i]);     
                }
            }
            foreach(Bullet b in bdes)
            {
                Bulletlist.Remove(b);
            }
        }
        public static void desWall(NotMovething wall)
        {
            walllist.Remove(wall);
        }
        public static void desTank(EnemyTank et)
        {
            tl.Remove(et);
        }

        public static void CreateExp(int x,int y)
        {
            Explosion exp = new Explosion(x, y);
            expList.Add(exp);
        }
        public static void Start()
        {
          
            points[0].X = 0; points[0].Y = 0;
            points[1].X = 6*30; points[1].Y = 0;
            points[2].X = 12*30; points[2].Y = 0;
        }
        private static void enemyBorn()
        {
            enemyBornCount++;
            if (enemyBornCount < enemyBornSpeed)
                return;

            Random rd = new Random();
            int ind = rd.Next(0, 3);
            Point pos = points[ind];
            int enemyType = rd.Next(1, 5);
            switch (enemyType)
            {
                case 1: createEnemyTank1(pos.X, pos.Y);break;
                case 2: createEnemyTank2(pos.X, pos.Y); break;
                case 3: createEnemyTank3(pos.X, pos.Y); break;
                case 4: createEnemyTank4(pos.X, pos.Y); break;
            }
           
            enemyBornCount = 0;
        }

        private static void createEnemyTank1(int x,int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 2, Resources.GrayDown, Resources.GrayUp, Resources.GrayRight, Resources.GrayLeft);
            tl.Add(tank);
        }
        private static void createEnemyTank2(int x,int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 2, Resources.GreenDown, Resources.GreenUp, Resources.GreenRight, Resources.GreenLeft);
            tl.Add(tank);
        }
        private static void createEnemyTank3(int x, int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 4, Resources.QuickDown, Resources.QuickUp, Resources.QuickRight, Resources.QuickLeft);
            tl.Add(tank);
        }
        private static void createEnemyTank4(int x, int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 1, Resources.SlowDown, Resources.SlowUp, Resources.SlowRight, Resources.SlowLeft);
            tl.Add(tank);
        }
        public static NotMovething IsCollidedWall(Rectangle rt)
        {
            foreach(NotMovething wall in walllist)
            {
                if (wall.GetRectangle().IntersectsWith(rt))
                {
                    return wall;
                }
            }
            return null;
        }
        public static NotMovething IsCollidedSteel(Rectangle rt)
        {
            foreach (NotMovething wall in steellist)
            {
                if (wall.GetRectangle().IntersectsWith(rt))
                {
                    return wall;
                }
            }
            return null;
        }
        public static bool IsCollidedBoss(Rectangle rt)
        {
            return boss.GetRectangle().IntersectsWith(rt);
        }
        public static EnemyTank IsCollidedEnemyTank(Rectangle rt)
        {
            foreach(EnemyTank et in tl)
            {
                if (et.GetRectangle().IntersectsWith(rt))
                {
                    return et;
                }
            }
            return null;
    ;
        }
        public static MyTank IsCollidedMytank(Rectangle rt)
        {
            if (mt.GetRectangle().IntersectsWith(rt))
            {
                return mt;
            }
            return null;
        }
        public static void update()
        {
            foreach (NotMovething nm in walllist)
            {
                nm.update();
            }
            foreach (NotMovething nm in steellist)
            {
                nm.update();
            }
            foreach(EnemyTank tank in tl)
            {
                tank.update();
            }
            checkDes();
            for (int i=0;i<Bulletlist.Count;i++)
                {
                Bulletlist[i].update();
                }
            checkDes();
            foreach (Explosion exp in expList)
            {
                exp.update();
            }
            CheckdesExp();

            boss.update();
            mt.update();
            enemyBorn();
        }
        private static void CheckdesExp()
        {
            List<Explosion> bdes = new List<Explosion>();
            foreach (Explosion b in expList)
            {

                if (b.isdes == true)
                {
                    bdes.Add(b);
                }
            }
            foreach (Explosion b in bdes)
            {
                expList.Remove(b);
            }
        }
        /*
        public static void DrawMap()
        {
            foreach(NotMovething nm in walllist)
            {
                nm.DrawSelf();
            }
            foreach (NotMovething nm in steellist)
            {
                nm.DrawSelf();
            }
            boss.DrawSelf();
        }
        public static void DrawMytank()
        {
            mt.DrawSelf();
        }
        */
        public static void CreateMytank()
        {
            int x = 4 * 30;
            int y = 12 * 30;
            mt = new MyTank(x, y, 1,Direction.Up);
        }
        public static void CreateMap()
        {
            CreateWall(1, 1, 5,Resources.wall, walllist);
            CreateWall(3, 1, 5,Resources.wall, walllist);
            CreateWall(5, 1, 4, Resources.wall, walllist);
            CreateWall(7, 1, 4, Resources.wall, walllist);
            CreateWall(9, 1, 5, Resources.wall,walllist);
            CreateWall(11, 1, 5, Resources.wall, walllist);
            CreateWall(6, 3, 1, Resources.steel, steellist);
            
            CreateWall(2, 7, 1, Resources.wall, walllist);
            CreateWall(3, 7, 1, Resources.wall, walllist);
            CreateWall(5, 6, 1, Resources.wall, walllist);

            CreateWall(7, 6, 1, Resources.wall, walllist);
            CreateWall(9, 7, 1, Resources.wall, walllist);
            CreateWall(10, 7, 1, Resources.wall, walllist);

            CreateWall(1, 9, 4, Resources.wall, walllist);
            CreateWall(3, 8, 4, Resources.wall, walllist);
            CreateWall(5, 8, 2, Resources.wall, walllist);
            CreateWall(7, 8, 2, Resources.wall, walllist);
            CreateWall(9, 8, 4, Resources.wall, walllist);
            CreateWall(11, 9, 4, Resources.wall, walllist);

            CreateWall(0, 7, 1, Resources.steel, steellist);
            CreateWall(12, 7, 1, Resources.steel, steellist);

            CreateWall(5, 11, 2, Resources.wall, walllist);
            CreateWall(6, 11, 1, Resources.wall, walllist);
            Createboss(6, 12, Resources.Boss );
            CreateWall(7, 11, 2, Resources.wall, walllist);
            
        } 

        private static void Createboss(int x,int y,Image img)
        {
            int xp = x * 30;
            int yp = y * 30;
            boss = new NotMovething(xp, yp, img);
            
        }
        private static void CreateWall(int x,int y,int count,Image img,List<NotMovething> walllist)
        {
    
            int xp = x * 30;
            int yp = y * 30;
            for(int i = yp; i < yp + count * 30; i += 15)
            {
                NotMovething wall1 = new NotMovething(xp, i, img);
                NotMovething wall2 = new NotMovething(xp+15, i, img);
                walllist.Add(wall1);
                walllist.Add(wall2);
            }
             
        }
        public static void keydown(KeyEventArgs args)
        {
            mt.keydown(args);
        }
        public static void keyup(KeyEventArgs args)
        {
            mt.keyup(args);
        }

        
    }
}
