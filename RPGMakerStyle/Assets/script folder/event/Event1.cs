using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event1 : MonoBehaviour
{
    public Dialogue dialogue_1;
    public Dialogue dialogue_2;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer;   //animator.getFloat    "DirY" == 1f

    private bool flag;



    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        theOrder = FindObjectOfType<OrderManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!flag && Input.GetKey(KeyCode.Z) && thePlayer.animator.GetFloat("DirY") == 1f)   //GetKey= pressing works
        { 
            flag = true;
            //StartCoroutine(EventCoroutine());
        }
    }

    // THESE ARE ALL THE CODES THAT DIDN'T WORK WHEN MAKING COMMANDS FOR THE GAME
    /*
    IEnumerator EventCoroutine()
    {
        theOrder.PreLoadCharacter();

        theOrder.NotMove();

        theDM.ShowDialogue(dialogue_1);

        yield return new WaitUntil(()=> ! theDM.talking);

        theOrder.Move("player", "Right");
        theOrder.Move("player", "Right");

        theDM.ShowDialogue(dialogue_2);

        theOrder.Move();




        theOrder.Move();
    }
    */
}
