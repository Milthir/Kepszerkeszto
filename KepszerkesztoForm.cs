using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kepszerkeszto
{
    public partial class KepszerkesztoForm : Form
    {
        Bitmap im;
        public Bitmap Im
        {
            get { return im; }
            set { im = value; }
        }
        

        public KepszerkesztoForm()
        {
            InitializeComponent();
        }
        public KepszerkesztoForm(Bitmap im)
        {
            InitializeComponent();
            this.Resize(im);
        }
        public void Resize(Bitmap im)
        {
            this.im = im;
            if (im != null)
            {
                ClientSize = new Size(im.Width, im.Height);
            }
            this.Invalidate();
        }

        private void KepszerkesztoForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            g.DrawImage(im, new Point(0, 0));
        }
    }
}
