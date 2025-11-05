using Cloth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothItemPower : ClothItemBase
    {
        public float attackMultiplier = 2f;

        public override void Collect()
        {
            base.Collect();
            Player.Instance.ChangeAttackMultiplier(attackMultiplier, duration);
        }
    }

}

