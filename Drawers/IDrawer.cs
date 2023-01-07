using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsChart.Drawers
{
    internal interface IDrawer
    {
        Chart chart { set; }

        void Update(float[][] Values, Color[] Colors);

        void DrawGread();
    }
}
