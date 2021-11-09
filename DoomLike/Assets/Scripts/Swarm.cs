using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swarm : MonoBehaviour
{
    public GameObject DemonPrefab;
    public List<GameObject> Spawners;
    public GameObject Player;
    
    private List<Target> demons = new List<Target>();
    private float waitTime = 8.0f;
    private float timer = 0.0f;

    void Awake()
    {
        Time.timeScale = 1.0f;
    }

    void FixedUpdate()
    {
        Vector3 swarmCenter = Player.transform.position;
        int idx = 0;
        
        while (idx < demons.Count)
        {
            if (!demons[idx].IsDead())
            {
                demons[idx].SetTargetPosition(swarmCenter);
            }
            else
            {
                demons[idx].Stop();
                demons.RemoveAt(idx);
            }
            ++idx;
        }
        timer += Time.deltaTime;
        if (timer > waitTime)
        {
            SpawnDemons();
            timer = timer - waitTime;
            Time.timeScale = 1.0f;
        }
    }

    private void SpawnDemons()
    {
        foreach (GameObject spawn in Spawners)
        {
            AddDemon(spawn);
        }
    }

    private void AddDemon(GameObject spawn)
    {
        GameObject newDemon = (GameObject)Instantiate(DemonPrefab, spawn.transform.position, Quaternion.identity);
        Target newTarget = newDemon.GetComponent<Target>();
        demons.Add(newTarget);
    }
}
