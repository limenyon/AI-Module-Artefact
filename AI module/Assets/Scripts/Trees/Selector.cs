using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A selector is used to identify branches that did not return a failure and to execute them
public class Selector : Branch
{
    public List<Branch> branches = new List<Branch>();

    public Selector(List<Branch> branches)
    {
        //without this. I get an unneeded assignment error, better be safe
        this.branches = branches;
    }
    public override BranchState Observe()
    {
        foreach (var branch in branches)
        {
            switch (branch.Observe())
            {
                case BranchState.executing:
                    branchState = BranchState.executing;
                    return branchState;
                case BranchState.success:
                    branchState = BranchState.success;
                    return branchState;
                case BranchState.failure:
                    break;
                default:
                    break;
            }
        }
        branchState = BranchState.failure;
        return branchState;
    }
}