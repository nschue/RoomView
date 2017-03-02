using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour {

    public SteamVR_TrackedObject controller;

    [HideInInspector]
    public SteamVR_TrackedController controllerInput;
    [HideInInspector]
    public SteamVR_LaserPointer pointer;
    

    void Awake () {
        GameObject controllerObject = GameObject.FindGameObjectWithTag("Right Controller") as GameObject;
        if (controllerObject != null & controller == null)
            controller = controllerObject.GetComponent<SteamVR_TrackedObject>();
        else
            Debug.Log("controllerObject returned null");
	    if(controller !=null )
        {
            try
            {
                pointer = controller.GetComponent<SteamVR_LaserPointer>();
                controllerInput = controller.GetComponent<SteamVR_TrackedController>();
            }
            catch
            {
                Debug.LogError(this.name +": Could not find LaserPointer");
            }
        }
	}

    private void OnEnable()
    {
        pointer.PointerIn += OnHover;
        pointer.PointerOut += OffHover;
    }


    public virtual void OnHover(object sender, PointerEventArgs e)
    {
        if(e.target == GetComponent<Collider>().transform)
        {
            gameObject.GetComponent<Text>().color = Color.blue;
            controllerInput.TriggerClicked += OnSelectButton;
        }
    }

    public virtual void OffHover(object sender, PointerEventArgs e)
    {
        if(e.target == GetComponent<Collider>().transform)
        {
            gameObject.GetComponent<Text>().color = Color.black;
            controllerInput.TriggerClicked -= OnSelectButton;
        }
    }

    public virtual void OnSelectButton(object sender, ClickedEventArgs e)
    {
        Debug.LogError(this.name + ": No OnSelectButton method has been defined");
    }
}
