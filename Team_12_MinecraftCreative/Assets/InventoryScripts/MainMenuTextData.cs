using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/MenuText Item")]
public class MainMenuTextData : ScriptableObject
{
    public string ItemName;
    public int ItemID;

    public int TextEntry;
    public string OutputText;

    [TextArea(4, 4)]
    public string Description;

    public void RandomiseSplash()
    {
        int randomText = Random.Range(1, 10);
        if(randomText != TextEntry)
        {
            TextEntry = randomText;
        }
        else
        {
            TextEntry += 1;
        }
    }

    public void OutputEntry()
    {
        if(TextEntry == 1)
        {
            OutputText = "Yes this changes!!!!";
        } 
        else if(TextEntry == 2)
        {
            OutputText = "password is password";
        }
        else if(TextEntry == 3)
        {
            OutputText = "As seen on TV!";
        } 
        else if(TextEntry == 4)
        {
            OutputText = "Not much to survive innit";
        }
        else if (TextEntry == 5)
        {
            OutputText = "Alright, Alright, Alright!";
        }
        else if (TextEntry == 6)
        {
            OutputText = "cool, cool, cool, cool";
        }
        else if (TextEntry == 7)
        {
            OutputText = "Six seasons and a movie!!";
        }
        else if (TextEntry == 8)
        {
            OutputText = "I'm Batman!";
        }
        else if (TextEntry == 9)
        {
            OutputText = "Ceci n'est pas une title screen!";
        }

        else
        {
            OutputText = "Bringing home the bacon!";
            
        }

    }

}
