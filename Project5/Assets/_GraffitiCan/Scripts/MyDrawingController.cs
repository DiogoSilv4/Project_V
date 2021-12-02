using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDrawingController : MonoBehaviour {

    private MySpotDrawer spot;
    private MyDrawable[] drawablesInScene;
    [SerializeField] private GameObject sprayPaint;
    GameObject drawables;
    public AudioSource Paint_Spray;
    public AudioSource Can_shake;


    private OVRGrabbable ovrGrabable;
    public OVRInput.Button sprayButton;
    private Vector3 posLastFrame;



    public float magnitudeOfVelocityToSound = 1.0f;

    [SerializeField]
    private PlayerMovement plyrScpt;
    private bool CanGrabbed;
    private GameObject can;

    [SerializeField]
    private GameObject ThisCan;

    private void Awake()
    {
        drawables = GameObject.Find("Drawables");
    }
    void Start()
    {
        spot = GetComponent<MySpotDrawer>();
        drawablesInScene = drawables.GetComponent<Drawables>().AllDrawables;
        //Paint_Spray = GetComponentInParent<AudioSource>();
        //Can_shake = GetComponent<AudioSource>();

        ovrGrabable = GetComponentInParent<OVRGrabbable>();

        posLastFrame = transform.position;

        spot.UpdateDrawingMat();
        for (int i = 0; i < drawablesInScene.Length; i++)
        {
            spot.Draw(drawablesInScene[i]);
        }

    }
    // Update is called once per frame
    void Update () {

        if (Vector3.Distance(posLastFrame , transform.position )/Time.deltaTime > magnitudeOfVelocityToSound)
        {
            //Debug.Log("YEss");
            Can_shake.mute = false;

        }
        else
        {
            Can_shake.mute = true;
        }

        posLastFrame = transform.position;



        //if (ovrGrabable.isGrabbed && OVRInput.GetDown(sprayButton) )
        //{

        //}
        CanGrabbed = plyrScpt.IsCanGrabbed;
        can = plyrScpt.currentCan;
        


        if (Input.GetKey("space") && can == ThisCan && CanGrabbed )
        {
            spot.UpdateDrawingMat();

            foreach (var drawable in drawablesInScene)
                spot.Draw(drawable);

            sprayPaint.SetActive(true);
            Paint_Spray.mute = false;
            

        }
        else
        {
            sprayPaint.SetActive(false);
            Paint_Spray.mute = true;
        }



    }
}
