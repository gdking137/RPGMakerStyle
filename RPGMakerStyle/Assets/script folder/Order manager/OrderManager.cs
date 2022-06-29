using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{



    private PlayerManager thePlayer; //takes away the controller for player to the system so it can't move

    private List<MovingObject> characters;



    void Start()
    {
        thePlayer = FindObjectOfType<PlayerManager>();    
    }

    public void PreLoadCharacter()
    {
        characters = ToList();
    }


    public List<MovingObject> ToList()
    {
        List<MovingObject> tempList = new List<MovingObject>();
        MovingObject[] temp = FindObjectsOfType<MovingObject>();

        for(int i = 0; i < temp.Length;i++)
        {
            tempList.Add(temp[i]);
        }
        return tempList; //value will go into "characters"
    }

    public void NotMove()
    {
        thePlayer.notMove = true;

    }

    public void Move()
    {
        thePlayer.notMove = false;

    }


    public void SetThrough(string _name)
    {
       for (int i = 0; i < characters.Count; i++)
            {
                if (_name == characters[i].characterName)
                {
                    characters[i].boxCollider.enabled = false;
                }
        }
    }

    public void SetUnThrough(string _name)
    {

            for (int i = 0; i < characters.Count; i++)
            {
                if (_name == characters[i].characterName)
                {
                    characters[i].boxCollider.enabled = true;
                }
            }

    }



    public void SetTransParent(string _name)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].gameObject.SetActive(false);
            }
        }
    }
    public void SetUnTransParent(string _name)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].gameObject.SetActive(true);
            }
        }
    }


    public void Move(string _name, string _dir)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if(_name == characters[i].characterName)
            {
                characters[i].Move (_dir);
            }
        }
    }

    public void Turn(string _name, string _dir)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].animator.SetFloat("DirX", 0f);
                characters[i].animator.SetFloat("DirY", 0f);
                switch (_dir)
                {
                    case "Up":
                        characters[i].animator.SetFloat("DirY", 1f);
                        break;
                    case "Down":
                        characters[i].animator.SetFloat("DirY", -1f);
                        break;
                    case "Left":
                        characters[i].animator.SetFloat("DirX", -1f);
                        break;
                    case "Right":
                        characters[i].animator.SetFloat("DirX", 1f);
                        break; 
                }

            }
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
