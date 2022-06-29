using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Dialogue  
{ 

    [TextArea(1,2)] //can add lines for text
    public string[] sentences;
    public Sprite[] sprites;
    public Sprite[] dialogueWindows;
}
