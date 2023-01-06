using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsChart
{
    public partial class Chart : UserControl
    {
        #region
        public enum ChartStile { Line }
        [Description("Type of visual"), Category("Chart")]
        public ChartStile chartStile { get; set; }

        [Category("Chart")]
        public short NumberOfPoles { get; set; }

        [Category("Chart")]
        public int MaxValue { get; set; }

        [Category("Chart")]
        public int MinValue { get; set; }

        [Category("Chart")]
        public float GreadVolumeStap { get; set; }
        #endregion

        Image Table;

        byte MinIndent = 3;
        int IndentX;

        ushort[] PolesPositions;

        Color[] Palette = new Color[6]
        {
            Color.Red,
            Color.Green,
            Color.Blue,
            Color.Purple,
            Color.Orange,
            Color.LightBlue
        };

        public Chart()
        {
            InitializeComponent();
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            PolesPositions = new ushort[NumberOfPoles];

            DrawGread();
            PictureBox.Image = Table;
        }


        public void Update(double[] Values, Color color)
        {
            Point[] Points = new Point[NumberOfPoles];

            double factor = (double)(PictureBox.Height - (MinIndent * 2 + 1)) / (MaxValue - MinValue);

            for (int i = 0; i < Values.Length; i++)
            {
                Points[i] = new Point(PolesPositions[i] + MinIndent, (int)(PictureBox.Height - (Values[i] * factor)) - (MinIndent + 1));
            }

            Image image = DrawChartLine(color, Points);
            PictureBox.Image = image;
        }

        
        public void Update(double[][] Values)
        {
            if (chartStile == ChartStile.Line)
            {
                int palettecell = -1;
                foreach (double[] mas in Values)
                {
                    Point[] Points = new Point[NumberOfPoles];

                    double factor = (double)(PictureBox.Height - (MinIndent * 2 + 1)) / (MaxValue - MinValue);

                    for (int i = 0; i < Values.Length; i++)
                    {
                        Points[i] = new Point(PolesPositions[i] + MinIndent, (int)(PictureBox.Height - (mas[i] * factor)) - (MinIndent + 1));
                    }

                    palettecell++;
                    Image image = DrawChartLine(Palette[palettecell], Points);
                    PictureBox.Image = image;
                }
            }
        }

        void DrawGread()
        {
            Table = new Bitmap(PictureBox.Width, PictureBox.Height);

            Color GridColor = Color.FromArgb(130, 130, 130);

            ushort HorisontalLiens = (ushort)((MaxValue - MinValue) / GreadVolumeStap);

            float VerticalLinesStep = (float)(PictureBox.Width - (MinIndent * 2 + 1)) / (NumberOfPoles - 1);
            float HorisontallLinesStep = (float)(PictureBox.Height - (MinIndent * 2 + 1)) / HorisontalLiens;

            using (var graphics = Graphics.FromImage(Table))
            {
                for (int i = 0; i < NumberOfPoles; i++)
                {
                    graphics.DrawLine(
                        new Pen(GridColor),
                        new Point((int)(i * VerticalLinesStep + MinIndent), MinIndent),
                        new Point((int)(i * VerticalLinesStep + MinIndent), PictureBox.Height - (MinIndent + 1))
                        );

                    PolesPositions[i] = (ushort)(i * VerticalLinesStep + IndentX);
                }

                for (int i = 0; i <= HorisontalLiens; i++)
                {
                    graphics.DrawLine(
                        new Pen(GridColor),
                        new Point(MinIndent, (int)(i * HorisontallLinesStep + MinIndent)),
                        new Point(PictureBox.Width - (MinIndent + 1), (int)(i * HorisontallLinesStep + MinIndent)));
                }
            }
        }

        Image DrawChartLine(Color color, Point[] points)
        {
            Image image = Table.Clone() as Image;
            Pen pen = new Pen(color);
            pen.Width = 2;

            using (var graphics = Graphics.FromImage(image))
            {
                graphics.DrawLines(pen, points);
            }

            return image;
        }
    }
}
