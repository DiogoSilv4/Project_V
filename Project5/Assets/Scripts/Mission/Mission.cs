using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission : MonoBehaviour
{
    //[SerializeField] private Text UI_Mission_Stat;

    [SerializeField]
    private GameObject sphere_prefab;
    
    
    public List<Mission_things> Missions;


    private float distance;
    private int currentMissionValue = 0;

    private Texture wall;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Missions[currentMissionValue].Type == "delivery")
        {
            currentMission();
        }else if (Missions[currentMissionValue].Type == "paint")
        {
            
            if (checkDistance(Missions[currentMissionValue].Objects[1], Missions[currentMissionValue].Place.transform))
            {
                
                paintWall();
                
            }
               
        }
        
    }

    private void paintWall()
    {
        wall = Missions[currentMissionValue].Objects[0].GetComponent<Renderer>().material.GetTexture("_MainText");
        Texture2D _wall = (Texture2D)wall;
        var pix_count = 0;
        Debug.Log("yyyy");
        for (int x = 0; x < wall.width; x++)
        {
            for (int y = 0; y < wall.height; y++)
            {
                Color pix = _wall.GetPixel(x, y);
                if (pix != Color.white)
                {
                    pix_count++;
                }
            }
        }
        if (pix_count > 1)
        {
            Debug.Log("Completed");
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
            
            if (checkDistance(Missions[currentMissionValue].Objects[i], Missions[currentMissionValue].Place.transform))
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

    private void MissionCompleted()
    {
        Debug.Log("Mission_Completed");
        Missions[currentMissionValue].isCompleted = true;
        if (Missions.Count > currentMissionValue )
        {
            currentMissionValue++;
        }
        
    }
}
