using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseToObjective : MonoBehaviour
{
    //[SerializeField] private GameObject prefab;
    [SerializeField] private float ClosingDistance = 15f;
    [SerializeField] private Transform Objective;
    private Transform player;
    private Color cillinder;
    private float distance;
    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        cillinder = this.gameObject.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.position, Objective.position);
        if (distance <= ClosingDistance)
        {
            //float transparency = 0;
            cillinder.a = 0;
            Debug.Log("InClose");
        }
        else
        {
            cillinder.a = 1;
        }
    }
}
