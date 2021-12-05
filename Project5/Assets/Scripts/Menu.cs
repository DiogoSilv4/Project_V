using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public bool isOpen = false;
    [SerializeField]
    private GameObject menu;

    
    private PlayerMovement PlayerScript;


    [SerializeField]
    private Transform UI;

    [SerializeField]
    private Transform placeToBe;

    

    // Start is called before the first frame update
    void Start()
    {
        PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
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
        PlayerScript.canLook = false;
    }
    void CloseMenu()
    {
        menu.SetActive(false);

        PlayerScript.canWalk = true;
        PlayerScript.canLook = true;

    }
}
