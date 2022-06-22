using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public string characterName;


    public float speed;
    public int walkCount;
    protected int currentWalkCount;

    private bool notCoroutine = false;


    public BoxCollider2D boxCollider;
    public LayerMask layerMask;
    public Animator animator;

    protected Vector3 vector;

    public Queue<string> queue;     //queue is a cumulative function 




    public void Move(string _dir, int _frequency = 5) //setting the frequency to a number means you can skip the number when handed over somewhere else
    {
        queue.Enqueue(_dir);

        if(!notCoroutine)
        {
            notCoroutine = true;
            StartCoroutine(MoveCoroutine(_dir, _frequency));
        }


    }


    IEnumerator MoveCoroutine(string _dir, int _frequency)
    {
        while (queue.Count != 0)
        {
            string direction = queue.Dequeue();


            vector.Set(0, 0, vector.z);




            switch (direction)
            {
                case "Up":
                    vector.y = 1f;
                    break;
                case "Down":
                    vector.y = -1f;

                    break;
                case "Right":
                    vector.x = 1f;
                    break;
                case "Left":
                    vector.x = -1f;
                    break;
            }


            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);
            animator.SetBool("Walking", true);



            while (currentWalkCount < walkCount)
            {

                transform.Translate(vector.x * speed, vector.y * speed, 0);
                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);
            }
            currentWalkCount = 0;
            if (_frequency != 5)
                animator.SetBool("Walking", false);

              
        }
        animator.SetBool("Walking", false);
        notCoroutine = false;
    }

    protected bool CheckCollsion()
    {
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
            return true;
        return false;

    }

}
