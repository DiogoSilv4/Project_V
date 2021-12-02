using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingPoints : MonoBehaviour
{
    [SerializeField] public List<Transform> points = new List<Transform>();

    public Transform GetPOIIndex(int index)
    {
        if (index >= points.Count)
        {
            return null;
        }
        return points[index];
    }
}
