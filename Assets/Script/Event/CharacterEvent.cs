using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Script.Event
{
    internal class CharacterEvent
    {
        //character damage and damage value
        public static UnityAction<GameObject, int> characterDamaged;

        //character healed and amount healed
        public static UnityAction<GameObject, int> characterHealed;
    }
}
