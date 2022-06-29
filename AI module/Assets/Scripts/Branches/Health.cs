using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A branch for checking the health state of the enemy ai 
public class Health : Branch
{
    private EnemyAI enemyAI;
    private float hideThreshold;

    //auto generated constructor using unity, really cool thing
    public Health(EnemyAI enemyAI, float hideThreshold)
    {
        this.enemyAI = enemyAI;
        this.hideThreshold = hideThreshold;
    }

    //Just checking if the AI has enough health to keep attacking or if it should get to cover

    public override BranchState Observe()
    {
        if(enemyAI.GetCurrentHealth() <= hideThreshold)
        {
            return BranchState.success;
        }
        else
        {
            return BranchState.failure;
        }
    }
}
