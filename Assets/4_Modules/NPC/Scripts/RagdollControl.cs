using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public class RagdollControl : MonoBehaviour
    {
        private Rigidbody[] rbs;

        private void Awake()
        {
            rbs = GetComponentsInChildren<Rigidbody>();
        }

        public void ToggleRagdoll(Animator animator, bool x)
        {
            foreach(Rigidbody rb in rbs) rb.isKinematic = !x;
            animator.enabled = !x;
        }
    }
}
