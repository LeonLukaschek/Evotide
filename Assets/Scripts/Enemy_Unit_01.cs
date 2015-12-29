using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Unit_01 : MonoBehaviour
{
    public GameObject wayPointPref;
    public GameObject target;
    public List<Transform> points = new List<Transform>();

    [Range(0, 100)]
    public float health;

    private Transform closesetTransform;
    private EnemyUnitGun gun;

    private int lastWayPoint = 0;
    private int currentWayPoint = 0;
    private int counter;
    private int closestIndex;
    private int count;

    private float nextPointFloat = 5;
    private float lastInList;
    private float closest;

    private bool hasTakenOtherPath;

    private NavMeshAgent agent;
    private FieldOfView fow;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        fow = GetComponent<FieldOfView>();
        gun = GetComponent<EnemyUnitGun>();
        agent.autoBraking = true;
    }

    private void LateUpdate()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.

        if (agent.remainingDistance < 0.5f && !target)
        {
            GotoNextPoint();
        }
        if (fow.visibleTargets.Count > 0)
        {
            FindClosestEnemy();
            closesetTransform = fow.visibleTargets[closestIndex];
            target = closesetTransform.gameObject;
        }
        else
        {
            target = null;
        }

        if (target)
        {
            this.transform.LookAt(closesetTransform);
            Debug.Log("Shooting");
            gun.Shoot();
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            health -= other.GetComponent<Bullet>().damage;
        }
    }

    private void Die()
    {
        this.gameObject.SetActive(false);
        Destroy(this.gameObject, 1f);
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

    private GameObject FindClosestEnemy()
    {
        if (fow.visibleTargets.Count > 0)
        {
            foreach (Transform t in fow.visibleTargets)
            {
                float distance = Vector3.Distance(this.transform.position, t.transform.position);
                if (distance < lastInList)
                {
                    closest = distance;
                    closestIndex = count;
                }

                lastInList = distance;
                count++;
            }
            count = 0;

            return fow.visibleTargets[closestIndex].gameObject;
        }

        return null;
    }
}