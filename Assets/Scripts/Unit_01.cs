﻿using System.Collections;
using UnityEngine;

public class Unit_01 : MonoBehaviour
{
    public bool isSelected;

    public float offset;

    private Transform closesetTransform;
    private Player_Unit_Gun gun;

    private NavMeshAgent agent;
    private FieldOfView fow;

    private float nextPointFloat = 5;
    private float lastInList;
    private float closest;

    private int counter;
    private int closestIndex;
    private int count;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        fow = GetComponent<FieldOfView>();
        gun = GetComponent<Player_Unit_Gun>();
    }

    private void LateUpdate()
    {
        Debug.Log(transform.forward);

        if (isSelected)
        {
            if (Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    agent.destination = hit.point;
                }
            }
        }

        //Get the size of the list
        if (fow.visibleTargets.Count > 0)
        {
            FindClosestEnemy();
            foreach (Transform t in fow.visibleTargets)
            {
                counter++;
            }
        }

        //If there are enemys in the list, look at them
        if (counter != 0)
        {
            closesetTransform = fow.visibleTargets[closestIndex];
            gun.Shoot();
            this.transform.LookAt(closesetTransform);
        }

        counter = 0;
    }

    private void FindClosestEnemy()
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
        }
    }

    public void AddOffset(float increment)
    {
        offset = increment;
    }
}