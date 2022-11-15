using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] float range  =15f;
    [SerializeField] Transform ballista;
    [SerializeField] ParticleSystem particleSyst;
    bool isLocated;
    Transform enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = FindObjectOfType<Enemy>().transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        LocateEnemy();
        //FindClosestTarget();
    }
    bool FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        if(enemies.Length == 0) {return false;}
        float maxDistance = Mathf.Infinity;
        foreach(Enemy enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if(distance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = distance;
            }
        }
        enemy = closestTarget;
        return true;
    }
    private void LocateEnemy()
    {
       
        float distance = Vector3.Distance(transform.position, enemy.transform.position);
        ballista.LookAt(enemy);
        if(distance < range )
        {
            if(FindClosestTarget())
            Attack(true);
        } 
        else {
            Attack(false);
        }
        
    }

    private void Attack(bool isActive)
    {
        var emission = particleSyst.emission;
        emission.enabled = isActive;
    }
}
