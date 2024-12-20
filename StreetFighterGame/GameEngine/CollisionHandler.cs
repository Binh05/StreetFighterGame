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
            int lechX = (Player2.IsFacingLeft) ? (int)(Player2.charWidth * 2.5f) : Player2.charWidth;
            int lechDefense = (Player2.IsFacingLeft) ? (int)(Player2.charWidth * 1.5f) : 0;
            if (Player1.DangDanhDungKo() && Colliding(r1, r2))
            {
                if (Player2.isDefense)
                {
                    animationManager.DrawDefense(control, Player2.PositionX - lechDefense, Player2.PositionY - Player2.charHeight / 2 + 20, 0.3f, 0.3f);
                }
                else
                {
                    Player1.PlayHitSound();
                    //Console.WriteLine(Player2.PositionX - lechX);
                    animationManager.DrawMele(control, Player2.PositionX - lechX, Player2.PositionY - Player2.charHeight, 2, 2);
                    Player2.TruMau(Player1.Dame);
                    
                    Player2.XuLiKhiBiDanh();
                }
                if (Player1.AttackType != ActionState.AttackingI) Player1.HoiMana(10);
                Player2.PositionX = Math.Min(Player2.PositionX + lech1, 900);
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

