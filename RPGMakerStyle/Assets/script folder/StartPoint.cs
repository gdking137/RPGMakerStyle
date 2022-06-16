using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public string startPoint; //player starting point after being transferred over
    private MovingObject thePlayer;
    private cameraManager theCamera;   
    // Start is called before the first frame update
    void Start()
    {
        
        thePlayer = FindObjectOfType<MovingObject>();
        if (startPoint == thePlayer.currentMapName)
        {
            thePlayer.transform.position =  this.transform.position;
            theCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, theCamera.transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
