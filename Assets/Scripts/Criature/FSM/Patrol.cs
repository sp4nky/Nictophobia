using System;
using UnityEngine;
using UnityEngine.AI;

public partial class CriatureController
{
    public class Patrol : State
    {
        public Patrol(CriatureController criature) : base(criature)
        {

        }

        public override void OnStateEnter()
        {
            criature.ChangeToPatrolVelocity();
            criature.SetDefaultDestination();
            Debug.Log("Creature State" + this.GetType().ToString());

        }

        public override void Update()
        {
            if (criature.PlayerIsVisible())
            {
                criature.SetState(new Chase(criature));
            }
            else if (criature.HavePatrolsPoints() && !criature.agent.isStopped)
                criature.GotoNextPoint();
        }
    }

    internal void ForceDestination(Vector3 position)
    {
        NavMeshPath navMeshPath = new NavMeshPath();
        agent.CalculatePath(position, navMeshPath);
        if (navMeshPath.status == NavMeshPathStatus.PathComplete)
            agent.destination = position;
    }

    internal void PlayerLost()
    {
        sensor.PlayerLost();
    }
}
