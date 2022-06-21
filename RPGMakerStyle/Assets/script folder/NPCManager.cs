using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCMove
{
    [Tooltip("if you check NPCMove then NPC will move")] //adds description on the inspector
    public bool NPCmove;

    public string[] direction; //npc moving directions


    [Range(1, 5)]
    public int frequency; // npc rate of towards the chosen direction

}


public class NPCManager : MonoBehaviour
{
    [SerializeField]

    public NPCMove npc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
