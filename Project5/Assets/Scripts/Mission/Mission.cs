using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission : MonoBehaviour
{
    //[SerializeField] private Text UI_Mission_Stat;


    public List<Mission_things> Missions;


    private float distance;
    private int currentMissionValue = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentMission();
    }



    private void currentMission()
    {
        //UI_Mission_Stat.text = Missions[currentMissionValue].Name;

        var count = 0;
        for(int i = 0; i < Missions[currentMissionValue].Objects.Length; i++)
        {
            
            if (checkDistance(Missions[currentMissionValue].Objects[i], Missions[currentMissionValue].Place))
            {
                count++; 
            }
        }
        if (count == Missions[currentMissionValue].Objects.Length)
        {
            MissionCompleted();
        }

        //Debug.Log(Missions.Count);

        
    }

    private bool checkDistance(GameObject _object, Transform place)
    {
        distance = Vector3.Distance(_object.transform.position, place.position);

        if (distance < 1.0f)
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
        currentMissionValue++;
    }
}
