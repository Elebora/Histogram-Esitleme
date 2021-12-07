using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Histogram_Esitleme
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap girdi = new Bitmap(pictureBox1.Image);
            int[] r = new int[256];
            int[] g = new int[256];
            int[] b = new int[256];
            for (int i = 0; i < 256; i++)
            {
                r[i] = g[i] = b[i] = 0;
            }
            for (int x = 0; x < girdi.Width; x++)
            {
                for (int y = 0; y < girdi.Height; y++)
                {
                    r[girdi.GetPixel(x, y).R]++;
                    g[girdi.GetPixel(x, y).G]++;
                    b[girdi.GetPixel(x, y).B]++;
                }
            }
            int[] rKümülatif = new int[256];
            int[] gKümülatif = new int[256];
            int[] bKümülatif = new int[256];

            rKümülatif = r; gKümülatif = g; bKümülatif = b;

            for (int i = 1; i < 256; i++)
            {
                rKümülatif[i] = rKümülatif[i] + rKümülatif[i - 1];
                gKümülatif[i] = gKümülatif[i] + gKümülatif[i - 1];
                bKümülatif[i] = bKümülatif[i] + bKümülatif[i - 1];
            }
            double sabit = (double)255 / (girdi.Width * girdi.Height);// (L-1)/toplampiksel L=256
                                                                      //double sabit = 255 / rKümülatif[255];
            for (int i = 0; i < 256; i++)
            {
                rKümülatif[i] = (int)Math.Round(rKümülatif[i] * sabit);
                gKümülatif[i] = (int)Math.Round(gKümülatif[i] * sabit);
                bKümülatif[i] = (int)Math.Round(bKümülatif[i] * sabit);
            }
            Bitmap cikti = new Bitmap(girdi.Width, girdi.Height);
            int yeniR = 0; int yeniG = 0; int yeniB = 0;
            for (int x = 0; x < cikti.Width; x++)
            {
                for (int y = 0; y < cikti.Height; y++)
                {
                    yeniR = rKümülatif[girdi.GetPixel(x, y).R];
                    yeniG = gKümülatif[girdi.GetPixel(x, y).G];
                    yeniB = bKümülatif[girdi.GetPixel(x, y).B];
                    cikti.SetPixel(x, y, Color.FromArgb(yeniR, yeniG, yeniB));
                }
            }
            pictureBox2.Image = cikti;
        }
    }
}
