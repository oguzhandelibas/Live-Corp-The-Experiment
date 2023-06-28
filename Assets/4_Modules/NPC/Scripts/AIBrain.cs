using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    public class AIBrain : MonoBehaviour
    {
        [SerializeField] private AnimationControl _animationControl;
        [SerializeField] private Transform[] wayPoints;
        NavMeshAgent agent;
        Animator anim;
        public Transform player;
        State currentState;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponentInChildren<Animator>();
            currentState = new Idle(gameObject, agent, _animationControl, player, wayPoints);
        }

        void Update()
        {
            currentState = currentState.Process();
        }
    }
}
