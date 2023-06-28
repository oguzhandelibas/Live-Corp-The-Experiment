using System;
using UnityEngine;

namespace NPC
{
    public class AnimationControl : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        public CharacterType characterType;
        private AnimType animNowSelect = AnimType.IDLE;

        private void Start()
        {
            animator.SetFloat("Blend", (float)characterType);
        }

        public void PlayAnimation(AnimType animName)
        {
            if (animNowSelect == animName)
                return;

            foreach (AnimType item in (AnimType[])Enum.GetValues(typeof(AnimType)))
                animator.SetBool(item.ToString(), item == animName);

            animNowSelect = animName;
        }
    }
}
