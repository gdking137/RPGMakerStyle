using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

    public float speed;
    public int walkCount;
    protected int currentWalkCount;


 

    public BoxCollider2D boxCollider;
    public LayerMask layerMask;
    public Animator animator;

    protected  Vector3 vector;
}
