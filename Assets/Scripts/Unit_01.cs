using System.Collections;
using UnityEngine;

public class Unit_01 : MonoBehaviour
{
    public bool isSelected;

    public float offset;

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
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
    }

    public void AddOffset(float increment)
    {
        offset = increment;
    }
}