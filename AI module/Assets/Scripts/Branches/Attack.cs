using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Used to make the enemy AI do damage to the player
public class Attack : Branch
{
    private NavMeshAgent agent;
    private EnemyAI ai;
    private Transform target;

    private Vector3 currentVelocity;
    private float smoothDamp;

    public Attack(NavMeshAgent agent, EnemyAI ai, Transform target)
    {
        this.agent = agent;
        this.ai = ai;
        this.target = target;
        smoothDamp = 1f;
    }

    //A very simple pathfinding system that simply runs at the player avoiding obstacles, does damage when in range every frame
    public override BranchState Observe()
    {
        agent.isStopped = true;
        ai.SetColour(Color.green);
        Vector3 direction = target.position - ai.transform.position;
        Vector3 currentDirection = Vector3.SmoothDamp(ai.transform.forward, direction, ref currentVelocity, smoothDamp);
        Quaternion rotation = Quaternion.LookRotation(currentDirection, Vector3.up);
        ai.transform.rotation = rotation;
        return BranchState.executing;
    }

}