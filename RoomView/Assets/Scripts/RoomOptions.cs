using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomOptions : MonoBehaviour {


    
    public SteamVR_TrackedController controllerInput;
    private SteamVR_LaserPointer pointer;
   
    private bool optionsDisplaying = false;
    private bool isSelected = false;
    private bool buttonSelected = false;

    void Awake()
    {
   
        controllerInput.MenuButtonClicked += displayInRoomMenu;
        gameObject.SetActive(false);
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
       transform.position = Camera.main.transform.position + Camera.main.transform.forward * 3.0f;
      
        optionsDisplaying = true;
    }
    public void optionsOff()
    {
        transform.position = new Vector3(0.0f, -100.0f, 0.0f);
        gameObject.SetActive(false);
        optionsDisplaying = false;

    }


}
