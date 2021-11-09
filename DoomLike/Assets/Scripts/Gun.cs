using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Camera fpsCam;
    public GameObject gunObject;
    public GameObject impactEffect;
    public ParticleSystem muzzleFlash;
    public GameObject blood;
    public GameObject Player;
    public GameObject PausePanel;

    private float damage = 10f;
    private float range = 100f;
    private float fireRate = 15f;
    private float nextTimeToFire = 0f;

    void Update() {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && !PausePanel.activeInHierarchy) {
            Shoot();
            Player.GetComponent<SC_FPSController>().LooseLife();
            nextTimeToFire = Time.time + 2f/fireRate;
        }
    }

    void Shoot() {
        AudioSource gunshot = gunObject.GetComponent<AudioSource>();
        muzzleFlash.Play();
        var main = muzzleFlash.main;
        main.stopAction = ParticleSystemStopAction.None;
        gunshot.volume = 0.15f;
        gunshot.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null) {
                target.TakeDamage(damage);
                GameObject impactEnemy = Instantiate(blood, hit.point, Quaternion.LookRotation(hit.normal));
            }
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }
}
