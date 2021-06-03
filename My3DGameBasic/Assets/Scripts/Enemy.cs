using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform wayPoints;
    public List<Transform> points;
    public Transform player;
    int locationIndex = 0;
    bool isChase = false;
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        wayPoints = GameObject.Find("WayPoints").transform;
        foreach(Transform child in wayPoints)
        {
            points.Add(child);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (points.Count > 1 )
        if( agent.remainingDistance < 0.2f && !agent.pathPending)
        {
            isChase = false;
            agent.destination = points[locationIndex].position;
            locationIndex = (locationIndex + 1) % points.Count;
        }

        float dist = Vector3.Distance(player.position, this.transform.position);
        if ( dist < 5 && !isChase)
        {
            agent.destination = player.position;
            isChase = true;
        }
    }
}
