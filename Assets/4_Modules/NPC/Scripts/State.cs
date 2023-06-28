using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    public class State
    {
        public enum STATE
        {
            IDLE,
            PATROL,
            PURSUE,
            ATTACK,
            SLEEP
        };

        public enum EVENT
        {
            ENTER,
            UPDATE,
            EXIT
        };

        public STATE name;
        protected EVENT stage;
        protected GameObject npc;
        protected AnimationControl animationControl;
        protected Transform player;
        protected Transform[] wayPoints;
        protected State nextState;
        protected NavMeshAgent agent;

        float visDist = 10.0f;
        float visAngle = 50.0f;
        float shootDist = 7.0f;

        public State(GameObject _npc, NavMeshAgent _agent, AnimationControl _animationControl, Transform _player, Transform[] _wayPoints)
        {
            npc = _npc;
            agent = _agent;
            animationControl = _animationControl;
            stage = EVENT.ENTER;
            player = _player;
            wayPoints = _wayPoints;
        }

        public virtual void Enter()
        {
            stage = EVENT.UPDATE;
        }

        public virtual void Update()
        {
            stage = EVENT.UPDATE;
        }

        public virtual void Exit()
        {
            stage = EVENT.EXIT;
        }

        public State Process()
        {
            if (stage == EVENT.ENTER) Enter();
            if (stage == EVENT.UPDATE) Update();
            if (stage == EVENT.EXIT)
            {
                Exit();
                return nextState;
            }

            return this;
        }

        public bool CanSeePlayer()
        {
            Vector3 direction = player.position - npc.transform.position;
            float angle = Vector3.Angle(direction, npc.transform.forward);

            if (direction.magnitude < visDist && angle < visAngle)
            {
                return true;
            }

            return false;
        }

        public bool CanAttackPlayer()
        {
            Vector3 direction = player.position - npc.transform.position;
            if (direction.magnitude < shootDist)
            {
                return true;
            }

            return false;
        }
    }

    public class Idle : State
    {
        public Idle(GameObject _npc, NavMeshAgent _agent, AnimationControl _animationControl, Transform _player, Transform[] _wayPoints)
            : base(_npc, _agent, _animationControl, _player, _wayPoints)
        {
            name = STATE.IDLE;
        }

        public override void Enter()
        {
            animationControl.PlayAnimation(AnimType.IDLE);
            base.Enter();
        }

        public override void Update()
        {
            if (CanSeePlayer())
            {
                nextState = new Pursue(npc, agent, animationControl, player, wayPoints);
                stage = EVENT.EXIT;
            }
            else if (Random.Range(0, 100) < 2)
            {
                nextState = new Patrol(npc, agent, animationControl, player, wayPoints);
                stage = EVENT.EXIT;
            }
        }

        public override void Exit()
        {
            //animationControl.ResetTrigger("isIdle");
            base.Exit();
        }
    }

    public class Patrol : State
    {
        int currentIndex = -1;

        public Patrol(GameObject _npc, NavMeshAgent _agent, AnimationControl _animationControl, Transform _player, Transform[] _wayPoints)
            : base(_npc, _agent, _animationControl, _player, _wayPoints)
        {
            name = STATE.PATROL;
            agent.speed = 2;
            agent.isStopped = false;
        }

        public override void Enter()
        {
            float lastDist = Mathf.Infinity;
            for (var i = 0; i < wayPoints.Length; i++)
            {
                Transform thisWP = wayPoints[i];
                float distance = Vector3.Distance(npc.transform.position, thisWP.position);
                if (distance < lastDist)
                {
                    currentIndex = i - 1;
                    lastDist = distance;
                }
            }

            animationControl.PlayAnimation(AnimType.WALK);
            base.Enter();
        }

        public override void Update()
        {
            if (agent.remainingDistance < 1)
            {
                if (currentIndex >= wayPoints.Length - 1)
                    currentIndex = 0;
                else
                    currentIndex++;

                agent.SetDestination(wayPoints[currentIndex].position);
            }

            if (CanSeePlayer())
            {
                nextState = new Pursue(npc, agent, animationControl, player, wayPoints);
                stage = EVENT.EXIT;
            }
        }

        public override void Exit()
        {
            //animationControl.ResetTrigger("isIdle");
            base.Exit();
        }
    }

    public class Pursue : State
    {
        public Pursue(GameObject _npc, NavMeshAgent _agent, AnimationControl _animationControl, Transform _player, Transform[] _wayPoints)
            : base(_npc, _agent, _animationControl, _player, _wayPoints)
        {
            name = STATE.PURSUE;
            agent.speed = 5;
            agent.isStopped = false;
        }

        public override void Enter()
        {
            animationControl.PlayAnimation(AnimType.RUN);
            base.Enter();
        }

        public override void Update()
        {
            agent.SetDestination(player.position);
            if (agent.hasPath)
            {
                if (CanAttackPlayer())
                {
                    nextState = new Attack(npc, agent, animationControl, player, wayPoints);
                    stage = EVENT.EXIT;
                }
                else if (!CanSeePlayer())
                {
                    nextState = new Patrol(npc, agent, animationControl, player, wayPoints);
                    stage = EVENT.EXIT;
                }
            }
        }

        public override void Exit()
        {
            //animationControl.ResetTrigger("isRunning");
            base.Exit();
        }
    }

    public class Attack : State
    {
        float rotationSpeed = 25.0f;
        //AudioSource shoot;

        public Attack(GameObject _npc, NavMeshAgent _agent, AnimationControl _animationControl, Transform _player,  Transform[] _wayPoints)
            : base(_npc, _agent, _animationControl, _player, _wayPoints)
        {
            name = STATE.ATTACK;
            //shoot = _npc.GetComponent<AudioSource>();
        }

        public override void Enter()
        {
            animationControl.PlayAnimation(AnimType.SHOOT);
            agent.isStopped = true;
            //shoot.Play(14000);
            base.Enter();
        }

        public override void Update()
        {
            Vector3 direction = player.position - npc.transform.position;
            float angle = Vector3.Angle(direction, npc.transform.forward);
            direction.y = 0;

            npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(direction),
                Time.deltaTime * rotationSpeed);

            if (!CanAttackPlayer())
            {
                nextState = new Idle(npc, agent, animationControl, player, wayPoints);
                stage = EVENT.EXIT;
            }
        }

        public override void Exit()
        {
            //animationControl.ResetTrigger("isShooting");
            //shoot.Stop();
            base.Exit();
        }
    }
}
