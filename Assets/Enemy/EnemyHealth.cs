using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] int hitPoints = 5;
    [Tooltip("Adds difficulty when a ram dies")]
    [SerializeField] int addDifficulty = 1;
    [SerializeField] int currentHP = 0;
    Enemy enemyScript;
    // Start is called before the first frame update
    private void Start() {
        enemyScript = GetComponent<Enemy>();
    }
    void OnEnable()
    {
        currentHP = hitPoints;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnParticleCollision(GameObject other)
    {
        ProcessHit(other);

    }

    private void ProcessHit(GameObject other)
    {
        
        Debug.Log($"Hit from {other.gameObject.name}");
        currentHP -= damage;
        Debug.Log(currentHP);
        if (currentHP <= 0)
        {
            gameObject.SetActive(false);
            hitPoints += addDifficulty;
            enemyScript.GetEnemyLoot();
            return;
        }
    }

}
