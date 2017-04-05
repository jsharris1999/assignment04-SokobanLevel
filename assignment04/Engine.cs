using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;
using static assignment04.Sprite;

namespace assignment04
{
    public partial class Engine : Form
    {
        public static Sprite parent = new Sprite();
        public static Sprite canvas = new Sprite();
        public static Engine form;
        public Thread ActThread;
        public Thread PaintThread;
        public static int fps = 60;
        public static double running_fps = 60.0;

        public Engine()
        {
            DoubleBuffered = true;
            form = this;
            PaintThread = new Thread(new ThreadStart(render));
            ActThread = new Thread(new ThreadStart(update));
            PaintThread.Start();
            ActThread.Start();
        }

        public static void render()
        {
            DateTime last = DateTime.Now;
            DateTime now = last;
            TimeSpan frameTime = new TimeSpan(10000000 / fps);
            while (true)
            {
                DateTime temp = DateTime.Now;
                running_fps = .9 * running_fps + .1 * 1000.0 / (temp - now).TotalMilliseconds;
                now = temp;
                TimeSpan diff = now - last;
                if (diff.TotalMilliseconds < frameTime.TotalMilliseconds)
                    Thread.Sleep((frameTime - diff).Milliseconds);
                last = DateTime.Now;
                form.Invoke(new MethodInvoker(form.Refresh));

            }
        }

        public static void update()
        {
            DateTime last = DateTime.Now;
            DateTime now = last;
            TimeSpan frameTime = new TimeSpan(10000000 / fps);
            while (true)
            {
                DateTime temp = DateTime.Now;
                now = temp;
                TimeSpan diff = now - last;
                if (diff.TotalMilliseconds < frameTime.TotalMilliseconds)
                    Thread.Sleep((frameTime - diff).Milliseconds);
                last = DateTime.Now;
                parent.update();

            }
        }
        /*
        protected override void OnKeyDown(KeyEventArgs e)
        {
            //base.OnKeyDown(e);
            //Console.WriteLine("asdffasdf");
            if (e.KeyCode == Keys.Left) parent.TargetX -= 30;
            if (e.KeyCode == Keys.Right) parent.TargetX += 30;
            if (e.KeyCode == Keys.Up) parent.TargetY -= 30;
            if (e.KeyCode == Keys.Down) parent.TargetY += 30;
        }
        */


        protected override void OnMouseMove(MouseEventArgs e)
        {
            //Refresh();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            ActThread.Abort();
            PaintThread.Abort();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            canvas.render(e.Graphics);
            parent.render(e.Graphics);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
