using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace TransferСalculation
{
    //TODO 1. Добавить обработку null с перескакиванием столбца 2.Добавить лейблы 3. Сделать крупкю сетку читаеемее с помощь замутнения некоторых промежуточных линий сетки

    internal class Chart
    {
        byte ChartStile;

        PictureBox PictureBox;
        Image Table;

        short NumberOfPoles;

        int MaxValue, MinValue;
        float GreadVolumeStap;

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

        public Chart(PictureBox pictureBox, short NumberOfPoles, int MinValue, int MaxValue, float GreadVolumeStap)
        {
            PictureBox = pictureBox;
            this.NumberOfPoles = NumberOfPoles;
            this.MaxValue = MaxValue;
            this.MinValue = MinValue;
            this.GreadVolumeStap = GreadVolumeStap;
            PolesPositions = new ushort[NumberOfPoles];

            DrawGread();
            pictureBox.Image = Table;
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
            // Line
            if (ChartStile == 0)
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
            // Pillows
            else
            {

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
