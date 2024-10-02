using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostMove : MonoBehaviour
{
    public Transform player;
    Transform target;
    public Transform enemy;
    public Transform[] randTarget;
    NavMeshAgent ghost;
    bool isMoving = false;
    bool isFollowing = false;
    public GameObject gameManager;
    public GameObject screamer;

    public LayerMask TargetMask;
    public LayerMask ObstacleMask;

    public float angle;
    public float distance;

    void Start()
    {
        ghost = GetComponent<NavMeshAgent>();
        target = randTarget[Random.Range(0, 5)];
        FindNewDest();
    }

    void Update()
    {
        Debug.Log(isFollowing);
        FindVisibleTargets();
        if (isFollowing == true && Vector3.Distance(enemy.position, player.position) <= 2)
        {
            isFollowing = false;
            ghost.speed = 0.5f;
            GetComponent<Animator>().speed /= 2;
            GetComponent<AudioSource>().pitch = 0.22f;
            FindNewDest();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "random")
            FindNewDest();
    }

    void GetBack()
    {
         FindNewDest();
    }

    public void FindVisibleTargets()
    {
        Collider[] targets = Physics.OverlapSphere(enemy.position, distance, TargetMask);

        for (int i = 0; i < targets.Length; i++)
        {
            Transform target1 = targets[i].transform;

            Vector3 dirToTarget = (target1.position - enemy.position).normalized;

            if (target1.name == player.name && Vector3.Dot(enemy.forward, dirToTarget) > Mathf.Cos((angle / 2) * Mathf.Deg2Rad) && isFollowing == false)
            {
                float distToTarget = Vector3.Distance(enemy.position, target1.position);
                ghost.SetDestination(player.position);
                ghost.speed = 1;
                GetComponent<Animator>().speed *= 2;
                isFollowing = true;
                GetComponent<AudioSource>().pitch = 0.44f;
                screamer.GetComponent<AudioSource>().Play();
            }
        }
    }

    void FindNewDest()
    {
        while (true)
        {
            Transform newTarget = randTarget[Random.Range(0, 5)];
            if (target.name != newTarget.name)
            {
                target = newTarget;
                break;
            }
        }
        ghost.SetDestination(target.position);
        Debug.Log(target.name);
    }
}
