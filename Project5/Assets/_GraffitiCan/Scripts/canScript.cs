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
    private float rayDistance = 1.35f;

    [SerializeField]
    private Transform rayOrigin = null;

    [SerializeField]
    private GameObject hitOnWall;

    [SerializeField]
    private MyDrawingController drawingScript;
    [SerializeField]
    private PlayerMovement movementScript;

    private float firstCircleSize;

    // Start is called before the first frame update
    void Start()
    {
        //outline = this.GetComponent<Outline>();
        //outline.enabled = false;
        outline = gameObject.AddComponent<Outline>();

        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = CanColor;
        outline.OutlineWidth = 10f;

        rayDistance = range + 1.76f;
    }

    // Update is called once per frame
    void Update()
    {
        changeVariable();

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

        RaycastHit objectHit;
        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);

        if (Physics.Raycast(ray, out objectHit, rayDistance) && movementScript.IsCanGrabbed && movementScript.currentCan == this.gameObject)
        {
            Debug.DrawLine(startPoint, objectHit.point, Color.green);
            //Debug.Log(objectHit.distance);

            //if (objectHit.Equals(j))
            //{
            //    Debug.Log("YUP");
            //}
            
            hitOnWall.transform.position = objectHit.point + objectHit.normal * 0.10f;
            //hitOnWall.transform.rotation = Quaternion.Euler(objectHit.normal + new Vector3(0,90,0));
            hitOnWall.transform.forward = objectHit.normal;

            hitOnWall.transform.localScale = new Vector3(1, 1, 1) * (objectHit.distance * firstCircleSize / 0.768f );



            hitOnWall.SetActive(true);
        }
        else
        {
            hitOnWall.SetActive(false);

        }

    }

    private void changeVariable()
    {
        if (canTap == 6f)
        {
            firstCircleSize = 0.72752f;
        }
        else if (canTap == 15f)
        {
            firstCircleSize = 1.313395f;
        }else if (canTap == 23f)
        {
            firstCircleSize = 2.211007f;
        }
    }
}
