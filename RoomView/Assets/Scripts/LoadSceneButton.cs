using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneButton : MenuButton {

    public string levelName;
    public LevelManager levelManager;

    public override void OnSelectButton(object sender, ClickedEventArgs e)
    {
        levelManager.LoadLevel(levelName);
    }
}
