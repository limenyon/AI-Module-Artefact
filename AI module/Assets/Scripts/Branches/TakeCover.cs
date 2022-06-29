using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//used for moving the enemy agent behind cover that has been identified as the best

public class TakeCover : Branch
{
    private NavMeshAgent agent;
    private EnemyAI ai;
    private float distanceToCover = 0.5f;

    public TakeCover(NavMeshAgent agent, EnemyAI ai)
    {
        this.agent = agent;
        this.ai = ai;
    }

    //The observe here sets the state of the enemy agent and it's colour to indicate that it is taking cover and only once the agent is stopped it returns success and moves onto the next branch

    public override BranchState Observe()
    {
        Transform coverSpot = ai.GetBestCover();
        if(coverSpot == null)
            return BranchState.failure;
        ai.SetColour(Color.white);
        float distance = Vector3.Distance(coverSpot.position, agent.transform.position);
        if (distance > distanceToCover)
        {
            agent.isStopped = false;
            agent.SetDestination(coverSpot.position);
            return BranchState.executing;
        }
        else
        {
            agent.isStopped = true;
            return BranchState.success;
        }
    }


}