using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canScript : MonoBehaviour
{
    [SerializeField] private GameObject CanSpot;
    public Color CanColor = Color.green;
    [Range(0.01f, 90f)] public float canTap = 15.0f;
    public float range = 1.0f;
    [SerializeField] private Transform controller;

    [SerializeField] private Transform controller2;

    private Outline outline = null;

    [SerializeField] private float ControllerDistance = 1.0f;


    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(controller.position, this.transform.position);
        float dist2 = Vector3.Distance(controller2.position, this.transform.position);


        if (( dist <= ControllerDistance || dist2 <= ControllerDistance) && outline == null)
        {

            outline = gameObject.AddComponent<Outline>();

            outline.OutlineMode = Outline.Mode.OutlineVisible;
            outline.OutlineColor = CanColor;
            outline.OutlineWidth = 10f;

        }
        else if (dist > ControllerDistance && dist2 > ControllerDistance)
        {

            Destroy(outline);
        }


       
    }
}
