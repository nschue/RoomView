using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour {

    private SteamVR_TrackedObject controller;
    private SteamVR_TrackedController controllerInput;
    private SteamVR_Teleporter teleporter;
    private SteamVR_LaserPointer pointer;

	// Use this for initialization
	void Awake () {
        controller = GetComponent<SteamVR_TrackedObject>();
        controllerInput = GetComponent<SteamVR_TrackedController>();
        teleporter = GetComponent<SteamVR_Teleporter>();
        pointer = GetComponent<SteamVR_LaserPointer>();
	}

    void OnEnable()
    {
        controllerInput.PadClicked += EnableTeleport;
        controllerInput.PadUnclicked += DisableTeleport;
    }

    void Start()
    {
        teleporter.teleportOnClick = false;
        pointer.pointer.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update () {
        
	}

    private void EnableTeleport (object sender, ClickedEventArgs e)
    {
        teleporter.teleportOnClick = true;
        pointer.pointer.GetComponent<Renderer>().enabled = true;
    }

    private void DisableTeleport (object sender, ClickedEventArgs e)
    {
        teleporter.teleportOnClick = false;
        pointer.pointer.GetComponent<Renderer>().enabled = false;
    }
}
