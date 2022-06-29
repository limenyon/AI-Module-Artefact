using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a way to identify how far away the player is
public class Range : Branch
{
    private float range;
    private Transform target;
    private Transform origin;

    public Range(float range, Transform target, Transform origin)
    {
        this.range = range;
        this.target = target;
        this.origin = origin;
    }

    //Checks how far away the player is

    public override BranchState Observe()
    {
        float distance = Vector3.Distance(target.position, origin.position);
        if(distance <= range)
        {
            return BranchState.success;
        }
        else
        {
            return BranchState.failure;
        }
    }
}
