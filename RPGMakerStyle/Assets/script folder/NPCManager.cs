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
        StartCoroutine(MoveCoroutine());
    }

    // Update is called once per frame
public void SetMove()
    {

    }


public void SetNotMove()
    {

    }


    IEnumerator MoveCoroutine()
    {
        if (npc.direction.Length != 0)
        {
            for (int i = 0; i < npc.direction.Length; i++)
            {
                switch (npc.frequency)
                {
                    case 1:
                        yield return new WaitForSeconds(4f);
                        break;
                    case 2:
                        yield return new WaitForSeconds(3f);
                        break;
                    case 3:
                        yield return new WaitForSeconds(2f);
                        break;
                    case 4:
                        yield return new WaitForSeconds(1f);
                        break;
                    case 5:
                        break;

                }
                yield return new WaitUntil(() => npcCanMove);     //wait until npcCanMove

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
