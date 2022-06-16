using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public Transform target;
    private MovingObject thePlayer;
    private cameraManager theCamera;
    public string transferMapName;
    public bool flag;


    // Start is called before the first frame update
    void Start()
    {
        if (!flag)
        {
            theCamera = FindObjectOfType<cameraManager>();
        }
        thePlayer = FindObjectOfType<MovingObject>();
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.gameObject.name == "Player")
        {
            if(flag)
                SceneManager.LoadScene(transferMapName);
            else
            //SceneManager.LoadScene(transferMapName);
            thePlayer.currentMapName = transferMapName;

            thePlayer.transform.position = target.transform.position;


            thePlayer.transform.position = target.transform.position;
            theCamera.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, theCamera.transform.position.z);
        }
    }


}

