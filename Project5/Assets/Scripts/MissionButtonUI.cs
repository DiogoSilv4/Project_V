using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionButtonUI : MonoBehaviour
{
    [SerializeField] private Mission script_mission;

    [SerializeField]
    private Text mission_stat;

    [SerializeField]
    private GameObject left;
    [SerializeField]
    private GameObject right;
    [SerializeField]
    private GameObject mission_bg;

    private Button _left;
    private Button _right;

    private string[] _missions;
    private int showCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        _left = left.GetComponent<Button>();
        _right = right.GetComponent<Button>();

        //for (int i = 0; i < script_mission.Missions.Count; i++)
        //{
        //    _missions[i] = script_mission.Missions[i].Name;
        //}
        
    }

    // Update is called once per frame
    void Update()
    {
        _left.onClick.AddListener(onRightClick);
        _right.onClick.AddListener(onLeftClick);

        mission_stat.text = script_mission.Missions[showCount].Name;

        if (script_mission.Missions[showCount].isCompleted  )
        {
            mission_bg.SetActive(true);
        }
        else
        {
            mission_bg.SetActive(false);
        }


        if (showCount == 0)
        {
            left.SetActive(false);
        }
        else
        {
            left.SetActive(true);
        }
        if (showCount == script_mission.Missions.Count - 1)
        {
            right.SetActive(false);
        }
        else
        {
            right.SetActive(true);
        }

    }

    void onLeftClick()
    {
        showCount--;
    }
    void onRightClick()
    {
        showCount += 1;
    }
}
