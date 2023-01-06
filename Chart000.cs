using System.ComponentModel;
using WinFormsChart.Drawers;

namespace WinFormsChart
{
    public partial class Chart000 : UserControl
    {
        #region
        [Description("Test text displayed in the textbox"), Category("Chart")]
        public byte ChartStile { get; set; }
        public short NumberOfPoles { get; set; }
        public int MaxValue { get; set; }
        public int MinValue { get; set; }
        public float GreadVolumeStap { get; set; }
        #endregion

        public PictureBox PictureBox;
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


        public Chart000(/*PictureBox pictureBox, short NumberOfPoles, int MinValue, int MaxValue, float GreadVolumeStap*/)
        {
            InitializeComponent();

            PictureBox = new PictureBox();
            PolesPositions = new ushort[NumberOfPoles];

            DrawGread();
            PictureBox.Image = Table;
        }


        public void Update(float [] Values, Color color)
        {
            Drawer.Update(this, new float[1][] { Values }, new Color[1] {color});
        }
        public void Update(float[][] Values)
        {
            Drawer.Update(this, Values, Palette);
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

        
    }
}