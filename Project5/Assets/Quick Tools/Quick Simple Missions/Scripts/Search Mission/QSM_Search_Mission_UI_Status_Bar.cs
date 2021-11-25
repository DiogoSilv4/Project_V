using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// A simple world space canvas showing a status on the search missiong being completed

public class QSM_Search_Mission_UI_Status_Bar : MonoBehaviour 
{
	[Tooltip("Link this to the trigger")]
	public QSM_Search_Mission_Trigger Search_Mission_Trigger_Script;

	[Tooltip("The amount of time that is on the Trigger to search the area")]
	public Slider TimeRemaining;

	void Start()
	{
		//Match the value
		TimeRemaining.maxValue = Search_Mission_Trigger_Script.Player_Must_Hold_down_key_this_many_seconds;
	}


	void Update()
	{
		//We will update the status of our UI bar
		TimeRemaining.value = Search_Mission_Trigger_Script.Player_Must_Hold_down_key_this_many_seconds;
	}
}
