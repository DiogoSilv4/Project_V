using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission : MonoBehaviour
{

    [SerializeField] private string[] missions_names = new string[] { "goTo", "delivery", "paint" };
    public List<Mission_things> Missions;


    private float distance;
    public int currentMissionValue = 0;

    private Texture wall;
    public bool entered = false;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Missions.Count; i++)
        {
            Missions[i].Place.SetActive(false);
        }
        AbleObjectives();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Missions[currentMissionValue].Type == missions_names[1])
        {
            currentMission();
        }
    }

    public void paintWall()
    {
        //wall = Missions[currentMissionValue].Objects[0].GetComponent<Renderer>().material.GetTexture("_MainText");

        var mpb = new MaterialPropertyBlock();
        Missions[currentMissionValue].Objects[0].GetComponent<Renderer>().GetPropertyBlock(mpb);
        wall =  mpb.GetTexture("_MainTex");
        RenderTexture wall_ = (RenderTexture) wall;

        Texture2D _wall = toTexture2D(wall_);

        var pix_count = 0;
        //Debug.Log("yyyy");

        for (int x = 0; x < _wall.width; x++)
        {
            for (int y = 0; y < _wall.height; y++)
            {
                Color pix = _wall.GetPixel(x, y);
                if (pix != Color.black)
                {
                    pix_count++;
                }
            }
        }
        if (pix_count > 1)
        {
            MissionCompleted();
        }
        else
        {
            Debug.Log("NOT");
        }
    }

    private void currentMission()
    {
        //UI_Mission_Stat.text = Missions[currentMissionValue].Name;

        //Instantiate(sphere_prefab, Missions[currentMissionValue].Place);

        Missions[currentMissionValue].Place.SetActive(true);

        var count = 0;
        for(int i = 0; i < Missions[currentMissionValue].Objects.Length; i++)
        {
            
            if (checkDistance(Missions[currentMissionValue].Objects[i], Missions[currentMissionValue].Place.transform)
                && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().IsCanGrabbed == false)
            {
                count++; 
            }
        }
        if (count == Missions[currentMissionValue].Objects.Length)
        {
            Missions[currentMissionValue].Place.SetActive(false);
            MissionCompleted();
        }

        //Debug.Log(Missions.Count);

        
    }

    private bool checkDistance(GameObject _object, Transform place)
    {
        distance = Vector3.Distance(_object.transform.position, place.position);

        if (distance < 2.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(512, 512, TextureFormat.RGB24, false);
        // ReadPixels looks at the active RenderTexture.
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }

    public void MissionCompleted()
    {
        
        Debug.Log("Mission_Completed");
        Missions[currentMissionValue].isCompleted = true;

        Destroy(Missions[currentMissionValue].Place); 
        if (currentMissionValue < Missions.Count - 1  )
        {
            currentMissionValue++;
        }
        else
        {
            this.gameObject.SetActive(false);
        }

        AbleObjectives();
    }
    public void OnCollision()
    {

        if ( Missions[currentMissionValue].Type == missions_names[0])
        {
            MissionCompleted();
        }
        else if (Missions[currentMissionValue].Type == missions_names[2] && !entered)
        {
            paintWall();
            entered = true;
        }

    }
    

    private void AbleObjectives()
    {
        
        if (Missions[currentMissionValue].Type == missions_names[0])
        {
            Missions[currentMissionValue].Place.SetActive(true);
        }
        else if (Missions[currentMissionValue].Type == missions_names[1])
        {
            Missions[currentMissionValue].Place.SetActive(true);
        }
        else if (Missions[currentMissionValue].Type == missions_names[2])
        {
            Missions[currentMissionValue].Place.SetActive(true);
        }
        else
        {
            Missions[currentMissionValue].Place.SetActive(false);
        }
    }
}
