using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Unit_01 : MonoBehaviour
{
    public GameObject wayPointPref;
    public GameObject target;
    public List<Transform> points = new List<Transform>();

    private int lastWayPoint = 0;
    private int currentWayPoint = 0;
    private int destPoint = 0;

    private float nextPointFloat = 5;

    private bool hasTakenOtherPath;

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.autoBraking = true;
    }

    private void GotoNextPoint()
    {
        points.Clear();
        AddPoint();

        // Set the agent to go to the currently selected destination.
        agent.destination = points[currentWayPoint].position;
        currentWayPoint++;
        lastWayPoint = currentWayPoint--;
    }

    private void AddPoint()
    {
        if (hasTakenOtherPath)
        {
            nextPointFloat = nextPointFloat * (-1);
        }

        Vector3 randomPos = new Vector3(this.transform.position.x + nextPointFloat, 0, this.transform.position.z + Random.Range(-5, 5));

        if (!agent.CalculatePath(randomPos, agent.path))
        {
            Debug.Log("Taking other path");
            hasTakenOtherPath = true;
            randomPos = new Vector3(this.transform.position.x + Random.Range(-5, 5), 0, this.transform.position.z + Random.Range(Random.Range(-19.5f, -10f), Random.Range(19.5f, 10f)));
        }
        else
        {
            hasTakenOtherPath = false;
        }
        GameObject spawnedWayPoint = Instantiate(wayPointPref, randomPos, Quaternion.identity) as GameObject;
        points.Add(spawnedWayPoint.gameObject.transform);
        spawnedWayPoint.transform.parent = GameObject.Find("WayPointHolder").transform;
        Destroy(spawnedWayPoint, 10);
    }

    private void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }
    }
}