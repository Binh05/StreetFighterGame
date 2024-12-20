using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StreetFighterGame.GameEngine
{
    public class Hitbox
    {
        public Control renderControl { get; set; }
        public List<Image> Images { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public float ScaleX { get; set; }
        public float ScaleY { get; set; }
        private Timer renderTimer { get; set; }

        private int currentFrame;
        public Hitbox(Control control, List<Image> images, int positionX, int positionY, float scaleX, float scaleY, int interval)
        {
            renderControl = control;
            Images = images;
            PositionX = positionX;
            PositionY = positionY;
            ScaleX = scaleX;
            ScaleY = scaleY;
            currentFrame = 0;

            renderTimer = new Timer { Interval = interval };
            renderTimer.Tick += OnTick;

            renderControl.Paint += OnPaint;
        }
        public void StartDraw()
        {
            currentFrame = 0;
            renderTimer.Start();
        }
        private void OnPaint(object sender, PaintEventArgs e)
        {
            if (Images.Count == 0 || currentFrame >= Images.Count) return;

            // Vẽ hình ảnh hiện tại
            var image = Images[currentFrame];
            e.Graphics.DrawImage(image, PositionX, PositionY, (int)(image.Width * ScaleX), (int)(image.Height * ScaleY));
        }
        private void OnTick(object sender, EventArgs e)
        {
            if (Images.Count == 0 || renderControl == null) return;

            // Yêu cầu vẽ lại control
            renderControl.Invalidate(new Rectangle(PositionX, PositionY, Images[currentFrame].Width, Images[currentFrame].Height));

            if (currentFrame == Images.Count - 1)
            {
                renderTimer.Stop();
                renderControl.Paint -= OnPaint;
            }
            // Chuyển sang frame tiếp theo
            currentFrame = (currentFrame + 1) % Images.Count;
        }
    }
}
