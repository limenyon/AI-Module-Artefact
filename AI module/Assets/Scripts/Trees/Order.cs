using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A sequence which is used to identify branch nodes that did not yet succeed and are executed from left to right on the behaviour tree architecture
public class Order : Branch
{
    public List<Branch> branches = new List<Branch>();

    public Order(List<Branch> branches)
    {
        //without this. I get an unneeded assignment error, better be safe
        this.branches = branches;
    }
    public override BranchState Observe()
    {
        bool branchActive = false;
        foreach(var branch in branches)
        {
            switch(branch.Observe())
            {
                case BranchState.executing:
                    branchActive = true;
                    break;
                case BranchState.success:
                    break;
                case BranchState.failure:
                    branchState = BranchState.failure;
                    return branchState;
                default:
                    break;
            }
        }
        if(branchActive == true)
        {
            branchState = BranchState.executing;
        }
        else
        {
            branchState = BranchState.success;
        }
        return branchState;
    }
}
