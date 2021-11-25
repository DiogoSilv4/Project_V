using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{

    [SerializeField]
    private string[] Tasks;

    [SerializeField]
    private GameObject[] objects;

    [SerializeField]
    private Transform[] Place;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i< Tasks.Length; i++)
        {
            if (Tasks[i] == "bring")
            {

            }

        }
    }
}
