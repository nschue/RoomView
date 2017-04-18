using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PButton : MenuButton
{

    public enum action
    {
        reset, load, save, exit
    }

    public action selectedAction;

    private GameObject levelManager;
    private LoadSubMenu subMenu;

    public void Start()
    {
        levelManager = GameObject.Find("LevelManager");
        subMenu = gameObject.AddComponent<LoadSubMenu>();
    }


    public override void OnSelectButton(object sender, ClickedEventArgs e)
    {
        switch (selectedAction)
        {
            case action.reset:
                Debug.Log("Cleared the scene");
                levelManager.GetComponent<LevelManager>().ResetLevel();
                break;

            case action.load:
                subMenu.hideMenus = new string[] {"Pause Menu"};
                subMenu.showMenus = new string[] {"Load Menu"};
                subMenu.OnSelectButton(sender, e);
                break;

            case action.save:
                subMenu.hideMenus = new string[] { "Pause Menu" };
                subMenu.showMenus = new string[] { "Save Menu" };
                subMenu.OnSelectButton(sender, e);
                break;

            case action.exit:
                levelManager.GetComponent<LevelManager>().LoadLevel("Start");
                break;

            default:
                break;
        }
    }

}

