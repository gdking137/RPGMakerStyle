using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    /// database is a necessary for certain events
    /// scene transition destroys everything that's been set up
    /// reserve what has been done and reference database to call back on data 
    /// 

    /// save and load needs database to work
    /// 

    ///pre-plan materials like item to bring up and use
    // Start is called before the first frame update
    static public DatabaseManager instance;

    public AudioClip[] clips;
    private AudioSource source;


    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    private void Awake()
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



    public string[] var_name;
    public float[] var;

    public string[] switch_name;
    public bool[] switches;

    public List<Item> itemList = new List<Item>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
