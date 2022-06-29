using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//used to pathfind to the player
public class Chase : Branch
{
    private Transform target;
    private NavMeshAgent agent;
    private EnemyAI ai;
    private float distanceToCover = 0.5f;

    public Chase(Transform target, NavMeshAgent agent, EnemyAI ai)
    {
        this.target = target;
        this.agent = agent;
        this.ai = ai;
    }

    //the observe changes the colour and branch of the enemy and only returns success and moves on after getting close enough to the player
    public override BranchState Observe()
    {
        ai.SetColour(Color.cyan);
        float distance = Vector3.Distance(target.position, agent.transform.position);
        if (distance > distanceToCover)
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);
            return BranchState.executing;
        }
        else
        {
            agent.isStopped = true;
            return BranchState.success;
        }
    }


}