using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSelectionMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
