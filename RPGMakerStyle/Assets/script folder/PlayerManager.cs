using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MovingObject
{
    static public PlayerManager instance;

    //a character instance = a
    //b character instance = a
    //corellating objects into one

    public string currentMapName; // 


    public float runSpeed;
    private float applyRunSpeed;
    private bool applyRunFlag = false;

   
    private bool canMove = true;



    public string walkSound_1;
    public string walkSound_2;
    public string walkSound_3;
    public string walkSound_4;
    private AudioManager theAudio;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            queue = new Queue<string> ();


            DontDestroyOnLoad(this.gameObject);
            animator = GetComponent<Animator>();
            boxCollider = GetComponent<BoxCollider2D>();
            instance = this;


            theAudio = FindObjectOfType<AudioManager>();


        }
        else
        {
            Destroy(this.gameObject);
        }


    }

    IEnumerator MoveCoroutine()
    {

        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed;
                applyRunFlag = true;
            }
            else
            {
                applyRunSpeed = 0;
                applyRunFlag = false;
            }


            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if (vector.x != 0)
                vector.y = 0;

            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            RaycastHit2D hit;
            //shoot raser and see if something hits or not
            //position A + B
            // hit = null
            // hit = object

            Vector2 start = transform.position;// A point player's current position
            Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount); // B Point player's next position


            boxCollider.enabled = false;

            hit = Physics2D.Linecast(start, end, layerMask);

            boxCollider.enabled = true;

            if (hit.transform != null) //if there is a wall what comes after this won't be affective
                break;

            animator.SetBool("Walking", true);


            int temp = Random.Range(1, 4);
            switch (temp)
            {
                case 1:
                    theAudio.Play(walkSound_1);
                    break;
                case 2:
                    theAudio.Play(walkSound_2);
                    break;
                case 3:
                    theAudio.Play(walkSound_3);
                    break;
                case 4:
                    theAudio.Play(walkSound_4);
                    break;
            }

            theAudio.SetVolumn(walkSound_2, 0.5f);

            while (currentWalkCount < walkCount)
            {
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0);
                }
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
                }
                if (applyRunFlag)
                {
                    currentWalkCount++;
                }
                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);




            }
            currentWalkCount = 0;


        }
        animator.SetBool("Walking", false);
        canMove = true;

    }


    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }
    }
}
