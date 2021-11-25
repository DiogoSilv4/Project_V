using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GodCan : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement plyrScpt;
    [SerializeField]
    private mouseLook LookScript;

    private bool CanGrabbed;
    private bool thisOne = false;
    private GameObject can;

    public FlexibleColorPicker picker;
    public GameObject background;

    [SerializeField]
    private GameObject GodCanUI;

    [SerializeField]
    private GameObject eventS;

    [SerializeField]
    private Transform UI;

    [SerializeField]
    private Transform placeToBe;

    

    // Start is called before the first frame update
    void Start()
    {
        picker.color = Color.yellow;

    }

    // Update is called once per frame
    void Update()
    {
        CanGrabbed = plyrScpt.IsCanGrabbed;
        can =  plyrScpt.currentCan;
        
        if (can == this.gameObject && CanGrabbed)
        {
            thisOne = true;
            //Debug.Log("GodCan");
        }
        else
        {
            thisOne = false;
        }

        if (thisOne && Input.GetKeyDown(KeyCode.N))
        {
            UI.position = placeToBe.position;
            UI.rotation = placeToBe.rotation;


            GodCanUI.SetActive(!GodCanUI.activeInHierarchy);
            //eventS.SetActive(!eventS.activeInHierarchy);
            

            //Debug.Log("OpenUI");
            plyrScpt.canWalk = !plyrScpt.canWalk;
            LookScript.canLook = !LookScript.canLook;

        }
        else if (!thisOne )
        {
            GodCanUI.SetActive(false);
            //eventS.SetActive(false);

            plyrScpt.canWalk = true;
            LookScript.canLook = true;

        }

        background.GetComponent<Image>().color = picker.color;
        this.GetComponent<canScript>().CanColor = picker.color;

    }
}
