using learn_窗体.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace learn_窗体
{
    public partial class Form1 : Form
    {
        private Thread t;
        private static Graphics g;
        private static Bitmap tempbmp;
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            g = this.CreateGraphics();
            

            tempbmp = new Bitmap(450, 450);
            Graphics bmpg = Graphics.FromImage(tempbmp);
            GameFrameWork.g = bmpg;
            
            

            t = new Thread(new ThreadStart(GameMainThread));
            t.Start();
        }

        private static void GameMainThread()
        {
            GameFrameWork.start();

            int sleeptime = 1 / 60;

            while (true)
            {
                GameFrameWork.g.Clear(Color.Black);
                GameFrameWork.updata();
                g.DrawImage(tempbmp, 0, 0);
                Thread.Sleep(sleeptime);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
             t.Abort();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            GameObjectManager .keyup(e);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            GameObjectManager.keydown(e);
        }
    }
}
