using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Range(1.0f, 20f)]
    public float damage = 10f;
    [Range(1.0f, 200f)]
    public float range = 100f;
    [Range(10f, 20f)]
    public float fireRate = 15f;
    public float impactForce = 30f;

    private float timer = 0f;

    //public Camera fpsCam; 
    public ParticleSystem muzzleFlash;
    public AudioSource muzzleSound;
    public GameObject shootEffect;

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetButton("Fire1") && Time.time >= timer)
        {
            timer = Time.time + 1f / fireRate;
            Shoot();
        }
        
    }

    void Shoot()
    {
        muzzleFlash.Play();
        muzzleSound.Play();

        Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
        //Debug.DrawRay(ray.origin, ray.direction * range, Color.red, 1f);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range))
        {
            GameObject impactGO = Instantiate(shootEffect, transform.position, transform.rotation);
            impactGO.GetComponent<ShotBehavior>().SetTarget(hit.point);
            Destroy(impactGO, 2f);
            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                Debug.Log(target);
                target.TakeDamage(damage);
            }

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(hit.normal * impactForce);
            }

            
        }
    }
}

