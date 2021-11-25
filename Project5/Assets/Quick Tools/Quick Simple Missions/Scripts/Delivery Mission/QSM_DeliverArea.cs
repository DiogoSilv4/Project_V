using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class QSM_DeliverArea : MonoBehaviour
{
    [Tooltip("Assign this to the Delivery Mission from the scene. Example collect and apples and deliver them here")]
    public QSM_DeliveryMissionControl RelatedMission;

    [Header("Inform Player")]
    [Tooltip("The amount of the time the message will display before going away.")]
    public int DisplayTime;

    [Tooltip("On Trigger enter a message will be displayed to the player to inform them they have not collected all required items to progrss.")]
    public string InformPlayer = "You haven't collected all require items yet";

    [Tooltip("The UI text gameobject that we will change at runtime.")]
    public GameObject Text_UI_GameObject;

    [Tooltip("This is the text UI that we will change at runtime")]
    public Text Text_DisplayMessage;

    public void OnEnable()
    {
        if (RelatedMission == null)
        {
            Debug.LogWarning("There is no Mission related to this Deliver Area. Please create one or link it in the Inspector!");
        }

        if (Text_UI_GameObject == null)
            Debug.LogWarning("Their is no Text_UI_Gameobject assign to the Delievery Area please assign it in the Inspector!");

        //Turning off object until we need it.
        Text_UI_GameObject.SetActive(false);

    }

    void OnTriggerEnter(Collider Other)
    {
        if (Other.gameObject.tag == "Player")
        {
            if(RelatedMission.TryCompleteMission())
            {
                gameObject.SetActive(false);

                RelatedMission.GetComponent<QSM_MissionMessenger>().ThisMissionIsCompleted = true;
            }
            else
            {
                //Player did not meet requirements show message.
                StartCoroutine(MessageDisplayer());
            }

        }
    }

    void InformText()
    {
        Text_DisplayMessage.text = InformPlayer;
    }

    IEnumerator MessageDisplayer()
    {
        //Display message for player
        Text_UI_GameObject.SetActive(true);

        //Change text at runtime.
        InformText();

        //Start timer
        yield return new WaitForSeconds(DisplayTime);

        //Turn off display message for player
        Text_UI_GameObject.SetActive(false);
    }
}
