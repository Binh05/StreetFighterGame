using StreetFighterGame.GameEngine;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace StreetFighterGame.Characters
{
    public class Chunli : Character
    {
        public Chunli(int startX, int startY, float scaleFactor) : base(startX, startY, scaleFactor, 300, 2)
        {
            // Tải hoạt ảnh cho Chunli với ActionState
            LoadAnimations(".\\Chunli", new Dictionary<ActionState, string>
            {
                { ActionState.Standing, "ChunLi_0" },
                { ActionState.WalkingFront, "ChunLi_20" },
                { ActionState.WalkingBack, "ChunLi_20" },
                { ActionState.Defending, "ChunLi_10" },
                { ActionState.Jumping, "ChunLi_41" },
                { ActionState.AttackingJ, "ChunLi_2122" },
                { ActionState.AttackingK, "ChunLi_201" },
                { ActionState.AttackingL, "ChunLi_202" },
                { ActionState.AttackingI, "ChunLi_205" }
            }, new Dictionary<ActionState, int>
            {
                { ActionState.Standing, 4 },         // 4 khung hình cho Standing
                { ActionState.WalkingFront, 6 },     // 6 khung hình cho WalkingFront
                { ActionState.WalkingBack, 6 },      // 6 khung hình cho WalkingBack
                { ActionState.Defending, 3 },        // 3 khung hình cho Defending
                { ActionState.Jumping, 10 },         // 10 khung hình cho Jumping
                { ActionState.AttackingJ, 3 },       // 3 khung hình cho AttackingJ
                { ActionState.AttackingK, 17 },      // 17 khung hình cho AttackingK
                { ActionState.AttackingL, 8 },       // 4 khung hình cho AttackingL
                { ActionState.AttackingI, 5 }        // 4 khung hình cho AttackingI
            });
            Name = "Chunli";
            LoadAvatar(".\\Chunli\\ChunLi_9000-1.png");
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
