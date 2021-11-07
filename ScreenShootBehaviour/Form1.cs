using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading;
using System.IO;

namespace ScreenShootBehaviour
{
    public partial class Form1 : Form
    {
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(System.Int32 vKey);

        private string _folderName;

        public Form1()
        {
            InitializeComponent();

            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    if (GetAsyncKeyState((int)Keys.PrintScreen) != 0)
                    {
                        Image file = Clipboard.GetImage();

                        if(file != null)
                            Save(file);
                    } 
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            var folderBrowser = new FolderBrowserDialog();

            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                _folderName = folderBrowser.SelectedPath;
                System.Environment.CurrentDirectory = _folderName;
                label2.Text = _folderName;  
            }
        }
        private void Save(Image file)
        {
            string fileName = Directory.GetFiles(_folderName).Length + ".png";
            file.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
            file.Dispose();
        }

    }
}
