using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float damage = 20.0f;
    private Animator animatorController;
    private bool hitting = false;
    private Target target;
    private Vector3 tmp;

    void Start()
    {
        animatorController = GetComponent<Animator>();
        target = GetComponent<Target>();
    }

    IEnumerator WaitThenDoThings(float time, Collider hit)
    {
        yield return new WaitForSeconds(time);
        
        SC_FPSController player = hit.GetComponent<SC_FPSController>();
        player.TakeDamage(damage);
        hitting = false;
        target.SetTargetPosition(tmp);
    }

    void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("Player") && !hitting && !target.IsDead())
        {
            hitting = true;
            tmp = target.GetDestination();
            target.Stop();
            animatorController.SetTrigger("Hit");
            StartCoroutine(WaitThenDoThings(0.5f, hit));
        }
    }
}
