using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Drawing;
using System.Windows.Forms;

namespace StreetFighterGame.GameEngine
{
    static class CollisionHandler
    {
        public static bool KiemTra2ThangDanhNhau(Character Player1, Character Player2, Rectangle r1, Rectangle r2, AnimationManager animationManager, Control control)
        {
            int lech1 = (Player1.IsFacingLeft) ? -2 : 2;
            int lechX = (Player2.IsFacingLeft) ? (int)(Player2.charWidth * 1.5f) : 0;
            if (Player1.DangDanhDungKo() && Colliding(r1, r2))
            {
                Player1.PlayHitSound();
                //Console.WriteLine(Player2.PositionX - lechX);
                animationManager.DrawMele(control, Player2.PositionX - lechX, Player2.PositionY - Player2.charHeight / 3);
                Player2.TruMau(Player1.Dame);
                Player2.PositionX = Math.Min(Player2.PositionX + lech1, 900);
                Player2.XuLiKhiBiDanh();
                return true;
            }
            return false;
        }
        public static bool Colliding(Rectangle r1, Rectangle r2)
        {
            return r1.IntersectsWith(r2);
        }
    }
}

