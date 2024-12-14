using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing;

namespace StreetFighterGame.GameEngine
{
    internal class Hitbox
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool isActive { get; private set; }
        public int Lifetime { get; private set; }
        public List<Image> HitboxImage { get; private set; }

        private Timer LifetimeTimer;
        public Hitbox(int positionX, int positionY, int lifetime, List<Image> hitboxImage)
        {
            PositionX = positionX;
            PositionY = positionY;
            Lifetime = lifetime;
            isActive = true;
            HitboxImage = hitboxImage;

            LifetimeTimer = new Timer { Interval = lifetime };
            LifetimeTimer.Tick += (sender, e) => { isActive = false; LifetimeTimer.Stop(); };
            LifetimeTimer.Start();
            
        }
        public void Draw(Graphics g, bool isCircle = false)
        {
            if (!isActive) return;

            if (isCircle)
            {
                g.FillEllipse(Brushes.Red, PositionX, PositionY, Width, Height);
            }
            else
            {
                using (SolidBrush redBrush = new SolidBrush(Color.Red))
                {
                    g.FillRectangle(redBrush, PositionX, PositionY, Width, Height); // Tô đầy hitbox
                }
                /*g.FillRectangle(Brushes.Red, PositionX, PositionY, Width, Height);*/
            }
        }
        
    }
}
