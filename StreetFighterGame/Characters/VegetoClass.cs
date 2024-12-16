using StreetFighterGame.GameEngine;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace StreetFighterGame.Characters
{
    public class Vegeto : Character
    {
        public Vegeto(int startX, int startY, float scaleFactor) : base(startX, startY, scaleFactor, 750, 1)
        {
            // Tải hoạt ảnh cho Goku với ActionState
            LoadAnimations(".\\Vegeto", new Dictionary<ActionState, string>
            {
                { ActionState.Standing, "Vegeto_2" },
                { ActionState.WalkingFront, "Vegeto_20" },
                { ActionState.WalkingBack, "Vegeto_21" },
                { ActionState.Defending, "Vegeto_120" },
                { ActionState.Jumping, "Vegeto_40" },
                { ActionState.AttackingJ, "Vegeto_201" },
                { ActionState.AttackingK, "Vegeto_202" },
                { ActionState.AttackingL, "Vegeto_203" },
                { ActionState.AttackingI, "Vegeto_204" },
                { ActionState.hit, "Vegeto_220" }
            }, new Dictionary<ActionState, int>
            {
                { ActionState.Standing, 8 },         // 4 khung hình cho Standing
                { ActionState.WalkingFront, 4 },     // 6 khung hình cho WalkingFront
                { ActionState.WalkingBack, 4 },      // 6 khung hình cho WalkingBack
                { ActionState.Defending, 1 },        // 3 khung hình cho Defending
                { ActionState.Jumping, 1 },         // 10 khung hình cho Jumping
                { ActionState.AttackingJ, 9 },       // 3 khung hình cho AttackingJ
                { ActionState.AttackingK, 7 },      // 17 khung hình cho AttackingK
                { ActionState.AttackingL, 7 },       // 4 khung hình cho AttackingL
                { ActionState.AttackingI, 4 },        // 4 khung hình cho AttackingI
                { ActionState.hit, 1 }
            });

            LoadHitboxAnimations(".\\Vegeto", new Dictionary<ActionState, string>
            {
                { ActionState.AttackingI, "Vegeto_1000" }
            }, new Dictionary<ActionState, int>
            {
                { ActionState.AttackingI, 22 }
            });

            Name = "Vegeto";
            LoadAvatar(".\\Vegeto\\Vegeto_9000-4.png");
            LoadHitSound(".\\sound\\PunchHit1.wav");
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
