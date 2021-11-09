using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class path_finding : MonoBehaviour
{
    public GameObject targetGo;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    
    void Start() {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update() {
        HeadForDestintation();
    }   

    private void HeadForDestintation() {
        Vector3 destination = targetGo.transform.position;  
        navMeshAgent.SetDestination(destination);
        //UsefulFunctions.DebugRay(transform.position, destination, Color.yellow);
    }
}

