using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QSM_DeliveryMissionControl : MonoBehaviour
{
    public bool DeliveryMissionCompeleted => _objectiveTracker.AreObjectivesComplete;

    [Tooltip("If true mission is completed")]
    public bool Delivery_Mission_Completed;

	[Tooltip("After mission is compeleted notfy the player!")]
	public bool UseNotification;

	//Using a UI image to nofity the player
	public GameObject NotifyImage;

	//Here you will write information about the mission
	[Tooltip("Describe the Delivery Missions details here")]
	[TextArea]
	public string DelieveryMissionInfo = "";
	public Text UIMissionInformationForUI;

	[Space(1)]
	[Tooltip("This will add or subtract a text during game play")]
	public Text UIMissionInformationForUI_UpDate_Count_DeliveryMission;

	[Tooltip("After the mission is complete you can gray out or tone down the color to show its completed")]
	public Color ChangeColorAfterMissionComplete;

    public List<QSM_Objective> ItemObjectives;
    private QSM_ObjectiveTracker _objectiveTracker;

    [Tooltip("The amount of items the player is carrying.")]
    public int InventoryAmount;

	void OnEnable()
	{

		if (UseNotification == true) 
		{
			if (NotifyImage == null)
			{
				Debug.LogWarning ("There is no notification UI please create one or link in the Inspector for the Delivery Mission!");
			} 
			else
			{
				NotifyImage.SetActive (false);
			}

		}
	}

	void Start () 
	{
        _objectiveTracker = new QSM_ObjectiveTracker(ItemObjectives);

		if (UIMissionInformationForUI == null) 
		{
			Debug.LogWarning ("No UI in use no information will be displayed");
		} 
		else 
		{
			//On Start up we will update UI text with the mission details
			UIMissionInformationForUI.text = DelieveryMissionInfo;
		}

		if (UIMissionInformationForUI == null) 
		{
			Debug.LogWarning ("There is no information for the Delivery Mission Info");
		}

		if (UIMissionInformationForUI_UpDate_Count_DeliveryMission == null) 
		{
			Debug.Log ("No information is being sent using the UI");
		}

        if (_objectiveTracker.GetUIReference(item:name))
        {
            Debug.LogWarning("Item not set to a UI refrence");
        }
	}

    /// <summary>
    /// Registers the item to the corresponding objective, if found.
    /// </summary>
    /// <param name="itemName">The name of the item</param>
    /// <param name="amount">The amount picked up.</param>
    /// <returns>Returns false if the item was not found on this objective, otherwise true.</returns>
    public bool PickupItem(string itemName, int amount = 1)
    {
        return _objectiveTracker.Additem(itemName, amount) >= 0;
    }

    /// <summary>
    /// Checks to see if all objectives are complete. If true, it registers the mission as complete.
    /// </summary>
    /// <returns>Returns true if the mission is complete, otherwise false.</returns>
    public bool TryCompleteMission()
    {
        if (!_objectiveTracker.AreObjectivesComplete)
            return false;

        //Mission is completed
        Delivery_Mission_Completed = true;

        //We will dim the text as we have completed the mission
        UIMissionInformationForUI.color = ChangeColorAfterMissionComplete;

        //Turn On the image to notify the player, if UseNotification is enabled
        if (UseNotification)
            NotifyImage.SetActive(true);

        //Collect Mission is completed we will turn off the game
        gameObject.SetActive(false);

        return true;
    }

    void Update()
    {

        //We will check inventory to see what the player has
        UIMissionInformationForUI_UpDate_Count_DeliveryMission.text = InventoryAmount.ToString();

        if (Delivery_Mission_Completed == true)
        {
            //Turn On the image
            NotifyImage.SetActive(true);

            // Change inventory
            InventoryAmount = 0;
        }

        if (UseNotification && Delivery_Mission_Completed== true)
        {
            if (Delivery_Mission_Completed == true)
            {
                //Turn On the image
                NotifyImage.SetActive(true);

                // Change inventory
                InventoryAmount = 0;
            }
        }
    }

    public void AddItemUI()
    {
        //Add item from pick up
        InventoryAmount++;

        //Update count
        UIMissionInformationForUI_UpDate_Count_DeliveryMission.text = InventoryAmount.ToString();
    }
}
