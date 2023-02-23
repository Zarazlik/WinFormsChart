using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsChart.Drawers
{
    internal interface IDrawer
    {
        Chart chart { set; get; }

        void Update(float[][] Values, Color[] Colors);

        void DrawGread();

        bool CheckChartSize()
        {
            if (chart.Size.Width > 0 && chart.Height > 0) 
            { 
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
