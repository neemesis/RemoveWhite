using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoveWhite {
    public partial class Form1 : Form {

        Bitmap img;
        bool imageOpen = false;

        public Form1() {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e) {
            if (openFile.ShowDialog() == DialogResult.OK) {
                img = (Bitmap) Image.FromStream(openFile.OpenFile());
                lblOpen.Text = openFile.FileName.Substring(openFile.FileName.LastIndexOf('\\') + 1);
                imageOpen = true;
                Debug.WriteLine("Succ");
                Debug.WriteLine(openFile.FileName);

                txt1.Text = "200";
                txt2.Text = "200";
                txt3.Text = "200";
            }
            Debug.WriteLine(Color.Transparent);
        }

        private void btnProcess_Click(object sender, EventArgs e) {
            if (!Validator())
                return;

            int p1;
            int.TryParse(txt1.Text, out p1);
            int p2;
            int.TryParse(txt2.Text, out p2);
            int p3;
            int.TryParse(txt3.Text, out p3);

            for (int i = 0; i < img.Width; ++i) {
                for (int j = 0; j < img.Height; ++j) {
                    Color c = img.GetPixel(i, j);
                    if (c.R > p1 && c.G > p2 && c.B > p3)
                        img.SetPixel(i, j, Color.Transparent);
                }
            }
            Debug.WriteLine("Done");

            saveFile.Filter = "Images|*.png";
            if (saveFile.ShowDialog() == DialogResult.OK) {
                img.MakeTransparent(Color.White);
                img.Save(saveFile.FileName);
            }
        }

        private void txt_Validating(object sender, CancelEventArgs e) {
            e.Cancel = Validator();
        }

        private bool Validator() {
            int p1 = -1;
            int.TryParse(txt1.Text, out p1);
            int p2 = -1;
            int.TryParse(txt2.Text, out p2);
            int p3 = -1;
            int.TryParse(txt3.Text, out p3);

            if (p1 < 0 || p2 < 0 || p3 < 0 || !imageOpen) {
                return false;
            } else
                return true;
        }
    }
}
