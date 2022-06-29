using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript3 : MonoBehaviour
{
    private OrderManager theOrder;
    private NumberSystem theNumber;



    public bool flag;

    // Start is called before the first frame update
    void Start()
    {
        theOrder = FindObjectOfType<OrderManager>();
        theNumber = FindObjectOfType<NumberSystem>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
