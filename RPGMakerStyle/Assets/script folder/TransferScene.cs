using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferScene : MonoBehaviour
{
    private MovingObject thePlayer;
    private cameraManager theCamera;
    public string transferMapName;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<MovingObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.gameObject.name == "Player")
        {

                SceneManager.LoadScene(transferMapName);
                thePlayer.currentMapName = transferMapName;

        }
    }

}
