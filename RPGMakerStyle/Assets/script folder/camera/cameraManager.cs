using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{
    static public cameraManager instance;


    public GameObject target;
    public float moveSpeed;
    private Vector3 targetPosition;

    
    void Start()
    {
            if (instance != null)
            {
            Destroy(this.gameObject);
            }
            else
            {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
            }

    }

    // Update is called once per frame
    void Update()
    {
        if(target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);

            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime); //move 1 second of moveSpeed 
        }
    }
}
 