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
            if (Player1.DangDanhDungKo() && KhoangCachX(Player1.PositionX, Player2.PositionX) < 150f)
            {
                Player2.TruMau(Player1.Dame);
                Player1.PositionX = Math.Min(Player1.PositionX + 2, 900);
                Player2.PositionX = Math.Min(Player2.PositionX + 2, 900);
                Player2.XuLiKhiBiDanh();
            }
            if (Player2.DangDanhDungKo() && KhoangCachX(Player2.PositionX, Player1.PositionX) < 150f)
            {
                Player1.TruMau(Player2.Dame);
                Player1.PositionX = Math.Min(Player1.PositionX + 2, 900);
                Player1.XuLiKhiBiDanh();
            }
        }
        public static float KhoangCachX(int pl1, int pl2)
        {
            return Math.Abs(Math.Abs(pl1) - Math.Abs(pl2));
        }
    }
}

