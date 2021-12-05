using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GodCan : MonoBehaviour
{
    
    private PlayerMovement plyrScpt;

    [SerializeField]
    private Menu menuScript;



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

        plyrScpt = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

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
            

            //Debug.Log("OpenUI");
            plyrScpt.canWalk = !plyrScpt.canWalk;
            plyrScpt.canLook = !plyrScpt.canLook;

        }
        else if (!thisOne)
        {
            GodCanUI.SetActive(false);

        }else if(!thisOne && !menuScript.isOpen)
        {
            plyrScpt.canWalk = true;
            plyrScpt.canLook = true;
        }

        background.GetComponent<Image>().color = picker.color;
        this.GetComponent<canScript>().CanColor = picker.color;

    }
}
