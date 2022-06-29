using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TransferMap : MonoBehaviour
{
    public Transform target;
    private PlayerManager thePlayer;
    private cameraManager theCamera;
    public string transferMapName;

    public BoxCollider2D targetBound;

    private FadeManager theFade;

    private OrderManager theOrder;

    // Start is called before the first frame update
    void Start()
    {
        theOrder = FindObjectOfType<OrderManager>();
        theCamera = FindObjectOfType<cameraManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
       theFade = FindObjectOfType<FadeManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.gameObject.name == "Player")
        {

            theFade.FadeOut();
            thePlayer.currentMapName = transferMapName;
            theCamera.SetBound(targetBound);

            thePlayer.transform.position = target.transform.position;
            theCamera.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, theCamera.transform.position.z);
            theFade.FadeIn();
        }
    }

    IEnumerator TransferCoroutine()
    {
        theFade.FadeOut();

        yield return new WaitForSeconds(1f);


        thePlayer.currentMapName = transferMapName;
        theCamera.SetBound(targetBound);

        thePlayer.transform.position = target.transform.position;
        theCamera.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, theCamera.transform.position.z);
        theFade.FadeIn();
    }
}

