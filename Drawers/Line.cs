using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsChart.Drawers
{
    internal class Line : IDrawer
    {
        public Chart chart { get; set; }

        public Line(Chart chart)
        {
            this.chart = chart;
        }

        void IDrawer.Update(float[][] Values, Color[] colors)
        {
            Image image = chart.Table;
            sbyte colorID = 0;

            foreach (float[] mas in Values)
            {
                #region Point calculation
                Point[] Points = new Point[chart.NumberOfPoles];

                float factor = (float)(chart.PictureBox.Height - (chart.MinIndent * 2 + 1)) / (chart.MaxValue - chart.MinValue);

                for (int i = 0; i < mas.Length; i++)
                {
                    Points[i] = new Point(chart.PolesPositions[i] + chart.MinIndent, (int)(chart.PictureBox.Height - (mas[i] * factor)) - (chart.MinIndent + 1));
                }
                #endregion

                #region Drawning
                Pen pen = new Pen(colors[colorID]);
                pen.Width = 2;
                colorID++;

                using (var graphics = Graphics.FromImage(image))
                {
                    graphics.DrawLines(pen, Points);
                }
                #endregion
            }

            chart.PictureBox.Image = image;
        }

        void IDrawer.DrawGread()
        {
            chart.Table = new Bitmap(chart.PictureBox.Width, chart.PictureBox.Height);

            Color GridColor = Color.FromArgb(130, 130, 130);

            ushort HorisontalLiens = (ushort)((chart.MaxValue - chart.MinValue) / chart.GreadVolumeStap);

            float VerticalLinesStep = (float)(chart.PictureBox.Width - (chart.MinIndent * 2 + 1)) / (chart.NumberOfPoles - 1);
            float HorisontallLinesStep = (float)(chart.PictureBox.Height - (chart.MinIndent * 2 + 1)) / HorisontalLiens;

            using (var graphics = Graphics.FromImage(chart.Table))
            {
                for (int i = 0; i < chart.NumberOfPoles; i++)
                {
                    graphics.DrawLine(
                        new Pen(GridColor),
                        new Point((int)(i * VerticalLinesStep + chart.MinIndent), chart.MinIndent),
                        new Point((int)(i * VerticalLinesStep + chart.MinIndent), chart.PictureBox.Height - (chart.MinIndent + 1))
                        );

                    chart.PolesPositions[i] = (ushort)(i * VerticalLinesStep + chart.IndentX);
                }

                for (int i = 0; i <= HorisontalLiens; i++)
                {
                    graphics.DrawLine(
                        new Pen(GridColor),
                        new Point(chart.MinIndent, (int)(i * HorisontallLinesStep + chart.MinIndent)),
                        new Point(chart.PictureBox.Width - (chart.MinIndent + 1), (int)(i * HorisontallLinesStep + chart.MinIndent)));
                }
            }
        }
    }
}
