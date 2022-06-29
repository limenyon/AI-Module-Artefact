using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//An abstract node used to derive other nodes from, only has enumerated states and an overridable Observe function used to see what state the branches are in

[System.Serializable]
public abstract class Branch
{
    public BranchState branchState;

    public BranchState ReturnBehaveSatete()
    {
        return branchState;
    }

    public abstract BranchState Observe();
}
public enum BranchState
{
    failure, executing, success,
}