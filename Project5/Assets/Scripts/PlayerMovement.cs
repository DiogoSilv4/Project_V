using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject[] taggedObjects;
    [SerializeField] private float closeDistance;

    [SerializeField] private Transform CanSpot;

    public GameObject currentCan = null;
    public bool IsCanGrabbed = false;
    public bool canWalk = true;
    public bool canLook = true;

    void Start()
    {
        taggedObjects = GameObject.FindGameObjectsWithTag("Can");
    }

    // Update is called once per frame
    void Update()
    {
        cursorLock();


        if (currentCan == null)
        {
            closeToCan();
        }
        else
        {
            GrabCan(currentCan);

            if (Input.GetKeyDown(KeyCode.E))
            {
                DropCan(currentCan);
                
            }
        }
    }

   

    private void closeToCan()
    {
        for (int i = 0; i < taggedObjects.Length; i++)
        {
            float dist = Vector3.Distance(this.transform.position, taggedObjects[i].transform.position);
            if ( dist <= closeDistance)
            {

                if (Input.GetKeyDown(KeyCode.E))
                {
                    currentCan = taggedObjects[i];
                }
            }
            else 
            {

                
            }
        
        }
    }

    private void GrabCan(GameObject Can)
    {
        IsCanGrabbed = true;
        Can.transform.position = CanSpot.position;
        Can.transform.rotation = CanSpot.rotation;

        //ApplyForce(Can.GetComponent<Rigidbody>(), CanSpot);

        Can.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        //Can.GetComponent<Rigidbody>().useGravity = false;
        Can.GetComponent<Rigidbody>().isKinematic = true;

    }
    private void DropCan(GameObject Can)
    {
        IsCanGrabbed = false;
        Can.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        currentCan = null;
        Can.GetComponent<Rigidbody>().isKinematic = false;


    }
    
    private void cursorLock()
    {
        if (canLook)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

   
}
