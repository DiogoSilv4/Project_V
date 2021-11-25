using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QSM_TravelMissionControl : MonoBehaviour 
{
	[Tooltip("If true we will alert Master Control that this Travel Mission is completed")]
	public bool AllRequiredTravelMissionsAreDone;

	[Tooltip("After mission is compeleted notify the player!")]
	public bool UseNotification;

	[Tooltip("A UI image on a canvas we will nofity the player with")]
	public GameObject NotifyImage;

	[Tooltip("Here you describe the Travel Missions detail")]
	[TextArea]
	public string TravelMissionInfo = "";

	[Tooltip("This the text that will be use for our Travel Missions detail on the Canvas")]
	public Text UIMissionInformationForUI;

	[Tooltip("After the mission is complete you can gray out or tone down the color to show its completed")]
	public Color ChangeColorAfterMissionComplete;

	private QSM_MissionMessenger _MissionMessenger;

	void OnEnable()
	{

		if (UseNotification == true) 
		{
			if (NotifyImage == null)
			{
				Debug.LogWarning ("There is no notification UI please create one or link in the Inspector for the Travel Mission!");
			} 
			else
			{
				NotifyImage.SetActive (false);
			}

		}
	}

	void Start () 
	{	

		if (UIMissionInformationForUI == null) 
		{
			Debug.LogWarning ("No UI in use no information will be displayed TRAVEL MISSION");
		} 
		else 
		{
			//Updating the string on the Canvas
			UIMissionInformationForUI.text = TravelMissionInfo;
		}

		if (TravelMissionInfo == null) 
		{
			Debug.LogWarning ("There is no information for the Destory Mission Info");
		}
		_MissionMessenger = gameObject.GetComponent<QSM_MissionMessenger> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Since are Triggers are completed then we will alert the Master Control
		if (AllRequiredTravelMissionsAreDone == true) 
		{
			//This Mission is completed
			_MissionMessenger.ThisMissionIsCompleted = true;

			//We will dim the text as we have completed the mission
			UIMissionInformationForUI.color = ChangeColorAfterMissionComplete;

			//Travel mission is completed we will turn off the object
			gameObject.SetActive(false);
		}

		//Since are Triggers are completed then we will alert the Master Control and Alert the player with UI GameObject on the Canvas
		if (AllRequiredTravelMissionsAreDone && UseNotification == true) 
		{
			//This Mission is completed
			_MissionMessenger.ThisMissionIsCompleted = true;

			//We will dim the text as we have completed the mission
			UIMissionInformationForUI.color = ChangeColorAfterMissionComplete;

			NotifyImage.SetActive (true);
		}
	}
}
