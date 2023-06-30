using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    public class AIBrain : MonoBehaviour, IHealth
    {
        [SerializeField] private RagdollControl _ragdollControl;
        [SerializeField] private AnimationControl _animationControl;
        [SerializeField] private EffectControl _effectControl;
        [SerializeField] private Transform[] wayPoints;
        [SerializeField] private  Transform player;
        private int health = 100;
        private bool alive;
        NavMeshAgent agent;
        Animator animator;
        State currentState;

        void Start()
        {
            alive = true;
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponentInChildren<Animator>();
            currentState = new Idle(gameObject, agent, _animationControl, player, wayPoints);
            _ragdollControl.ToggleRagdoll(animator, false);
        }

        void Update()
        {
            if(alive) currentState = currentState.Process();
        }

        public void TakeDamage(Vector3 hitPos)
        {
            health -= 50;
            //create effect
            _effectControl.CreateBloodEffect(transform, hitPos);
            if (health <= 0) Death();
        }

        public void Death()
        {
            GetComponent<CapsuleCollider>().enabled = false;
            alive = false;
            agent.enabled = false;
            _ragdollControl.ToggleRagdoll(animator, true);
        }
    }
}
