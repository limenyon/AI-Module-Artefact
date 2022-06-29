using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//to check if the enemy is covered
public class Covered : Branch
{
    private Transform target;
    private Transform origin;

    public Covered(Transform target, Transform origin)
    {
        this.target = target;
        this.origin = origin;
    }

    //Checks if a ray can hit the player, if not it is covered, if so it is not, useful for moving terrain

    public override BranchState Observe()
    {
        if (Physics.Raycast(origin.position, target.position - origin.position, out RaycastHit hit))
        {
            if (hit.collider.transform != target)
            {
                return BranchState.success;
            }
        }
        return BranchState.failure;
    }
}