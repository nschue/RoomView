using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Create a sub menu 
public class LoadSubMenu : MenuButton {

    public string[] hideMenus;//Include a tag for each menu that should be hidden
    public string[] showMenus;//Include a tag for each menu that should be shown

    public override void OnSelectButton(object sender, ClickedEventArgs e)
    {
        foreach(string hideMenu in hideMenus)
        {
            GameObject menu = GameObject.FindGameObjectWithTag(hideMenu);
            foreach (Transform child in menu.transform)
            {
                child.gameObject.SetActive(false);
            }

        }

       
        foreach(string showMenu in showMenus)
        {
            GameObject menu = GameObject.FindGameObjectWithTag(showMenu);
            foreach (Transform child in menu.transform)
            {
                child.gameObject.SetActive(true);
            }
        }

    }

}
