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
        public enum ChartStile { Line }
        [Description("Type of visual"), Category("Chart")]
        public ChartStile chartStyle { get; set; } = ChartStile.Line;

        [Category("Chart"), DefaultValue(10)]
        public short NumberOfPoles { get; set; } = 10;

        [Category("Chart")]
        public int MaxValue { get; set; } = 100;

        [Category("Chart")]
        public int MinValue { get; set; } = 0;

        [Category("Chart")]
        public float GreadVolumeStap { get; set; } = 5;

        [Category("Chart")]
        public bool Ignore0 { get; set; } = false;

        [Category("Chart")]
        public bool AdaptiveUp;

        [Category("Chart")]
        public bool AdaptiveDown;
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
