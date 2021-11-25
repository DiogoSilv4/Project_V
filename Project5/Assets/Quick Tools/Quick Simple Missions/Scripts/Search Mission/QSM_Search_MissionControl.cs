using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QSM_Search_MissionControl : MonoBehaviour 
{
	public bool SearchMissionCompleted;

	[Tooltip("After mission is compeleted notfy the player!")]
	public bool UseNotification;

	[Tooltip("A Image on the UI Canvas that will notify the player")]
	public GameObject NotifyImage;

	[Tooltip("Here is where you the describe the Destoy Missions details here")]
	[TextArea]
	public string SearchMissionInfo = "";

	[Tooltip("The text on the UI Canvas that we will update on start")]
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
				Debug.LogWarning ("There is no notification UI please create one or link in the Inspector for the Search Mission!");
			} 
			else
			{
				NotifyImage.SetActive (false);
			}

		}
	}

	void Start()
	{

		//If there is no data for the UI we will notify you
		if (UIMissionInformationForUI == null) 
		{
			Debug.LogWarning ("No UI in use no information will be displayed");
		} 
		else 
		{
			//Send Info to UI
			UIMissionInformationForUI.text = SearchMissionInfo;

		}

		//If there is no data for the UI we will notify you
		if (SearchMissionInfo == null) 
		{
			Debug.LogWarning ("There is no information for the Destory Mission Info");
		}

		//Getting reference to the script
		_MissionMessenger = gameObject.GetComponent<QSM_MissionMessenger>();


	}

	void LateUpdate()
	{
		//The Destroy Mission is completed
		if (SearchMissionCompleted == true) 
		{
			//Search mission is Completd
			_MissionMessenger.ThisMissionIsCompleted = true;

			//We will dim the text as we have completed the mission
			UIMissionInformationForUI.color = ChangeColorAfterMissionComplete;

			//We completed the destory mission and now will turn if off
			gameObject.SetActive (false);
		}

		//If we notify the player and the Destory Mission is completed notify the Master Control
		if (SearchMissionCompleted && UseNotification == true) 
		{
			//Search mission is Completd
			_MissionMessenger.ThisMissionIsCompleted = true;

			//Setting Notifacation true
			NotifyImage.SetActive (true);

			//We will dim the text as we have completed the mission
			UIMissionInformationForUI.color = ChangeColorAfterMissionComplete;

			//We completed the destory mission and now will turn if off
			gameObject.SetActive (false);
		}
	}
}
