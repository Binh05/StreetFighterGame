using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace StreetFighterGame.GameEngine
{
    internal class AnimationManager
    {
        public List<Image> mele {  get; set; }
        private int currentFrame;
        private int positionXMele, positionYMele;
        //private PaintEventArgs currentGraphics;
        private Control renderControl;

        private Timer meleTimer;

        public AnimationManager()
        {
            mele = LoadImages(".\\Effect", 7, "VaCham_2");
            meleTimer = new Timer { Interval = 32 };
            meleTimer.Tick += OnMeleTimerTick;
        }
        private void OnMeleTimerTick(object sender, EventArgs e)
        {
            if (mele.Count == 0 || renderControl == null) return;

            // Yêu cầu vẽ lại control
            renderControl.Invalidate(new Rectangle(positionXMele, positionYMele, mele[currentFrame].Width, mele[currentFrame].Height));

            if (currentFrame == mele.Count - 1) meleTimer.Stop();
            // Chuyển sang frame tiếp theo
            currentFrame = (currentFrame + 1) % mele.Count;
        }
        public void DrawMele(Control control, int positionX, int positionY)
        {
            renderControl = control;

            // Cập nhật vị trí vẽ
            //Console.WriteLine($"mele:  {positionX}");
            positionXMele = positionX;
            positionYMele = positionY;

            // Bắt đầu Timer
            meleTimer.Start();
        }
        public void DrawImage(Graphics g)
        {
            if (mele.Count > 0)
            {
                // Vẽ frame hiện tại
                g.DrawImage(mele[currentFrame], positionXMele, positionYMele, mele[currentFrame].Width, mele[currentFrame].Height);
            }
        }
        private List<Image> LoadImages(string folderPath, int count, string filePrefix)
        {
            var images = new List<Image>();
            for (int i = 0; i < count; i++)
            {
                string filePath = Path.Combine(folderPath, $"{filePrefix}-{i}.png");
                if (File.Exists(filePath))
                {
                    images.Add(Image.FromFile(filePath));
                }
            }
            return images;
        }
    }
}
