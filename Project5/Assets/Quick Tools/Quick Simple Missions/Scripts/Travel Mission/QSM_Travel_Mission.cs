using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class QSM_Travel_Mission : MonoBehaviour 
{
	private QSM_TravelMissionControl TravelMissionControl_Script;

	[Header("Start Trigger")]
	[Tooltip("This is Started Trigger it will let Travel mission know if the has player started through the travel")]
	public bool IS_Started_Trigger;

	[Header("Halfway Trigger")]
	[Tooltip("This is Halfway Trigger it will let Travel mission know if the player has made it Half through the travel")]
	public bool IS_Halfway_Trigger;

	[Header("Finish Trigger")]
	[Tooltip("This is Finished Trigger it will let Travel mission know if the player is about to finish the travel")]
	public bool IS_Finished_Trigger;

	//This Mission has been completed if set to true
	[Header("Mission Compelted")]
	[Tooltip("If true then this mission is compeleted")]
	public bool TravelCompelete;

	[Header("Reward")]
	[Tooltip("Do we want to reward the player for the mission after it is completed?")]
	public bool RewardPlayer;

	[Tooltip("A tranform in the 3d world space that will spawn the reward")]
	public Transform RewardLocation;

	[Tooltip("The GameObject/Prefab that will be awarded to the player after completion of the mission")]
	public GameObject Reward;

	[Header("Audio Controls")]
	[Tooltip("This sound will play if all triggers are true")]
	public AudioClip MissionCompeletedSound;

	[Tooltip("How loud will our alert be for the player?")]
	[Range(0.0f,10.0f)]
	public float VolumeOfAlert = 1.0f;

	[Tooltip("Do we want to alert the player with a sound that they completed the mission, if so set to true")]
	public bool PlaySoundOnLastTrigger;

	private AudioSource OurAudioSourceOnGameObject;

	void Start ()
	{
		//Getting reference to the AudioSource
		OurAudioSourceOnGameObject = gameObject.GetComponent<AudioSource> ();

		//Getting reference to the TraveLMission on the GameObject
		TravelMissionControl_Script = gameObject.GetComponent<QSM_TravelMissionControl> ();

		if (RewardPlayer == true) 
		{
			if (Reward == null) {
				Debug.LogWarning ("There is no gameobject in the reward gameobject slot please besure to fill the slot if you want the player to be rewarded.");
			}
			else 
			{
				//Setting reward as inactive
				Reward.SetActive (false);
			}

		}
	}

	// Update is called once per frame
	void Update () 
	{
		//If all the trigger have been triggered
		if (IS_Started_Trigger && IS_Halfway_Trigger && IS_Finished_Trigger == true) 
		{
			//All triggers have been set to true now finish the mission
			TravelCompelete =true;

			if (TravelCompelete == true) 
			{
				OurAudioSourceOnGameObject.PlayOneShot (MissionCompeletedSound, VolumeOfAlert);

				//Do we wish to reward the player for their travels? If ture
				if (RewardPlayer == true) 
				{
					//Give reward
					Reward.SetActive (true);

					//Game transform 
					Reward.transform.position = RewardLocation.transform.position;

					//Turning off now that the reward has appeart
					RewardPlayer = false;

					//We completed the mission and now we will turn off the gameobject
					gameObject.SetActive (false);
				}

				//Disable the audio now
				OurAudioSourceOnGameObject.mute = true;

				//The Mission has been completed notfiy the Master Control
				TravelMissionControl_Script.AllRequiredTravelMissionsAreDone = true;
			}
		}
	}
}
