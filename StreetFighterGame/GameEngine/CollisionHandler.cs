using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace StreetFighterGame.GameEngine
{
    static class CollisionHandler
    {
        public static void KiemTra2ThangDanhNhau(Character Player1, Character Player2)
        {
            int lech1 = (Player1.IsFacingLeft) ? -2 : 2;
            int lech2 = (Player2.IsFacingLeft) ? -2 : 2;
            if (Player1.DangDanhDungKo() && KhoangCachX(Player1.PositionX, Player2.PositionX) < 250f)
            {
                Player1.PlayHitSound();
                Player2.TruMau(Player1.Dame);
                Player1.PositionX = Math.Min(Player1.PositionX + lech1, 900);
                Player2.PositionX = Math.Min(Player2.PositionX + lech1, 900);
                Player2.XuLiKhiBiDanh();
            }
            if (Player2.DangDanhDungKo() && KhoangCachX(Player2.PositionX, Player1.PositionX) < 250f)
            {
                Player1.TruMau(Player2.Dame);
                Player1.PositionX = Math.Min(Player1.PositionX + lech2, 900);
                Player2.PositionX = Math.Min(Player2.PositionX + lech2, 900);
                Player1.XuLiKhiBiDanh();
            }
        }
        public static float KhoangCachX(int pl1, int pl2)
        {
            return Math.Abs(Math.Abs(pl1) - Math.Abs(pl2));
        }
    }
}

