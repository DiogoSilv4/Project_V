using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QSM_Search_Mission_Status : MonoBehaviour
{
	private QSM_Search_MissionControl Search_Mission_Control_Script;

	public QSM_Search_Mission_Trigger[] Number_Of_Areas_Still_To_Be_Searched;

	void Start()
	{
		//Getting reference to the Search Mission Control
		Search_Mission_Control_Script = GetComponent<QSM_Search_MissionControl> ();
	}

	//Checking to see if all mission in current area have been set to completed
	public bool All_Area_Missions_Completed ()
	{
		{
			for( int i = 0; i < Number_Of_Areas_Still_To_Be_Searched.Length; ++i)
			{
				if(Number_Of_Areas_Still_To_Be_Searched[i].SearchAreaTrigger == false)
				{
					return false;
				}
			}

			return true;
		}
	}

	void Update()
	{
		if (Number_Of_Areas_Still_To_Be_Searched.All (Number_Of_Areas_Still_To_Be_Searched => Number_Of_Areas_Still_To_Be_Searched.SearchAreaTrigger)) 
		{
			//Area set to complete
			Search_Mission_Control_Script.SearchMissionCompleted = true;
		}

	}

}
