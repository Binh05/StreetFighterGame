using StreetFighterGame.GameEngine;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace StreetFighterGame.Characters
{
    public class Kyo : Character
    {
        public Kyo(int startX, int startY, float scaleFactor) : base(startX, startY, scaleFactor, 500, 5)
        {
            // Tải các hoạt ảnh cho Kyo với ActionState
            LoadAnimations(".\\Kyo", new Dictionary<ActionState, string>
            {
                { ActionState.Standing, "Kyo_0" },
                { ActionState.WalkingFront, "Kyo_2" },
                { ActionState.WalkingBack, "Kyo_2" },
                { ActionState.Defending, "Kyo_4" },
                { ActionState.Jumping, "Kyo_3" },
                { ActionState.AttackingJ, "Kyo_7" },
                { ActionState.AttackingK, "Kyo_8" },
                { ActionState.AttackingL, "Kyo_580" },
                { ActionState.AttackingI, "Kyo_9" }
            }, new Dictionary<ActionState, int>
            {
                { ActionState.Standing, 6 },        // 6 khung hình cho Standing
                { ActionState.WalkingFront, 8 },    // 8 khung hình cho WalkingFront
                { ActionState.WalkingBack, 8 },     // 8 khung hình cho WalkingBack
                { ActionState.Defending, 2 },       // 2 khung hình cho Defending
                { ActionState.Jumping, 1 },         // 1 khung hình cho Jumping
                { ActionState.AttackingJ, 10 },     // 10 khung hình cho AttackingJ
                { ActionState.AttackingK, 13 },     // 13 khung hình cho AttackingK
                { ActionState.AttackingL, 8 },      // 8 khung hình cho AttackingL
                { ActionState.AttackingI, 7 }       // 7 khung hình cho AttackingI
            });
            Name = "Kyo";
            LoadAvatar(".\\Kyo\\Kyo_9000-1.png");
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
