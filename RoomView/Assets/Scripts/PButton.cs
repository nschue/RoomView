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

    public override void OnSelectButton(object sender, ClickedEventArgs e)
    {
        switch (selectedAction)
        {
            case action.reset:
                //TODO reset scene
                break;

            case action.load:
                //TODO load scene 
                break;

            case action.save:
                //TODO save scene
                break;

            case action.exit:
                //TODO exit to main menu
                break;

            default:
                break;
        }
    }

}

