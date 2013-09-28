using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Kepszerkeszto
{
    public partial class Form1 : Form
    {
        KepszerkesztoForm mdiChild = null;
        Stopwatch s = new Stopwatch();
        Bitmap bmap = null;

        public Form1()
        {
            InitializeComponent();
        }

        //Open image
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog oFD = new OpenFileDialog();

            oFD.InitialDirectory = "c:\\";
            oFD.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            oFD.FilterIndex = 2;
            oFD.RestoreDirectory = true;
            
            if (oFD.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = oFD.OpenFile()) != null)
                    {
                        s.Start();
                        using (myStream)
                        {
                            bmap = new Bitmap(oFD.FileName);
                            mdiChild = new KepszerkesztoForm(bmap);
                            mdiChild.MdiParent = this;
                            mdiChild.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoving);
                            mdiChild.Show();
                        }
                        s.Stop();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void MouseMoving(object sender, MouseEventArgs e)
        {
            if (mdiChild != null)
            {
                Bitmap k = ((KepszerkesztoForm)sender).Im;
                footLabel.Text = "X: " + e.X.ToString() + " Y: " + e.Y.ToString();
                if (e.X < k.Width && e.Y <= k.Height)
                {
                    Color szin = k.GetPixel(e.X, e.Y);
                    footLabel.Text += " R: " + szin.R + " G: " + szin.G + " B: " + szin.B;
                }
                footLabel.Text += " Load time: " + s.Elapsed.Milliseconds + " ms";
            }
        }

        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mdiChild != null && bmap != null)
            {
               Worker.Invert(bmap);
               mdiChild.Invalidate();
            }
        }

        private void grayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mdiChild != null && bmap != null)
            {
                Worker.GrayScale(bmap);
                mdiChild.Invalidate();
            }
        }
    }
}
