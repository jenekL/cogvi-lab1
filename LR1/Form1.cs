using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Diagnostics;
using Emgu.CV.Util;
using System.IO;
using Emgu.CV.UI;

namespace LR1
{
    public partial class Form1 : Form
    {
        Image<Bgr, byte> imgBgr;
        Image<Gray, Byte> imgGray;
        string[] colorsScheme = new string[] { "RGBA", "HLS", "HSV", "LAB", "LUV", "XYZ", "YCC" };
        Image<Bgr, byte> resizeImage;
        Image<Gray, byte> imgGrayFilter;
        Image<Gray, byte> imgGrayNormalize;
        Image<Gray, byte> imgGrayDetect;
        int alpha = 0;
        int beta = 200;
        int heightKernel = 31;
        int widthKernel = 51;
        double sigma1 = 0.2;
        double sigma2 = 0.3;


        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(colorsScheme);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();


            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imgBgr = new Image<Bgr, byte>(dialog.FileName);
                imgBgr = imgBgr.Resize(200, 200, Inter.Linear);
                imageBox1.Image = imgBgr;
                imgGray = imgBgr.Convert<Gray, Byte>();
                imageBox2.Image = imgGray;
            }
            label1.Visible = true;
            label2.Visible = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedState = comboBox1.SelectedItem.ToString();
            label1.Text = selectedState;
            switch (Array.IndexOf(colorsScheme, selectedState))
            {
                case 0:
                    ConvertIMG(ColorConversion.Bgr2Bgra, imageBox3);
                    break;
                case 1:
                    ConvertIMG(ColorConversion.Bgr2Hls, imageBox3);
                    break;
                case 2:
                    ConvertIMG(ColorConversion.Bgr2Hsv, imageBox3);
                    break;
                case 3:
                    ConvertIMG(ColorConversion.Bgr2Lab, imageBox3);
                    break;
                case 4:
                    ConvertIMG(ColorConversion.Bgr2Luv, imageBox3);
                    break;
                case 5:
                    ConvertIMG(ColorConversion.Bgr2Xyz, imageBox3);
                    break;
                case 6:
                    ConvertIMG(ColorConversion.Bgr2YCrCb, imageBox3);
                    break;

                default:
                    break;
            }
        }

        private UMat ConvertIMG(ColorConversion color, PictureBox outputPictureBox)
        {
            UMat uimage = new UMat();

            if (imgBgr == null)
            {
                CreateDialogMessage("Загрузите изображение!", "Ошибка чтения изображения");
            }
            else
            {
                CvInvoke.CvtColor(imgBgr, uimage, color);
                outputPictureBox.Image = uimage.Bitmap;
                return uimage;
            }
            return null;
        }

        private static void CreateDialogMessage(string message, string caption)
        {

            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;
            result = MessageBox.Show(message, caption, buttons);
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (imgBgr == null)
            {
                CreateDialogMessage("Загрузите изображение!", "Ошибка чтения изображения");

            }
            else
            {
                resizeImage = imgBgr.Resize(200 + e.NewValue, 200 + e.NewValue, Inter.Linear);
                imageBox1.Image = resizeImage;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 F2 = new Form2();
            F2.ShowDialog(); 
            alpha = F2.alpha;
            beta = F2.beta;
            heightKernel = F2.heightKernel;
            widthKernel = F2.widthKernel;
            sigma1 = F2.sigma1;
            sigma2 = F2.sigma2;
        }

        private void button3_Click(object sender, EventArgs e)
        {   
            imgGrayFilter = imgGray.SmoothGaussian(widthKernel, heightKernel, sigma1, sigma2);
            imageBox4.Image = imgGrayFilter;
            label4.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            imgGrayNormalize = new Image<Gray, byte>(imgGray.Bitmap);
            CvInvoke.Normalize(imgGray, imgGrayNormalize, alpha, beta, NormType.MinMax);
            imageBox5.Image = imgGrayNormalize;
            label5.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (imgBgr == null)
            {
                CreateDialogMessage("Загрузите изображение или примените нужные фильтры!", "Ошибка чтения изображения");
            }
            else
            {
                Form dlg = new Form();
                HistogramBox histogramBox = new HistogramBox();
                histogramBox.Location = new Point(10, 10);
                histogramBox.GenerateHistograms(imgBgr, 256);
                histogramBox.Enabled = true;
                histogramBox.Size = new System.Drawing.Size(512, 512);
                histogramBox.Refresh();
                dlg.Controls.Add(histogramBox);
                dlg.Width = 540;
                dlg.Height = 560;
                dlg.ShowDialog();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (imgGray == null)
            {
                CreateDialogMessage("Загрузите изображение или примените нужные фильтры!", "Ошибка чтения изображения");
            }
            else
            {
                Form dlg = new Form();
                HistogramBox histogramBox = new HistogramBox();
                histogramBox.Location = new Point(10, 10);
                histogramBox.GenerateHistograms(imgGray, 256);
                histogramBox.Enabled = true;
                histogramBox.Size = new System.Drawing.Size(512, 512);
                histogramBox.Refresh();
                dlg.Controls.Add(histogramBox);
                dlg.Width = 540;
                dlg.Height = 560;
                dlg.ShowDialog();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (imgGrayNormalize == null)
            {
                CreateDialogMessage("Загрузите изображение или примените нужные фильтры!", "Ошибка чтения изображения");
            }
            else
            {
                Form dlg = new Form();
                HistogramBox histogramBox = new HistogramBox();
                histogramBox.Location = new Point(10, 10);
                histogramBox.GenerateHistograms(imgGrayNormalize, 256);
                histogramBox.Enabled = true;
                histogramBox.Size = new System.Drawing.Size(512, 512);
                histogramBox.Refresh();
                dlg.Controls.Add(histogramBox);
                dlg.Width = 540;
                dlg.Height = 560;
                dlg.ShowDialog();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (imgGrayFilter == null)
            {
                CreateDialogMessage("Загрузите изображение или примените нужные фильтры!", "Ошибка чтения изображения");
            }
            else
            {
                Form dlg = new Form();
                HistogramBox histogramBox = new HistogramBox();
                histogramBox.Location = new Point(10, 10);
                histogramBox.GenerateHistograms(imgGrayFilter, 256);
                histogramBox.Enabled = true;
                histogramBox.Size = new System.Drawing.Size(512, 512);
                histogramBox.Refresh();
                dlg.Controls.Add(histogramBox);
                dlg.Width = 540;
                dlg.Height = 560;
                dlg.ShowDialog();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            
            Mat uimageGray = new Mat();
            Mat finalImg = new Mat();
            CvInvoke.CvtColor(imgBgr, uimageGray, ColorConversion.Bgr2Gray);
            imgGrayDetect = new Image<Gray, byte>(uimageGray.Bitmap);
            var treshold = 120;
            CvInvoke.Threshold(uimageGray, imgGrayDetect, treshold, 255, ThresholdType.Binary);
            CvInvoke.BitwiseNot(imgGrayDetect, finalImg);
            imageBox3.Image = finalImg;
        }
    }
}

