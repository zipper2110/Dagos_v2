using Library.Dagos.Project;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dagos.Import
{
    public partial class ImportImagesFolder : Form
    {
        public delegate void ImportImage(DagosImage image);
        public ImportImage onImportImage;

        private List<string> importImagesFiles;

        private DagosImage outputImage;

        public ImportImagesFolder()
        {
            InitializeComponent();
        }

        private void buttonSelectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog imageFolderSelect = new FolderBrowserDialog();
            string status = "";
            bool canImport = false;

            if (imageFolderSelect.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string folder = imageFolderSelect.SelectedPath;

                if (!Directory.Exists(folder))
                {
                    status = "Directory you specified does not exist";
                }
                else
                {
                    string[] files = Directory.GetFiles(folder);
                    List<string> filteredImageFiles = new List<string>();

                    foreach (string file in files)
                    {
                        if (file.ToLower().EndsWith(".png") || file.ToLower().EndsWith(".bmp"))
                        {
                            filteredImageFiles.Add(file);
                        }
                    }

                    if (filteredImageFiles.Count == 0)
                    {
                        status = "No images found in specified folder";
                    }
                    else
                    {
                        using (Image templateImage = Image.FromFile(filteredImageFiles.ElementAt(0)))
                        {
                            List<string> images = new List<string>();
                            progressBarImport.Maximum = filteredImageFiles.Count;

                            Image image;
                            foreach (string imageFile in filteredImageFiles)
                            {
                                using (image = Image.FromFile(imageFile))
                                {
                                    if (image != null && image.Width == templateImage.Width && image.Height == templateImage.Height)
                                    {
                                        images.Add(imageFile);
                                    }
                                }
                                progressBarImport.Increment(1);
                            }

                            importImagesFiles = images;
                        }

                        status = "Images found: " + importImagesFiles.Count;
                        canImport = true;
                    }
                }
            }

            labelFolderInfo.Text = status;
            buttonImport.Enabled = canImport;
        }

        public DagosImage importImages(List<string> imageFiles)
        {
            if (imageFiles == null || imageFiles.Count == 0)
            {
                MessageBox.Show("No images selected. Can't start images import");
                return null;
            }

            byte[,,] imageData;
            using (Image templateImage = Image.FromFile(imageFiles.ElementAt(0)))
            {
                imageData = new byte[templateImage.Width, templateImage.Height, imageFiles.Count];
            }

            BitmapData bitmapData;
            byte[] bitmapDataArray;
            int x, y, z;

            for (z = 0; z < imageFiles.Count; z++) 
            {
                using (Image image = Image.FromFile(imageFiles.ElementAt(z)))
                {
                    using (Bitmap imageBitmap = new Bitmap(image))
                    {
                        bitmapData = imageBitmap.LockBits(new Rectangle(0, 0, imageBitmap.Width, imageBitmap.Height),
                            System.Drawing.Imaging.ImageLockMode.ReadOnly, imageBitmap.PixelFormat);

                        bitmapDataArray = new byte[bitmapData.Stride * bitmapData.Height];
                        System.Runtime.InteropServices.Marshal.Copy(bitmapData.Scan0, bitmapDataArray, 0, bitmapData.Stride * bitmapData.Height);

                        for (y = 0; y < imageBitmap.Height; y++)
                        {
                            for (x = 0; x < imageBitmap.Width; x++)
                            {
                                imageData[x, y, z] = bitmapDataArray[(x + y * bitmapData.Width) * 3];
                            }
                        }
                    }
                }

                if (backgroundWorker1.CancellationPending)
                {
                    return null;
                }
                backgroundWorker1.ReportProgress((int)((float) z / (float)imageFiles.Count * 100));
            }

            return new DagosImage(imageData);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (onImportImage != null)
            {
                progressBarImport.Value = 0;
                progressBarImport.Maximum = 100;
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
            this.Close();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            outputImage = null;
            outputImage = importImages(importImagesFiles);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (onImportImage != null)
            {
                this.Close();
                onImportImage(outputImage);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarImport.Value = e.ProgressPercentage;
        }
    }
}
