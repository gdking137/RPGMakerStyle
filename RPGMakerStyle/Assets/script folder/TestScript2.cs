using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript2 : MonoBehaviour
{
    BGMManger BGM;


    public int playMusicTrack;

    // Start is called before the first frame update
    void Start()
    {
        BGM = FindObjectOfType<BGMManger>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(abc());
    }
    
    IEnumerator abc()
    {
        BGM.FadeOutMusic();

        yield return new WaitForSeconds(3f);

        BGM.FadeInMusic();
    
    }








    void Update()
    {
        
    }
}
