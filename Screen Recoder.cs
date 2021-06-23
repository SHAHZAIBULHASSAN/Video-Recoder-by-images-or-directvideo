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
using VisioForge.Types.OutputFormat;

namespace screenrec
{
    public partial class Form1 : Form
    {
        bool folderSelected = false;
        string outputPath = "";
        string finalVideName = "FinalVideo.mp4";
        ScreenRecorder screenRec = new ScreenRecorder(new Rectangle(), "");
        public Form1()
        {
            InitializeComponent();
        }

        private void folderSelected_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Please Select an Output ";
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                outputPath = folderBrowserDialog.SelectedPath;
                folderSelected = true;
                Rectangle bounds = Screen.FromControl(this).Bounds;

                screenRec = new ScreenRecorder(bounds, outputPath);
            }
            else
            {
                MessageBox.Show("Please Select the folder ", "Error for folder");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderSelected)
            {
                tmRecord.Start();

            }
            else
            {
                MessageBox.Show("you must select an output before recording", "Error");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tmRecord.Stop();
            ScreenRecorder.vFWriter.Close();
            screenRec.Stop();
            Application.Restart();


        }

        private void tmRecord_Tick(object sender, EventArgs e)
        {
            screenRec.RecordAudio();

            screenRec.RecordVideo();

            lblTime.Text = screenRec.getElapsed();
        }

        private void lblTime_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Creating a new Bitmap object
            Bitmap captureBitmap = new Bitmap(1024, 768, PixelFormat.Format32bppArgb);

            //Creating a Rectangle object which will capture our Current Screen
            Rectangle captureRectangle = Screen.AllScreens[0].Bounds;

            //Creating a New Graphics Object
            Graphics captureGraphics = Graphics.FromImage(captureBitmap);

            //Copying Image from The Screen
            captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);

            //Saving the Image File (I am here Saving it in My D drive).
            captureBitmap.Save(@"C:\Capture.jpg", ImageFormat.Jpeg);

            //Displaying the Successfull Result

            MessageBox.Show("Screen Captured");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            
        }

    }
    }
    

