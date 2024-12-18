using StreetFighterGame.GameEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace StreetFighterGame.Characters
{
    public class Ryu : Character
    {
        public Ryu(int startX, int startY, float scaleFactor) : base(startX, startY, scaleFactor, 400, 7)
        {
            // Tải các ảnh cho từng trạng thái với số lượng khung hình khác nhau
            LoadAnimations(".\\Ryu", new Dictionary<ActionState, string>
            {
                { ActionState.Standing, "Ryu_0" },
                { ActionState.WalkingFront, "Ryu_20" },
                { ActionState.WalkingBack, "Ryu_20" },
                { ActionState.Jumping, "Ryu_40" },
                { ActionState.Defending, "Ryu_10" },
                { ActionState.AttackingJ, "Ryu_200" },
                { ActionState.AttackingK, "Ryu_210" },
                { ActionState.AttackingL, "Ryu_230" },
                { ActionState.AttackingI, "Ryu_400" }
            }, new Dictionary<ActionState, int>
            {
                { ActionState.Standing, 5 },       // 5 khung hình cho Standing
                { ActionState.WalkingFront, 7 },   // 7 khung hình cho WalkingFront
                { ActionState.WalkingBack, 7 },    // 7 khung hình cho WalkingBack
                { ActionState.Jumping, 7 },        // 7 khung hình cho Jumping
                { ActionState.Defending, 3 },      // 3 khung hình cho Defending
                { ActionState.AttackingJ, 7 },     // 7 khung hình cho AttackingJ
                { ActionState.AttackingK, 10 },    // 10 khung hình cho AttackingK
                { ActionState.AttackingL, 18 },    // 18 khung hình cho AttackingL
                { ActionState.AttackingI, 9 }      // 9 khung hình cho AttackingI
            });



            Name = "Ryu";
            LoadAvatar(".\\Ryu\\Ryu_9000-1.png");
        }
        public override void SpecicalSkill()
        {
            throw new System.NotImplementedException();
        }

        //public override void Attack(ActionState attackType)
        //{
        //    // Logic tấn công của Ryu
        //    ChangeState(attackType);
        //}

        //public override void StopAttacking()
        //{
        //    ChangeState(ActionState.Standing);
        //}
    }
}
