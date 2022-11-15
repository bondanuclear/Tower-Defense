using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 50;
    [SerializeField] private float timeToWait = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Build());
    }

   public bool CreateTower(Vector3 tilePosition)
   {
        Bank bank = FindObjectOfType<Bank>();
        if(bank == null) {return false;}
        if(bank.CurrentBalance >= cost)
        {
            Instantiate(gameObject,tilePosition, Quaternion.identity);
            bank.Withdraw(cost);
            return true;
        }
        
        return false;
   }
    IEnumerator Build()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(timeToWait);
        }
        
    }
    
}
