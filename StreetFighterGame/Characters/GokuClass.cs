﻿using StreetFighterGame.GameEngine;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace StreetFighterGame.Characters
{
    public class Goku : Character
    {
        public Goku(int startX, int startY, float scaleFactor) : base(startX, startY, scaleFactor, 1000, 1)
        {
            // Tải hoạt ảnh cho Goku với ActionState
            LoadAnimations(".\\GokuMUI", new Dictionary<ActionState, string>
            {
                { ActionState.Standing, "GokuMUI_0" },
                { ActionState.WalkingFront, "GokuMUI_25" },
                { ActionState.WalkingBack, "GokuMUI_25" },
                { ActionState.Defending, "GokuMUI_120" },
                { ActionState.Jumping, "GokuMUI_40" },
                { ActionState.AttackingJ, "GokuMUI_201" },
                { ActionState.AttackingK, "GokuMUI_202" },
                { ActionState.AttackingL, "GokuMUI_203" },
                { ActionState.AttackingI, "GokuMUI_205" },
                { ActionState.hit, "GokuMUI_5001" }
            }, new Dictionary<ActionState, int>
            {
                { ActionState.Standing, 3 },         // 4 khung hình cho Standing
                { ActionState.WalkingFront, 5 },     // 6 khung hình cho WalkingFront
                { ActionState.WalkingBack, 5 },      // 6 khung hình cho WalkingBack
                { ActionState.Defending, 3 },        // 3 khung hình cho Defending
                { ActionState.Jumping, 1 },         // 10 khung hình cho Jumping
                { ActionState.AttackingJ, 15 },       // 3 khung hình cho AttackingJ
                { ActionState.AttackingK, 8 },      // 17 khung hình cho AttackingK
                { ActionState.AttackingL, 6 },       // 4 khung hình cho AttackingL
                { ActionState.AttackingI, 9 },        // 4 khung hình cho AttackingI
                { ActionState.hit, 1 }
            });
            Name = "Goku";
            LoadAvatar(".\\GokuMUI\\GokuMUI_9000-1.png");
        }

        //public override void Attack(ActionState attackType)
        //{
        //    if (CurrentState != ActionState.Jumping)
        //    {
        //        ChangeState(attackType);
        //    }
        //}

        //public override void StopAttacking()
        //{
        //    ChangeState(ActionState.Standing);
        //}
    }
}