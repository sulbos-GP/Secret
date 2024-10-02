using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerTrigger : MonoBehaviour
{
    Transform target;
    public Transform enemy;
    public Transform[] randTarget;
    NavMeshAgent ghost;
    bool isMoving = false;

    private void OnTriggerStay(Collider collision)
    {
        
    }
}
