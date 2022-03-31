using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : BaseCharacter
{
    public GameObject player;
    public NavMeshAgent navMeshAgent;
    public void MoveToPlayer()
    {

        if (player != null && navMeshAgent.isOnNavMesh)
        {
            navMeshAgent.SetDestination(player.transform.position);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    { 
    }
}
