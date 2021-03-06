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
    public int frequency; // npc speed/rate/tempo of towards the chosen direction

}


public class NPCManager : MovingObject
{
    [SerializeField]

    public NPCMove npc;
    // Start is called before the first frame update
    void Start()
    {
        queue = new Queue<string>();
        StartCoroutine(MoveCoroutine());

    }

    // Update is called once per frame
    public void SetMove()
    {
    }


public void SetNotMove()
    {
        StopAllCoroutines();
    }


    IEnumerator MoveCoroutine()
    {
        if (npc.direction.Length != 0)
        {
            for (int i = 0; i < npc.direction.Length; i++)
            {
                
                yield return new WaitUntil(() => queue.Count < 2);     //wait until npcCanMove

                base.Move(npc.direction[i], npc.frequency);
                //moving space

                if (i == npc.direction.Length - 1)
                {
                    i = -1;
                }
            }
        }
    }


}
