using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScrumBoard.UI.Controls
{
    public partial class Mover : UserControl
    {
        private Point loc;

        public Mover()
        {
            InitializeComponent();
        }

        public void SetMoverLocation(Point p)
        {
            loc = p;
            this.Location = loc;
        }

         


        protected override void OnLocationChanged(EventArgs e)
        {
            if (Location.X != loc.X || Location.Y != loc.Y)
            {
                Console.WriteLine(" Override location");
                this.Location = loc;
            }
            else
            {
               
                base.OnLocationChanged(e);
            }
          //  Console.WriteLine("  X: " + this.Location.X + ", Y: " + Location.Y);
            
        }
    }
}
