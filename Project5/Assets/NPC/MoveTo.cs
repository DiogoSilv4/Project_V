using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{

    [SerializeField] private NavMeshAgent cube;

    private Transform poiDestiny;
    private int poiNumber = 0;
    private int number;

    [SerializeField] public List<Transform> points = new List<Transform>();

    public Transform GetPOIIndex(int index)
    {
        if (index >= points.Count)
        {
            return null;
        }
        return points[index];
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        poiDestiny = points[poiNumber];
        moveCube(poiDestiny);
        hasArrived(poiDestiny);
    }
    private void moveCube(Transform poiTransform)
    {
        cube.SetDestination(poiTransform.position);
        // cube.jump;
    }
    private void hasArrived(Transform poiTransform)
    {

        if (!cube.pathPending)
        {
            if (cube.remainingDistance <= cube.stoppingDistance)
            {

                if (!cube.hasPath || cube.velocity.sqrMagnitude == 0f)
                {
                    number = points.Count - 1;
                    if (number == poiNumber)
                    {
                        poiNumber = 0;
                    }
                    else
                    {
                        poiNumber++;
                    }

                }
            }
        }
    }
}