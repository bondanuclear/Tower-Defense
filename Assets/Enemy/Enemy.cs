using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Bank bank;
    [SerializeField]private int lootAmount = 50;
    [SerializeField]private int stolenAmount = 40;

    // Start is called before the first frame update
    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetEnemyLoot()
    {
        bank.Deposit(lootAmount);
    }
    public void StealFromBank()
    {
        bank.Withdraw(stolenAmount);
    }
}
