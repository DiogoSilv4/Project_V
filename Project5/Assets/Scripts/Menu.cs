using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private bool isOpen = false;
    [SerializeField]
    private GameObject menu;

    [SerializeField]
    private PlayerMovement PlayerScript;

    [SerializeField]
    private mouseLook LookScript;

    [SerializeField]
    private Transform UI;

    [SerializeField]
    private Transform placeToBe;

    // Start is called before the first frame update
    void Start()
    {
        
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
        UI.position = placeToBe.position;
        UI.rotation = placeToBe.rotation;


        menu.SetActive(true);
        PlayerScript.canWalk = false;
        LookScript.canLook = false;
    }
    void CloseMenu()
    {
        menu.SetActive(false);
        PlayerScript.canWalk = true;
        LookScript.canLook = true;

    }
}
