using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public GameObject mainMenu;
    // Use this for initialization
    void Start()
    {
        GameObject menu = Instantiate(mainMenu);
        menu.transform.parent = gameObject.transform;
    }
}
