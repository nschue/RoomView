using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomOptions : MonoBehaviour {

    public SteamVR_TrackedObject controller;
    public SteamVR_TrackedController controllerInput;
    public SteamVR_LaserPointer pointer;
    public GameObject gameOptionsPrefab;
    private bool optionsDisplaying = false;


    void Awake()
    {
       controller = GetComponent<SteamVR_TrackedObject>(); 
        if (controller != null)
        {
            try
            {
                pointer = controller.GetComponent<SteamVR_LaserPointer>();
                controllerInput = controller.GetComponent<SteamVR_TrackedController>();
            }
            catch
            {
                Debug.LogError(this.name + ": Could not find LaserPointer");
            }
        }
    }
    private void OnEnable()
    {
         controllerInput.MenuButtonClicked += displayInRoomMenu;

    }
    
 
    public virtual void displayInRoomMenu(object sender, ClickedEventArgs e)
    {
        
        if (!optionsDisplaying)
        {
            GameObject contextMenu = Instantiate(gameOptionsPrefab, new Vector3(5, 5, 5),
            Quaternion.identity) as GameObject;
            contextMenu.GetComponent<UI_Follower>().mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
           contextMenu.GetComponent<UI_Follower>().setSnappedObject(gameObject);
        }
    
    }
    

}
