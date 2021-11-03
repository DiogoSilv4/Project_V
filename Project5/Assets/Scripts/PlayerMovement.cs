using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private CharacterController controller;

    [SerializeField] private float speed = 12f;

    private GameObject[] taggedObjects;
    [SerializeField] private float closeDistance;


    [SerializeField] private Transform CanSpot;

    private GameObject currentCan = null;
    // Start is called before the first frame update


    void Start()
    {
        taggedObjects = GameObject.FindGameObjectsWithTag("Can");
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        closeToCan();
        if (currentCan != null)
        {
            GrabCan(currentCan);

            if (Input.GetKeyDown(KeyCode.E))
            {
                DropCan(currentCan);
                
            }
        }
    }

    private void movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftControl))
        {
            this.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);

        }
        else
        {
            this.transform.position = new Vector3(this.transform.position.x, 1, this.transform.position.z);

        }
    }

    private void closeToCan()
    {
        

        for (int i = 0; i < taggedObjects.Length; i++)
        {
            float dist = Vector3.Distance(this.transform.position, taggedObjects[i].transform.position);
            if ( dist <= closeDistance)
            {

                if (Input.GetKey(KeyCode.E))
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
        Can.transform.position = CanSpot.position;
        Can.transform.rotation = CanSpot.rotation;

        //ApplyForce(Can.GetComponent<Rigidbody>(), CanSpot);

        Can.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }
    private void DropCan(GameObject Can)
    {
        Can.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        currentCan = null;
    }

    void ApplyForce(Rigidbody can, Transform pos)
    {
        Vector3 direction =  - pos.position + can.transform.position  ;
        can.AddForce(direction * -20f);
    }
}
