using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsChart.Drawers
{
    internal class Line : IDrawer
    {
        void IDrawer.Update(Chart000 chart, float[][] Values, Color[] colors)
        {
            sbyte colorID = -1;
            foreach (float[] mas in Values)
            {
                colorID++;
                foreach (float value in mas)
                {
                    Point[] Points = new Point[chart.NumberOfPoles];

                    double factor = (double)(chart.PictureBox.Height - (chart.MinIndent * 2 + 1)) / (chart.MaxValue - chart.MinValue);

                    for (int i = 0; i < Values.Length; i++)
                    {
                        Points[i] = new Point(chart.PolesPositions[i] + chart.MinIndent, (int)(chart.PictureBox.Height - (mas[i] * factor)) - (chart.MinIndent + 1));
                    }

                    Image image = DrawChartLine(colors[colorID], Points);
                    chart.PictureBox.Image = image;
                }
            }

            Image DrawChartLine(Color color, Point[] points)
            {
                Image image = chart.Table.Clone() as Image;
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
}
