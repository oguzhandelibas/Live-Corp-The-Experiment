using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public interface IHealth
    {
        void TakeDamage();
        void Death();
    }

}