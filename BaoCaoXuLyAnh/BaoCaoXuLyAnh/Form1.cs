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
using Emgu.CV.Structure;
using System.Drawing.Imaging;

namespace BaoCaoXuLyAnh
{
    public partial class Form1 : Form
    {
        Image image;
        Bitmap newImage;
        Bitmap backImage;
        Bitmap b1;
        Bitmap b2;
        Bitmap b3;
        Bitmap b4;
        Bitmap b5;
        Rectangle rect;
        Point locationXY;
        Point locationX1Y1;

        OpenFileDialog openFile = new OpenFileDialog();
        SaveFileDialog saveFile = new SaveFileDialog();
        static readonly CascadeClassifier cascade = new CascadeClassifier("haarcascade_frontalface_default.xml");
        bool IsMouseDown = false;// this variable is true when mouse event is occuted

        public Form1()
        {
            InitializeComponent();
        }

        private void btnOp_Click(object sender, EventArgs e)
        {
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                image = Image.FromFile(openFile.FileName);
                newImage = new Bitmap(image);
                pictureBox1.Image = newImage;
            }
        }

        private Rectangle GetRect()
        {

            rect = new Rectangle();

            rect.X = Math.Min(locationXY.X, locationX1Y1.X);

            rect.Y = Math.Min(locationXY.Y, locationX1Y1.Y);

            rect.Width = Math.Abs(locationXY.X - locationX1Y1.X);

            rect.Height = Math.Abs(locationXY.Y - locationX1Y1.Y);

            return rect;
        }

