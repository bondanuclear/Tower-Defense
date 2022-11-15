using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    public static ObjectPool Instance;
    private List<GameObject> pool;
    [SerializeField] [Range(0,50)]int volumeOfPool = 10;
    [SerializeField] [Range(0.4f,10f)]private float timeToSpawn;

    // Start is called before the first frame update
    void Awake() {
        Instance = this;
    }
    void Start()
    {
        pool = new List<GameObject>();
        GameObject tmp;
        for(int i = 0; i < volumeOfPool; i++) 
        {
            tmp = Instantiate(enemy,transform);
            tmp.SetActive(false);
            pool.Add(tmp);
        }
        StartCoroutine(SpawnEnemies());
    }
    public void GetPoolObject() {
        for(int i =0; i < pool.Count; i++) {
            if(!pool[i].activeInHierarchy) {
                pool[i].SetActive(true);
                return;
            }
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnEnemies() {
        while (true)
        {
           
           
            GetPoolObject();
            yield return new WaitForSeconds(timeToSpawn);

        }
        
    }
}
