using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomOptions : MonoBehaviour {


    
    public SteamVR_TrackedController controllerInput;
    private SteamVR_LaserPointer pointer;

    private GameObject menuManager;
    private bool optionsDisplaying = false;
    private bool isSelected = false;
    private bool buttonSelected = false;
    private bool catManIsActive;

    void Awake()
    {
        controllerInput.MenuButtonClicked += displayInRoomMenu;
        gameObject.SetActive(false);
    }

    void Start()
    {
        menuManager = GameObject.Find("MenuManager"); // not in use newDemo scene currently 4/12/17 gives exception
        if(menuManager == null)
        {
            Debug.LogError("menuManager not found");
        }
    }

    public virtual void displayInRoomMenu(object sender, ClickedEventArgs e)
    {
        
        if (optionsDisplaying)
        {
            optionsOff();
        }
        else
        {
            optionsOn();
        }
    }
    public void Update()
    {
        transform.LookAt(2 * transform.position - Camera.main.transform.position);
    }

    public virtual void onSelectClick(object sender, ClickedEventArgs e)
    {

    }
    public virtual void offSelectClick(object sender, ClickedEventArgs e)
    {


    }
    public void optionsOn()
    {      
        gameObject.SetActive(true);

        //catManIsActive = menuManager.GetComponent<MenuManager>().catalogActive; //Get current state of catalogManager
        //Working on making a MenuManager to store all of the current states of our menus, trying close all menus when the pause menu is opened. 
        //When the pause menu is closed, reopen whatever menus were open again.
        //if (catManIsActive)
        //{
        //    menuManager.GetComponent<CatalogManager>().catOff();//If active, turn it off
        //}
        transform.position = Camera.main.transform.position + Camera.main.transform.forward * 6.0f;
      
        optionsDisplaying = true;
        raycastIgnoreOtherObjects();
    }
    public void optionsOff()
    {
        transform.position = new Vector3(0.0f, -100.0f, 0.0f);
        gameObject.SetActive(false);
        optionsDisplaying = false;
        raycastHitOtherObjects();

        if (catManIsActive)
        {
            //prefabCatalog.GetComponent<CatalogManager>().catOn();//If it was active, turn it back on
        }
    }


    //Move all GameObjects to IgnoreRaycast layer
    private void raycastIgnoreOtherObjects()
    {
        GameObject[] sceneObjects = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject gObject in sceneObjects)
        {
            GameObject parentObject;
            Transform parentTransform = gObject.transform;
            while (parentTransform.parent != null)
            {
                parentTransform = parentTransform.parent;
            }
            parentObject = parentTransform.gameObject;
            if (parentObject != gameObject)
            {
                gObject.layer = 2;
            }
        }
    }

    //Move all GameObjects back to default layer
    private void raycastHitOtherObjects()
    {
        GameObject[] sceneObjects = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject gObject in sceneObjects)
        {
            GameObject parentObject;
            Transform parentTransform = gObject.transform;
            while (parentTransform.parent != null)
            {
                parentTransform = parentTransform.parent;
            }
            parentObject = parentTransform.gameObject;
            if (parentObject != gameObject)
            {
                if (parentObject.GetComponent<Furniture>() != null && parentObject.GetComponent<Furniture>().isMove)
                {
                    gObject.layer = 2;
                }
                else
                    gObject.layer = 0;
            }
        }
    }

}
