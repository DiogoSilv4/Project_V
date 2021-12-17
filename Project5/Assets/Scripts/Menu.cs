using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    public bool isOpen = false;
    [SerializeField]
    private GameObject menu;

    [SerializeField]
    private GameObject menun_UI;


    private PlayerMovement PlayerScript;

    
    private Transform UI;

    [SerializeField]
    private Transform placeToBe;

    [SerializeField]
    private GameObject godUI;
    [SerializeField]
    private GameObject script_god;

    // Start is called before the first frame update
    void Start()
    {
        PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        UI = menu.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && isOpen == false)
        {
            isOpen = true;
            OpenMenu();

        }else if (Input.GetKeyDown(KeyCode.M) && isOpen == true)
        {
            isOpen = false;
            CloseMenu();
        }
    }
    void OpenMenu()
    {
        if (godUI.active)
        {
            script_god.GetComponent<GodCan>().closeGodUI();
        }

        UI.position = placeToBe.position;
        UI.rotation = placeToBe.rotation;

        menu.SetActive(true);
        if (menu.active)
        {
            EventSystem.current.SetSelectedGameObject(menun_UI);
        }

        PlayerScript.canWalk = false;
        PlayerScript.canLook = false;
    }
    public void CloseMenu()
    {
        menu.SetActive(false);

        PlayerScript.canWalk = true;
        PlayerScript.canLook = true;

    }
}
