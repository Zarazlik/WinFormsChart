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
        #region
        [Category("Chart")]
        [Description("Type of visual")]
        public ChartStile chartStyle { get; set; } = ChartStile.Line;
        public enum ChartStile { Line }

        [Category("Chart")]
        [Description("Number of vertical lines on the x-axis that will display values \r\n" +
            "If the number of values exceeds the number of lines, then some of the values will not be displayed. " +
            "If the number of values is less than the number of lines, the empty values will be 0.")]
        public short NumberOfPoles { get; set; } = 10;

        [Category("Chart")]
        [Description("Number of vertical lines on the y-axis")]
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
        */

        [Category("Chart")]
        //[Description("Line boldness")]
        [DefaultValue(2)]
        public float LineBoldnes { get; set; } = 2;
        #endregion

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



        public Chart()
        {
            InitializeComponent();
        }

        private void Chart_Load(object sender, EventArgs e)
        {
            PolesPositions = new ushort[NumberOfPoles];

            if (chartStyle == ChartStile.Line)
            {
                Drawer = new Line(this);
            }

            Drawer.DrawGread();
            PictureBox.Image = Table;
        }



        public void Update(float[] Values, Color color)
        {
            Drawer.Update(new float[1][] { Values }, new Color[1] { color });
        }
        public void Update(float[][] Values)
        {
            Drawer.Update(Values, Palette);
        }
    }
}
