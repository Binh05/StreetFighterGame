using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Drawing;

namespace StreetFighterGame.GameEngine
{
    static class CollisionHandler
    {
        public static void KiemTra2ThangDanhNhau(Character Player1, Character Player2, Rectangle r1, Rectangle r2)
        {
            int lech1 = (Player1.IsFacingLeft) ? -2 : 2;
            if (Player1.DangDanhDungKo() && r1.IntersectsWith(r2))
            {
                Player1.PlayHitSound();
                Player2.TruMau(Player1.Dame);
                Player2.PositionX = Math.Min(Player2.PositionX + lech1, 900);
                Player2.XuLiKhiBiDanh();
            }
        }
    }
}

