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

namespace assignment04
{
    public class Picture : Sprite
    {
        public Image mutalisk;
        public float X = 0;
        public float Y = 0;
        public float velocity = 10;

        public Picture(Image img) : base()
        {
            mutalisk = img;
            X = 0;
            Y = 0;
        }

        public Picture(Image img, int x, int y) : base()
        {
            mutalisk = img;
            this.X = x;
            TargetX = x;
            this.Y = y;
            TargetY = y;
        }

        public override void act()
        {
            if (X + velocity < TargetX)
            {
                X += velocity;
            }
            else if (X - velocity > TargetX)
            {
                X -= velocity;
            }
            else if (Math.Abs(X - TargetX) <= velocity)
            {
                X = TargetX;
            }
            if (Y + velocity < TargetX)
            {
                Y += velocity;
            }
            else if (Y - velocity > TargetX)
            {
                Y -= velocity;
            }
            else if (Math.Abs(Y - TargetY) <= velocity)
            {
                Y = velocity;
            }
        }

        public override void paint(Graphics g)
        {
            g.DrawImage(mutalisk, 0, 0);
        }


    }

    public class Text : Sprite
    {
        public String WinText;
        public Boolean Visibility;
        public int x;
        public int y;

        public Text(int x, int y, String WinText)
        {
            this.x = x;
            this.y = y;
            this.WinText = WinText;
            Visibility = false;
        }

        public void Location(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void VisibilitySetting(Boolean Visibility)
        {
            this.Visibility = Visibility;
        }

        public override void paint(Graphics g)
        {
            //base.paint(g);
            if (Visibility) g.DrawString(WinText, new Font("Arial", 18), Brushes.Chartreuse, x, y);
        }
    }
}
