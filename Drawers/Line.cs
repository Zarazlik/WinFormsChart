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
        bool drawPoints;

        public Line(Chart chart, bool drawPoints)
        {
            this.chart = chart;
            this.drawPoints = drawPoints;
        }

        void IDrawer.Update(float[][] Values, Color[] colors)
        {
            Image image = chart.Table;
            sbyte colorID = 0;

            foreach (float[] mas in Values)
            {
                #region Point calculation
                List<Point> Points = new List<Point>();

                float factor = (chart.PictureBox.Height - (chart.MinIndent * 2 + 1)) / (chart.MaxValue - chart.MinValue);

                for (int i = 0; i < mas.Length; i++)
                {
                    if (chart.Ignore0)
                    {
                        if (mas[i] != 0)
                        {
                            AddPoint(i);
                        }
                    }
                    else
                    {
                        AddPoint(i);
                    }
                }

                void AddPoint(int i)
                {
                    Points.Add(new Point(chart.PolesPositions[i] + chart.MinIndent, 
                        (int)(chart.PictureBox.Height - (mas[i] * factor - (chart.MinValue * factor))) - (chart.MinIndent + 1)));
                }
                #endregion

                #region Drawning
                Pen pen = new Pen(colors[colorID]);
                pen.Width = chart.LineBoldnes;
                colorID++;

                using (var graphics = Graphics.FromImage(image))
                {
                    graphics.DrawLines(pen, Points.ToArray());
                    if(drawPoints)
                    {
                        foreach (var point in Points)
                        {
                            float PointWidth = 1.5f;
                            graphics.FillEllipse(new SolidBrush(pen.Color), 
                                point.X - pen.Width * PointWidth, point.Y - pen.Width * PointWidth,
                                pen.Width * (PointWidth * 2), pen.Width * (PointWidth * 2));
                        }
                    }
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

            float VerticalLinesStep = (float)(chart.PictureBox.Width - (chart.MinIndent * 2 + 1)) / (chart.AmountOfPoles - 1);
            float HorisontallLinesStep = (float)(chart.PictureBox.Height - (chart.MinIndent * 2 + 1)) / HorisontalLiens;

            using (var graphics = Graphics.FromImage(chart.Table))
            {
                for (int i = 0; i < chart.AmountOfPoles; i++)
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
