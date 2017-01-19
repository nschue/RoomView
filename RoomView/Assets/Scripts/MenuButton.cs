using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour {

    public SteamVR_TrackedObject controller;
    
    public LevelManager levelManager;
    public string loadLevel;


    private SteamVR_TrackedController controllerInput;
    private SteamVR_LaserPointer pointer;
	// Use this for initialization
	void Awake () {
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

    // Update is called once per frame
    void Update () {
		
	}

    void OnHover(object sender, PointerEventArgs e)
    {
        if(e.target == GetComponent<Collider>().transform)
        {
            gameObject.GetComponent<Text>().color = Color.green;
            controllerInput.TriggerUnclicked += OnSelectButton;
        }
    }

    void OffHover(object sender, PointerEventArgs e)
    {
        if(e.target == GetComponent<Collider>().transform)
        {
            gameObject.GetComponent<Text>().color = Color.black;
            controllerInput.TriggerUnclicked -= OnSelectButton;
        }
    }

    void OnSelectButton(object sender, ClickedEventArgs e)
    {
        levelManager.LoadLevel(loadLevel);
    }
}
