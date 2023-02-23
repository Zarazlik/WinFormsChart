using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsChart.Drawers;

namespace WinFormsChart
{
    public partial class Chart : UserControl
    {
        #region Propertyes

        [Category("Chart")]
        [Description("Type of visual")]
        public ChartStile chartStyle { get; set; } = ChartStile.Line;
        public enum ChartStile { Line, LineWithPoints }

        [Category("Chart")]
        [Description("Аmount of vertical lines on the x-axis that will display values \r\n" +
            "If the amount of values exceeds the amount of lines, then some of the values will not be displayed. " +
            "If the amount of values is less than the amount of lines, the empty values will be 0.")]
        public short AmountOfPoles { get; set; } = 10;

        [Category("Chart")]
        [Description("Amount of vertical lines on the y-axis")]
        public float GreadVolumeStap { get; set; } = 5;

        [Category("Chart")]
        [Description("Maximum value displayed by chart")]
        public float MaxValue { get; set; } = 100;

        [Category("Chart")]
        [Description("Minimum value displayed by chart")]
        public float MinValue { get; set; } = 0;

        [Category("Chart")]
        [Description("If TRUE, values equal to 0 will be pass")]
        public bool Ignore0 { get; set; } = false;

        /*
        [Category("Chart")]
        public bool AdaptiveUp;

        [Category("Chart")]
        public bool AdaptiveDown;

        [Category("Chart")]
        public bool RightToLeft;
        */

        [Category("Chart")]
        //[Description("Line boldness")]
        [DefaultValue(2)]
        public float LineBoldnes { get; set; } = 2;
        #endregion

        #region Variables
        public Image Table;

        public byte MinIndent = 3;
        public int IndentX;

        public ushort[] PolesPositions;

        Color[] Palette = new Color[6]
        {
            Color.Red,
            Color.Green,
            Color.Blue,
            Color.Purple,
            Color.Orange,
            Color.LightBlue
        };

        IDrawer Drawer;

        float[][] BackapPoints;
        Color[] BackapColors;

        bool FerstResize = true;

        #endregion

        public Chart()
        {
            InitializeComponent();
        }

        private void Chart_Load(object sender, EventArgs e)
        {
            PolesPositions = new ushort[AmountOfPoles];

            if (chartStyle == ChartStile.Line)
            {
                Drawer = new Line(this, false);
            }
            else if (chartStyle == ChartStile.LineWithPoints)
            {
                Drawer = new Line(this, true);
            }

            Drawer.DrawGread();
            PictureBox.Image = Table;
            FerstResize= false;
        }

        #region Update voids

        public void Update(float[] Values, Color color)
        {
            Drawer.Update(new float[1][] { Values }, new Color[1] { color });

            BackapPoints = new float[1][] { Values };
            BackapColors = new Color[1] { color };
        }
        public void Update(float[][] Values)
        {
            Drawer.Update(Values, Palette);

            BackapPoints = Values;
            BackapColors = Palette;
        }

        #endregion

        private void Chart_Resize(object sender, EventArgs e)
        {
            if (!FerstResize)
            {
                if (PictureBox.Size.Width > 0 && PictureBox.Size.Height > 0)
                {
                    Drawer.DrawGread();
                    Drawer.Update(BackapPoints, BackapColors);
                }
            }
        }
    }
}
