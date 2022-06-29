using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{

    public int itemID;
    public int _count;
    //public string pickUpSound;


    // Use this for initialization
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Inventory.instance.GetAnItem(itemID, _count);
            // AudioManager.instance.Play(pickUpSound);
            Destroy(this.gameObject);
        }
    }
}