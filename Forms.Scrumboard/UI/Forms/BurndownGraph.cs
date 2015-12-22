using ScrumBoard.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScrumBoard.UI.Forms
{
    public partial class BurndownGraph : Form
    {
        public BurndownGraph()
        {
            InitializeComponent();
            BackColor = Color.FromArgb(Config.DefaultBoardBackColor);
            burndown1.DrawChart();
        }

    }
}