        private void btnSe_Click(object sender, EventArgs e)
        {
            try
            {
                image = Image.FromFile(openFile.FileName);
                backImage = new Bitmap(image);
                pictureBox2.Image = backImage;
            }
            catch
            {
                MessageBox.Show("Chưa chọn ảnh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnNe_Click(object sender, EventArgs e)
        {
            if (pictureBox3.Image == null)
            {
                b1 = new Bitmap(pictureBox1.Image);
                pictureBox1.Image = null;
                pictureBox3.Image = pictureBox2.Image;
                pictureBox2.Image = null;
            }
            else if (pictureBox4.Image == null)
            {
                b2 = new Bitmap(pictureBox1.Image);
                pictureBox1.Image = null;
                pictureBox4.Image = pictureBox2.Image;
                pictureBox2.Image = null;
            }
            else if (pictureBox5.Image == null)
            {
                b3 = new Bitmap(pictureBox1.Image);
                pictureBox1.Image = null;
                pictureBox5.Image = pictureBox2.Image;
                pictureBox2.Image = null;
            }
            else if (pictureBox6.Image == null)
            {
                b4 = new Bitmap(pictureBox1.Image);
                pictureBox1.Image = null;
                pictureBox6.Image = pictureBox2.Image;
                pictureBox2.Image = null;
            }
            else if (pictureBox7.Image == null)
            {
                b5 = new Bitmap(pictureBox1.Image);
                pictureBox1.Image = null;
                pictureBox7.Image = pictureBox2.Image;
                pictureBox2.Image = null;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = pictureBox3.Image;
            pictureBox1.Image = b1;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = pictureBox4.Image;
            pictureBox1.Image = b2;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = pictureBox5.Image;
            pictureBox1.Image = b3;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = pictureBox6.Image;
            pictureBox1.Image = b4;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = pictureBox7.Image;
            pictureBox1.Image = b5;
        }

        private void btnSa_Click(object sender, EventArgs e)
        {
            saveFile.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFile.ShowDialog();
            if (saveFile.ShowDialog() == DialogResult.OK && pictureBox3.Image != null)
            {
                DialogResult h = MessageBox.Show("Bạn có muốn lưu ảnh", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (h == DialogResult.Cancel)
                {
                    Application.Exit();
                }
                pictureBox3.Image.Save(saveFile.FileName);
            }
            if (saveFile.ShowDialog() == DialogResult.OK && pictureBox4.Image != null)
            {
                DialogResult h = MessageBox.Show("Bạn có muốn lưu ảnh", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (h == DialogResult.Cancel)
                {
                    Application.Exit();
                }
                pictureBox4.Image.Save(saveFile.FileName);
            }
            if (saveFile.ShowDialog() == DialogResult.OK && pictureBox5.Image != null)
            {
                DialogResult h = MessageBox.Show("Bạn có muốn lưu ảnh", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (h == DialogResult.Cancel)
                {
                    Application.Exit();
                }
                pictureBox5.Image.Save(saveFile.FileName);
            }
            if (saveFile.ShowDialog() == DialogResult.OK && pictureBox6.Image != null)
            {
                DialogResult h = MessageBox.Show("Bạn có muốn lưu ảnh", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (h == DialogResult.Cancel)
                {
                    Application.Exit();
                }
                pictureBox6.Image.Save(saveFile.FileName);
            }
            if (saveFile.ShowDialog() == DialogResult.OK && pictureBox7.Image != null)
            {
                DialogResult h = MessageBox.Show("Bạn có muốn lưu ảnh", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (h == DialogResult.Cancel)
                {
                    Application.Exit();
                }
                pictureBox7.Image.Save(saveFile.FileName);
            }
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (checkBox1.Checked)
            {
                IsMouseDown = true; // if this event is occured so this variable is true
                locationXY = e.Location;//Get the starting location of point x and y.
            }
            else
            {
                Rectangle re = new Rectangle(e.Location.X, e.Location.Y, 50, 50);
                Bitmap bitmap = new Bitmap(pictureBox2.Image);
                using (Graphics graphics = Graphics.FromImage(pictureBox2.Image))
                {
                    using (Pen pen = new Pen(Color.Red, 1))
                    {
                        graphics.DrawRectangle(pen, re);
                        backImage = bitmap;
                        pictureBox2.Image = Blur(backImage, re, 10);
                    }
                }
            }
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDown == true)//this block is not execute untill mouse event is not a fire
            {
                locationX1Y1 = e.Location;//get the current location of point x and y.

                Refresh();//Refresh the form.
            }
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsMouseDown == true)//this block is not execute untill mouse event is not a fire
            {
                locationX1Y1 = e.Location;//get the ending point of x and y.

                IsMouseDown = false;//false this...
            }
            if (rect != null)
            {
                backImage = newImage;
                pictureBox2.Image = Blur(getPictureBoxImage(), rect, 10);
                newImage = (Bitmap)pictureBox2.Image;
            }
        }

        private Bitmap getPictureBoxImage()
        {
            Bitmap bmp = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(pictureBox2.Image,
                    new Rectangle(0, 0, bmp.Width, bmp.Height));
            }
            return bmp;
        }

        private System.Drawing.Image Blur(System.Drawing.Image image, Rectangle rectangle, Int32 blurSize)
        {

            Bitmap blurred = new Bitmap(image);   //image.Width, image.Height);
            using (Graphics graphics = Graphics.FromImage(blurred))
            {
                try
                {
                    // look at every pixel in the blur rectangle
                    for (Int32 xx = rectangle.Left; xx < rectangle.Right; xx += blurSize)
                    {
                        for (Int32 yy = rectangle.Top; yy < rectangle.Bottom; yy += blurSize)
                        {
                            Int32 avgR = 0, avgG = 0, avgB = 0;
                            Int32 blurPixelCount = 0;
                            Int32 opacity = Int32.Parse(textBox1.Text);
                            
                            Rectangle currentRect = new Rectangle(xx, yy, blurSize, blurSize);

                            // average the color of the red, green and blue for each pixel in the
                            // blur size while making sure you don't go outside the image bounds
                            for (Int32 x = currentRect.Left; (x < currentRect.Right && x < image.Width); x++)
                            {
                                for (Int32 y = currentRect.Top; (y < currentRect.Bottom && y < image.Height); y++)
                                {

                                    Color pixel = blurred.GetPixel(x, y);

                                    avgR += pixel.R;
                                    avgG += pixel.G;
                                    avgB += pixel.B;

                                    blurPixelCount++;
                                }
                            }

                            avgR = avgR / blurPixelCount + opacity;
                            avgG = avgG / blurPixelCount + opacity;
                            avgB = avgB / blurPixelCount + opacity;
                            //MessageBox.Show(Convert.ToString(blurPixelCount), Convert.ToString(avgR));
                            // now that we know the average for the blur size, set each pixel to that color
                            graphics.FillRectangle(new SolidBrush(Color.FromArgb(avgR, avgG, avgB)), currentRect);
                        }
                    }
                    graphics.Flush();
                }
                catch
                {
                    MessageBox.Show("Vui lòng không kéo ra ngoài !!!", "Thông Báo");

                }

            }
            return blurred;
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            if (rect != null)
            {
                //Ve Hinh Chu Nhat
                e.Graphics.DrawRectangle(Pens.Red, GetRect());
            }
        }

        private void btnRe_Click(object sender, EventArgs e)
        {
            
            if(pictureBox2.Image == pictureBox3.Image)
            {
                pictureBox3.Image = null;
                pictureBox2.Image = null;
            }
            if (pictureBox2.Image == pictureBox4.Image)
            {
                pictureBox4.Image = null;
                pictureBox2.Image = null;
            }
            if (pictureBox2.Image == pictureBox5.Image)
            {
                pictureBox5.Image = null;
                pictureBox2.Image = null;
            }
            if (pictureBox2.Image == pictureBox6.Image)
            {
                pictureBox6.Image = null;
                pictureBox2.Image = null;
            }
            if (pictureBox2.Image == pictureBox7.Image)
            {
                pictureBox7.Image = null;
                pictureBox2.Image = null;
            }
        }

        private void btnAutoFace_Click(object sender, EventArgs e)
        {
            pictureBox2.Image =pictureBox1.Image;
            Bitmap bitmap = new Bitmap(pictureBox2.Image);
            Image<Gray, byte> grayimage = bitmap.ToImage<Gray, byte>();
            Rectangle[] rectangles = cascade.DetectMultiScale(grayimage, 1.4, 0);
            Rectangle rectangle;
            for (int i = 0; i < 1; i++)
            {
                rectangle = rectangles[i];
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    using (Pen pen = new Pen(Color.Red, 1))
                    {
                        graphics.DrawRectangle(pen, rectangle);
                        backImage = bitmap;
                        pictureBox2.Image = Blur(backImage, rectangle, 10);
                    }
                }
            }
            newImage = (Bitmap)pictureBox2.Image;
        }
    }

}
