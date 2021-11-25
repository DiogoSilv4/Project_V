using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class QSM_Travel_WayPointTrigger : MonoBehaviour 
{
	public QSM_Travel_Mission TravelMissionScript;
	public bool Started;
	public bool HalfWay;
	public bool Finished;

	public GameObject Player;

	private BoxCollider Triggerbox;

	// Use this for initialization
	void Start() 
	{
		//
		Player = GameObject.FindGameObjectWithTag ("Player");

		//Getting reference to the Travel Mission

		//Getting reference to out box collider
		Triggerbox = gameObject.GetComponent<BoxCollider>();

		//Just in encase we forget to set the trigger
		Triggerbox.isTrigger = true;

		//Turn off the mesh to the trigger during runtime
		gameObject.GetComponent<MeshRenderer>().enabled = false;
	}

	void OnTriggerEnter(Collider Player)
	{
		//Trigger
		if 
			(Player.tag == "Player") 
		{
			//If this is the started Trigger set to true nofity Travel Mission Controller
			if (Started == true) 
			{
				TravelMissionScript.IS_Started_Trigger = true;
				Debug.Log("Player has started travel mission");
				gameObject.SetActive (false);
			}

			//If this is the HalfWay Trigger set to true nofity Travel Mission Controller
			if (HalfWay == true)
			{
				TravelMissionScript.IS_Halfway_Trigger = true;
				Debug.Log("Player has made it halfway with travel mission");
				gameObject.SetActive (false);
			}

			//If this is the Finished Trigger set to true nofity Travel Mission Controller
			if (Finished == true) 
			{
				TravelMissionScript.IS_Finished_Trigger = true;
				Debug.Log("Player has finished travel mission");
				gameObject.SetActive (false);
			}
		}
		//Trigger
	}
}
