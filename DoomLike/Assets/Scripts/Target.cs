using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public float maxHealth = 50f;
    public GameObject healthDrop;
    public GameObject healDrop;
    public GameObject speedDrop;

    private AudioSource sfxSource;
    public AudioClip deathSfx;

    private Animator animatorController;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private bool died = false;
    private Vector3 destination;
    private bool hasPlayed = false;

    void Start()
    {
        sfxSource = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
        animatorController = GetComponent<Animator>();
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public void Stop()
    {
        destination = gameObject.transform.position;
        navMeshAgent.SetDestination(destination);
    }

    public Vector3 GetDestination()
    {
        return destination;
    }

    public bool IsDead()
    {
        return died;
    }

    public void SetTargetPosition(Vector3 swarmCenterAverage)
    {
        destination = swarmCenterAverage;
        navMeshAgent.SetDestination(destination);
    }

    public void TakeDamage(float amount) 
    {
        health -= amount;
        if (health <= 0 && !died) {
            Die();
        }
    }

    public void Heal(float amount) 
    {
        health += amount;
        if (health > maxHealth) {
            health = maxHealth;
        }
        print("health: " + health);
    }

    public void AddHealth(float amount) 
    {
        maxHealth += amount;
        Heal(amount);
    }

    void Die()
    {
        if (!hasPlayed)
        {
            sfxSource.PlayOneShot(deathSfx);
            hasPlayed = true;
        }
        animatorController.SetTrigger("Die");
        ScoreCross.AddOne();
        Destroy(gameObject, 3);
        died = true;
        
        int chances = Random.Range(0, 100);
        Vector3 lootSpawn = gameObject.transform.position;

        lootSpawn.y += 1;
        if (chances >= 70 && chances < 90) 
        { 
            Instantiate(healDrop, lootSpawn, Quaternion.identity);
        }
        else if (chances >= 90)
        {
            Instantiate(speedDrop, lootSpawn, Quaternion.identity);
        }
        else if (chances >= 65 && chances < 70)
        {
            Instantiate(healthDrop, lootSpawn, Quaternion.identity);
        }
    }
}
