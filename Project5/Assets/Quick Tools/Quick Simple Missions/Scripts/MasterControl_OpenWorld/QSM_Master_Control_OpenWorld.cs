using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class QSM_Master_Control_OpenWorld : MonoBehaviour 
{
	[Tooltip("Is this connect to an open world? If true the mission will not load new level.")]
	public bool Open_World_Area;
	
	[Header("Load Level when all complete")]
	[Tooltip("If All missions are complete load a level")]
	public bool AfterCompletedLoadLevel;

	[Tooltip("Instead of just a instance load to the next scene, We will enable a Trigger Gameobject in the scene")]
	public bool LoadLevelWithTriggerInstead;

	[Tooltip("A GameObject that player will walk to trigger an exit")]
	public GameObject TriggerLevelLoader;

	[Header("Name of Level to load")]
	[Tooltip("Type in the scene you wish to load")]
	public string LevelToLoad = "Example Leve1 to Load";

	[Header("All Missions are done in this Area")]
	[Tooltip("If area is completed we may load level or say the area is completed")]
	public bool Area_Completed;

	[Header("All Current Missions")]
	[Tooltip("List All Current Mission in the area")]
	public QSM_MissionMessenger[] CurrentMissions;

	void Start()
	{
		//Warn the user
		if (CurrentMissions == null) 
		{
			Debug.LogWarning("You didn't assign anything in the missions in the Master Control Open world please do so now");
		}
	}


	//Checking to see if all mission in current area have been set to completed
	public bool All_Area_Missions_Completed ()
	{
		{
			for( int i = 0; i < CurrentMissions.Length; ++i)
			{
				if(CurrentMissions[i].ThisMissionIsCompleted == false)
				{
					return false;
				}
			}

			return true;
		}
	}
		
	void LateUpdate()
	{
		//All the missions are completed in the area or level
		if (CurrentMissions.All (CurrentMissions => CurrentMissions.ThisMissionIsCompleted)) 
		{
			//Area set to complete
			Area_Completed = true;
		}

		if (Area_Completed == true) 
		{
			//If we are not loadig with a trigger
			if (LoadLevelWithTriggerInstead == false) 
			{
				SceneManager.LoadScene (LevelToLoad);
			}

			//If we decide to use a Trigger to load the next level instead of automatically after the player completed everything
			if (LoadLevelWithTriggerInstead== true) 
			{
				//Setting Our Load Trigger Active
				TriggerLevelLoader.SetActive (true);
			}
		}
	}
}
