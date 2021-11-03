using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDrawingController : MonoBehaviour {

    private MySpotDrawer spot;
    private MyDrawable[] drawablesInScene;
    [SerializeField] private GameObject sprayPaint;
    GameObject drawables;
    private AudioSource Paint_Spray;


    private void Awake()
    {
        drawables = GameObject.Find("Drawables");
    }
    void Start()
    {
        spot = GetComponent<MySpotDrawer>();
        drawablesInScene = drawables.GetComponent<Drawables>().AllDrawables;
        Paint_Spray = GetComponentInParent<AudioSource>();

    }
    // Update is called once per frame
    void Update () {


        if (Input.GetKey("space"))
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
