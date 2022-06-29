using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private Transform[] covers;

    public Transform[] GetCovers()
    {
        return covers;
    }
}
