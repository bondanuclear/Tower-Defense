using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Bank : MonoBehaviour
{
    [SerializeField] int bankAmout = 400;
    [SerializeField] int currentBalance = 0;
    [SerializeField] TextMeshProUGUI goldText;
    public int CurrentBalance {get{return currentBalance;}}
    // Start is called before the first frame update
    private void Awake() {
        currentBalance = bankAmout;
        goldText.text = "Gold: " + currentBalance;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        goldText.text = "Gold: " + currentBalance;
    }
    public void Withdraw(int amount) 
    {
        currentBalance -= Mathf.Abs(amount);
        goldText.text = "Gold: " + currentBalance;
        if (CurrentBalance < 0)
        {
            ProcessRespawn();
        }
    }
    private void ProcessRespawn()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(sceneIndex);
        SceneManager.LoadScene(sceneIndex);
    }
}
