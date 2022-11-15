using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Node> path = new List<Node>();
    //[SerializeField] private float timeToWait = 1f;
    [SerializeField] [Range(0.1f,5)] float speed;
    Enemy enemyScript;
    GridManager gridManager;
    Pathfinding pathfinding;
    // Start is called before the first frame update
    void OnEnable()
    {

        ReturnToStart();
        FindPath(true);
       
    }
    private void Awake() {
        enemyScript = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinding = FindObjectOfType<Pathfinding>();
    }

    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathfinding.StartingCoordinates);
    }
    void FindPath(bool isReset) 
    {
      
        Vector2Int coordinates = new Vector2Int();
        if(isReset)
        {
            coordinates = pathfinding.StartingCoordinates;
        }
        else 
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }
        StopAllCoroutines();
        path.Clear();
        path = pathfinding.BuildNewPath(coordinates);
        StartCoroutine(FollowPath());
        // Transform parent = GameObject.FindGameObjectWithTag("Path").transform;
        
        // foreach (Transform child in parent)
        // {
           
        //     Waypoint waypoint = child.GetComponent<Waypoint>();
        //     if(waypoint != null)
        //     {
        //         path.Add(waypoint);
        //     }
        // }
        // Transform[] children = GameObject.FindGameObjectWithTag("Path").GetComponentsInChildren<Transform>();
        // foreach(Transform child in children)
        // {
        //     if(child!= null && child.gameObject != null)
        //     path.Add(child.gameObject.GetComponent<Waypoint>());
        // }
        // GameObject[] road = GameObject.FindGameObjectsWithTag("Path");
        // foreach(GameObject way in road)
        // {
        //     path.Add(way.GetComponent<Waypoint>());
        // }
    }
    IEnumerator FollowPath()
    {
        for(int i = 1; i < path.Count; i++)
        {

            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;
            transform.LookAt(endPosition);
            while (travelPercent < 1)
            {
                travelPercent += Time.deltaTime * speed;
                //Debug.Log(travelPercent);
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
            
            //yield return new WaitForSeconds(timeToWait);
            // transfrom.position = waypoint.transfrom.position;
        }
        // Destroy(gameObject);
        FinishPath();
    }

    private void FinishPath()
    {
        enemyScript.StealFromBank();
        gameObject.SetActive(false);
    }
}
