using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public class CoverAvailablityCheck : Branch
{
    private Wall[] availableWalls;
    private Transform target;
    private EnemyAI enemyAI;

    public CoverAvailablityCheck(Wall[] availableWalls, Transform target, EnemyAI enemyAI)
    {
        this.availableWalls = availableWalls;
        this.target = target;
        this.enemyAI = enemyAI;
    }

    //The observe returns success only after the best spot is found, if there are no spots found after all spots are checked then the branch is a failure and then the tree moves onto the next bracnh
    public override BranchState Observe()
    {
        Transform coverSpot = FindCoverSpot();
        enemyAI.SetCover(coverSpot);
        if(coverSpot != null)
        {
            return BranchState.success;
        }
        else
        {
            return BranchState.failure;
        }
    }
    //Finding the best spot from available walls
    private Transform FindCoverSpot()
    {
        float minimumAngle = 45.0f;
        Transform bestSpot = null;
        for (int i = 0; i < availableWalls.Length; i++)
        {
            Transform bestSpotInCover = FindBestCoverLocation(availableWalls[i], ref minimumAngle);
            if(bestSpotInCover != null)
            {
                bestSpot = bestSpotInCover;
            }
        }
        return bestSpot;
    }

    //Identifying which cover spot is the best to hide behind
    private Transform FindBestCoverLocation(Wall wall, ref float minimumAngle)
    {
        Transform[] availableSpots = wall.GetCovers();
        Transform bestSpot = null;
        for(int i = 0; i < availableSpots.Length; i++)
        {
            Vector3 direction = target.position - availableSpots[i].position;
            if(CheckIfSpotIsValid(availableSpots[i]))
            {
                float angle = Vector3.Angle(availableSpots[i].forward, direction);
                if(angle < minimumAngle)
                {
                    minimumAngle = angle;
                    bestSpot = availableSpots[i];
                }
            }
        }
        return bestSpot;
    }
    //A collision system to see if the agent can get to the point
    private bool CheckIfSpotIsValid(Transform spot)
    {
        Vector3 direction = target.position - spot.position;
        if (Physics.Raycast(spot.position, direction, out RaycastHit hit))
        {
            if(hit.collider.transform != target)
            {
                return true;
            }
        }
        return false; 
    }
}
