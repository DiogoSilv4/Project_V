using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor.Animations;

public class MoveTo : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    

    private Transform poiDestiny;
    private int poiNumber = 0;
    private int number;

    [SerializeField] public List<Transform> points = new List<Transform>();

    [SerializeField] private float[] secondsWaiting;

    //public Transform GetPOIIndex(int index)
    //{
    //    if (index >= points.Count)
    //    {
    //        return null;
    //    }
    //    return points[index];
    //}

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        poiDestiny = points[poiNumber];
        moveCube(poiDestiny);
        hasArrived();
        idleToWalking();
    }
    private void moveCube(Transform poiTransform)
    {
        agent.SetDestination(poiTransform.position);
        // cube.jump;
    }


    IEnumerator Waiting()
    {

        Debug.Log("nop");

        yield return new WaitForSeconds(secondsWaiting[poiNumber]);

        
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
    private void idleToWalking()
    {
        //Debug.Log(agent.velocity.magnitude);
        animator.SetFloat("speed", agent.velocity.magnitude);

        if (agent.velocity.magnitude == 0)
        {
            animator.SetBool("Walking", false);
        }
        else
        {
            animator.SetBool("Walking", true);
        }

        //if (agent.remainingDistance <= 2)
        //{
        //    agent.speed = agent.remainingDistance;
        //}
    }

    private void hasArrived()
    {

        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    Debug.Log("why");
                    Waiting();
                }
            }
        }
    }
}