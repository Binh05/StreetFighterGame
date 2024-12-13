using StreetFighterGame.GameEngine;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace StreetFighterGame.Characters
{
    public class King : Character
    {
        public King(int startX, int startY, float scaleFactor) : base(startX, startY, scaleFactor, 450, 5)
        {
            // Tải các hoạt ảnh cho King với ActionState
            LoadAnimations(".\\King", new Dictionary<ActionState, string>
            {
                { ActionState.Standing, "King_0" },
                { ActionState.WalkingFront, "King_20" },
                { ActionState.WalkingBack, "King_20" },
                { ActionState.Defending, "King_10" },
                { ActionState.Jumping, "King_40" },
                { ActionState.AttackingJ, "King_200" },
                { ActionState.AttackingK, "King_201" },
                { ActionState.AttackingL, "King_200" },
                { ActionState.AttackingI, "King_202" }
            }, new Dictionary<ActionState, int>
            {
                { ActionState.Standing, 3 },         // 3 khung hình cho Standing
                { ActionState.WalkingFront, 8 },     // 8 khung hình cho WalkingFront
                { ActionState.WalkingBack, 8 },      // 8 khung hình cho WalkingBack
                { ActionState.Defending, 2 },        // 2 khung hình cho Defending
                { ActionState.Jumping, 1 },          // 1 khung hình cho Jumping
                { ActionState.AttackingJ, 16 },      // 16 khung hình cho AttackingJ
                { ActionState.AttackingK, 17 },      // 17 khung hình cho AttackingK
                { ActionState.AttackingL, 3 },       // 3 khung hình cho AttackingL
                { ActionState.AttackingI, 4 }        // 4 khung hình cho AttackingI
            });
            Name = "King";
            LoadAvatar(".\\King\\King_9000-1.png");
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
