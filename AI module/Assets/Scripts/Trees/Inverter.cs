using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//An inverter is used to save on code by simply inverting predicates where a true was expected into a false
public class Inverter : Branch
{
    protected Branch branch;

    public Inverter(Branch branch)
    {
        //without this. I get an unneeded assignment error, better be safe
        this.branch = branch;
    }
    public override BranchState Observe()
    {
            switch (branch.Observe())
            {
                case BranchState.executing:
                    branchState = BranchState.executing;
                break;
                case BranchState.success:
                    branchState = BranchState.failure;
                    break;
                case BranchState.failure:
                    branchState = BranchState.success;
                    break;
                default:
                    break;
            }
        return branchState;
    }
}
