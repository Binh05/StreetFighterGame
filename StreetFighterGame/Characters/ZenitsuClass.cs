using StreetFighterGame.GameEngine;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace StreetFighterGame.Characters
{
    public class Zenitsu : Character
    {
        public Zenitsu(int startX, int startY, float scaleFactor) : base(startX, startY, scaleFactor, 500, 30)
        {
            // Tải hoạt ảnh cho Goku với ActionState
            LoadAnimations(".\\Zenitsu", new Dictionary<ActionState, string>
            {
                { ActionState.Standing, "Zenitsu_0" },
                { ActionState.WalkingFront, "Zenitsu_21" },
                { ActionState.WalkingBack, "Zenitsu_21" },
                { ActionState.Defending, "Zenitsu_7" },
                { ActionState.Jumping, "Zenitsu_40" },
                { ActionState.AttackingJ, "Zenitsu_201" },
                { ActionState.AttackingK, "Zenitsu_202" },
                { ActionState.AttackingL, "Zenitsu_205" },
                { ActionState.AttackingI, "Zenitsu_204" },
                { ActionState.hit, "Zenitsu_5010" }
            }, new Dictionary<ActionState, int>
            {
                { ActionState.Standing, 5 },         // 4 khung hình cho Standing
                { ActionState.WalkingFront, 5 },     // 6 khung hình cho WalkingFront
                { ActionState.WalkingBack, 5 },      // 6 khung hình cho WalkingBack
                { ActionState.Defending, 1 },        // 3 khung hình cho Defending
                { ActionState.Jumping, 1 },         // 10 khung hình cho Jumping
                { ActionState.AttackingJ, 4 },       // 3 khung hình cho AttackingJ
                { ActionState.AttackingK, 3 },      // 17 khung hình cho AttackingK
                { ActionState.AttackingL, 4 },       // 4 khung hình cho AttackingL
                { ActionState.AttackingI, 4 },        // 4 khung hình cho AttackingI
                { ActionState.hit, 1 }
            });
            Name = "Zenitsu";
            LoadAvatar(".\\Zenitsu\\Zenitsu_9000-0.png");
        }
    }
}
