using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TransferMap : MonoBehaviour
{
    public Transform target;
    private MovingObject thePlayer;
    private cameraManager theCamera;
    public string transferMapName;

    public BoxCollider2D targetBound;


    // Start is called before the first frame update
    void Start()
    {

        theCamera = FindObjectOfType<cameraManager>();
        thePlayer = FindObjectOfType<MovingObject>();
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.gameObject.name == "Player")
        {
            thePlayer.currentMapName = transferMapName;
            theCamera.SetBound(targetBound);

            thePlayer.transform.position = target.transform.position;
            theCamera.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, theCamera.transform.position.z);
        }
    }


}

