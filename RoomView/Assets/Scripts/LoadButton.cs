using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class LoadButton : MenuButton
{




    public SaveLoadUtility slu;
    private string saveGameName;
    private int selectedSaveGameIndex = -99;
    public List<SaveGame> saveGames;
    private char[] newLine = "\n\r".ToCharArray();

    private Regex regularExpression = new Regex("^[a-zA-Z0-9_\"  *\"]*$"); // A regular expression is a pattern that could be matched against an input text. 
                                                                           /*Regular expression, contains only upper and lowercase letters, numbers, and underscores.

                                                                                 * ^ : start of string
                                                                                [ : beginning of character group
                                                                                a-z : any lowercase letter
                                                                                A-Z : any uppercase letter
                                                                                0-9 : any digit
                                                                                _ : underscore
                                                                                ] : end of character group
                                                                                * : zero or more of the given characters
                                                                                $ : end of string

                                                                            */

    // Use this for initialization
    void Start()
    {
        if (slu == null)
        {
            slu = GetComponent<SaveLoadUtility>();
            if (slu == null)
            {
                Debug.Log("[SaveLoadMenu] Start(): Warning! SaveLoadUtility not assigned!");
            }
        }
    }


    public override void OnSelectButton(object sender, ClickedEventArgs e)
    {
        slu.LoadGame(gameObject.GetComponent<Text>().text);
    }
}
