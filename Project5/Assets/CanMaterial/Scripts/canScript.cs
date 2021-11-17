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

    
    private Outline outline;

    [SerializeField] private float ControllerDistance = 1.0f;

    [SerializeField]
    private float rayDistance = 1.5f;

    [SerializeField]
    private Transform rayOrigin = null;

    [SerializeField]
    private GameObject hitOnWall;

    [SerializeField]
    private MyDrawingController drawingScript;
    [SerializeField]
    private PlayerMovement movementScript;

    // Start is called before the first frame update
    void Start()
    {
        //outline = this.GetComponent<Outline>();
        //outline.enabled = false;
        outline = gameObject.AddComponent<Outline>();

        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = CanColor;
        outline.OutlineWidth = 10f;


    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance( transform.position, controller.position);
        float dist2 = Vector3.Distance(controller2.position, transform.position);


        //Debug.Log(movementScript.IsCanGrabbed);

        if ( ( dist <= ControllerDistance || dist2 <= ControllerDistance) && !movementScript.IsCanGrabbed )
        {

            outline.enabled = true;

        }
        else if ((dist > ControllerDistance  && dist2 > ControllerDistance) || movementScript.IsCanGrabbed)
        {
            outline.enabled = false;

            //Destroy(outline);
        }
        

        Vector3 startPoint = rayOrigin.position;
        //Vector3 endPoint = transform.position + (transform.forward * rayDistance);

        RaycastHit objectHit;
        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);

        if (Physics.Raycast(ray, out objectHit, rayDistance) && movementScript.IsCanGrabbed)
        {
            Debug.DrawLine(startPoint, objectHit.point, Color.green);

            
            hitOnWall.transform.position = objectHit.point;
            //hitOnWall.transform.rotation = Quaternion.Euler(objectHit.normal);
            hitOnWall.SetActive(true);

        }
        else
        {
            hitOnWall.SetActive(false);

        }


    }
    
}
