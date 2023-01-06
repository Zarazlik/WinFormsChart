﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsChart.Drawers
{
    internal interface IDrawer
    {
        void Update(Chart000 Chart, float[][] Values, Color[] Colors);
    }
}
