using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace assignment04
{
    partial class Program : Engine
    {
        public static Picture elephant;
        public static Picture [,] goals;
        public static Picture [,] walls;
        public static Picture [,] blocks;
        public static Text WinWords = new Text(0, 0, "You have Won!\n\nPress \"r\" to reset the level.");
        public static int x;
        public static int y;
        public static int width;
        public static int height;

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                if (canMoveTo(x + 1, y, 1, 0)) x++;
                if (blocks[x, y] != null) moveBlock(x, y, 1, 0);
            }
            if (e.KeyCode == Keys.Left)
            {
                if (canMoveTo(x - 1, y, -1, 0)) x--;
                if (blocks[x, y] != null) moveBlock(x, y, -1, 0);
            }
            if (e.KeyCode == Keys.Up)
            {
                if (canMoveTo(x, y - 1, 0, -1)) y--;
                if (blocks[x, y] != null) moveBlock(x, y, 0, -1);
            }
            if (e.KeyCode == Keys.Down)
            {
                if (canMoveTo(x, y + 1, 0, 1)) y++;
                if (blocks[x, y] != null) moveBlock(x, y, 0, 1);
            }
            if (e.KeyCode == Keys.R)
            {
                Reset();
            }
            elephant.TargetX = x * 100;
            elephant.TargetY = y * 100;
            VictoryMessage();
        }

        public void moveBlock(int i, int j, int dx, int dy)
        {
            blocks[i + dx, j + dy] = blocks[i, j];
            blocks[i, j] = null;
            blocks[i + dx, j + dy].TargetX = (i + dx) * 100;
            blocks[i + dx, j + dy].TargetY = (j + dy) * 100;
            if (goals[i + dx, j + dy] != null) blocks[i + dx, j + dy].mutalisk = Properties.Resources.BlockOnGoal;
            else blocks[i + dx, j + dy].mutalisk = Properties.Resources.MoveableBlock;

        }

        public Boolean canMoveTo(int i, int j, int dx, int dy)
        {

            if (walls[i, j] == null && blocks[i, j] == null) return true;
            if (walls[i, j] != null) return false;
            if (blocks[i, j] != null && blocks[i + dx, j + dy] == null && walls[i + dx, j + dy] == null) return true;
            return false;

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            fixScale();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            fixScale();
        }

        private void fixScale()
        {
            canvas.Scale = Math.Min(ClientSize.Width, ClientSize.Height) / (Math.Max(height,width)*100.0f);
            canvas.TargetX = (ClientSize.Width - (100 * width * canvas.Scale)) / 2;
            canvas.TargetY = (ClientSize.Height - (100 * height * canvas.Scale)) / 2;
        }

        public static void Reset()
        {
            canvas.kill();
            parent.kill();
            LevelConstruct();
        }

        public static void LevelConstruct()
        {
            String map = Properties.Resources.SampleLevel;
            String[] lines = map.Split('\n');
            width = lines[0].Length - 1;
            height = lines.Length;
            goals = new Picture[width, height];
            walls = new Picture[width, height];
            blocks = new Picture[width, height];
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    if (lines[j][i] == 'g' || lines[j][i] == 'B')
                    {
                        goals[i, j] = new Picture(Properties.Resources.Goal, i * 100, j * 100);
                        Program.canvas.add(goals[i, j]);
                    }
                    if (lines[j][i] == 'w')
                    {
                        walls[i, j] = new Picture(Properties.Resources.WallBlock, i * 100, j * 100);
                        Program.canvas.add(walls[i, j]);
                    }
                    if (lines[j][i] == 'b' || lines[j][i] == 'B')
                    {
                        blocks[i, j] = new Picture(Properties.Resources.MoveableBlock, i * 100, j * 100);
                        if (lines[j][i] == 'B') blocks[i, j].mutalisk = Properties.Resources.BlockOnGoal;

                    }
                    if (lines[j][i] == 'c')
                    {
                        elephant = new Picture(Properties.Resources.ZergDrone, i * 100, j * 100);
                        x = i;
                        y = j;

                    }

                }

            }
            for (int j = 0; j < height; j++)
                for (int i = 0; i < width; i++)
                    if (blocks[i, j] != null) Program.canvas.add(blocks[i, j]);
            Program.canvas.add(elephant);
            WinWords.VisibilitySetting(false);
            parent.add(WinWords);
        }
        public static Boolean Victory()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (goals[i, j] != null ^ blocks[i, j] != null) return false;
                }
            }
            return true;
        }

        protected void VictoryMessage()
        {
            if (Victory())
            {
                WinWords.Location(ClientSize.Width / 2 - 100, ClientSize.Height / 2 - 80);
                WinWords.VisibilitySetting(true);
            }
            else
            {
                WinWords.VisibilitySetting(false);
            }
            fixScale();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            LevelConstruct();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Program());
        }
    }
}
