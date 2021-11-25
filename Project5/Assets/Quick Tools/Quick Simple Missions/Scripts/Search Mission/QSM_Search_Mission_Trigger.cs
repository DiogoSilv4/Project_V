using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class QSM_Search_Mission_Trigger : MonoBehaviour
{
	[Tooltip("If this area has been Searched we will deactivate it")]
	public bool SearchAreaTrigger;

	[Tooltip("After the player searches this are will they recieve a reward or require collectable")]
	public bool Reward_After_Search;

	[Header("Player Reward")]
	[Tooltip("The gameobject,collectable or reward that spawn after area has been searched;")]
	public GameObject Reward_Prefab;

	[Tooltip("You can set the key that player must use to search the area")]
	public KeyCode KeyUseToSearchArea = KeyCode.F;

	[Tooltip("How long must the player hold down the key to compelet there seach?")]
	[Range(0,Mathf.Infinity)]
	public float Player_Must_Hold_down_key_this_many_seconds = 5;

	private MeshRenderer Mesh;

	private AudioSource _AudioSource;

	[Tooltip("How loud you will play a sound while searching")]
	[Range(0.0f,10.0f)]
	public float Volume;

	void Start()
	{
		//Getting reference to the mesh
		Mesh = gameObject.GetComponent<MeshRenderer> ();

		//turning off the mesh
		Mesh.enabled = false;

		//Getting Audiosource;
		_AudioSource = gameObject.GetComponent<AudioSource> ();

		//We don't want to play sound yet
		_AudioSource.enabled = false;

		if (Reward_After_Search == true)
		{
			if (Reward_Prefab == null) 
			{
				Debug.LogWarning ("There is no gameobject in the reward gameobject slot please besure to fill the slot if you want the player to be rewarded.");
			} 
			else 
			{
				//Setting reward as inactive
				Reward_Prefab.SetActive (false);
			}
		}
	}

	void Update()
	{
		//If count down is at zero set this trigger as completed
		if (Player_Must_Hold_down_key_this_many_seconds == 0) 
		{

			SearchAreaTrigger = true;
		}

		//Now the trigger is completed we will check to see if the player finds a reward here
		if (SearchAreaTrigger == true) 
		{
			/////////////////////////////////////////////////////////Player Has Reward
			if (Reward_After_Search == true) 
			{
				
				//Reward the player
				Reward_Prefab.SetActive(true);

				//Deactivating area
				gameObject.SetActive(false);
			}

			/////////////////////////////////////////////////////////Player Has NO Reward
			if (Reward_After_Search == false) 
			{
				//Deactivating area
				gameObject.SetActive(false);
			}
		}
	}

	void OnTriggerStay(Collider Player)
	{
		Player.tag = "Player";

		if(Input.GetKey (KeyUseToSearchArea)) 
		{
			SubtractTime ();

			_AudioSource.enabled = true;

			_AudioSource.volume = Volume;

			_AudioSource.loop = true;

		}

		if (Input.GetKeyUp (KeyUseToSearchArea)) 
		{
			_AudioSource.enabled = false;
		}
			
	}

	void SubtractTime()
	{
		Player_Must_Hold_down_key_this_many_seconds -= Time.deltaTime;


		//Setting a limit so its not go past zero.
		Player_Must_Hold_down_key_this_many_seconds = Mathf.Clamp (Player_Must_Hold_down_key_this_many_seconds, 0, Mathf.Infinity);
	}
		
}
