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
    enum GameState
    {
        running,
        gameover
    }
    class GameFrameWork
    {
        public static Graphics g;
        private static GameState state = GameState.running;
        public static void start()
        {
            GameObjectManager.Start();
            GameObjectManager.CreateMap();
            GameObjectManager.CreateMytank();
        }
        public static void updata()
        {
           
            if(state == GameState.running)
            {
                GameObjectManager.update();
            }
            else
            {
                int x = 400 / 2 - Resources.GameOver.Width / 2;
                int y = 400 / 2 - Resources.GameOver.Height / 2;
                g.DrawImage(Resources.GameOver,x,y );
            }
        }
        public static void changeState()
        {
            state = GameState.gameover;
        }

        
    }
}