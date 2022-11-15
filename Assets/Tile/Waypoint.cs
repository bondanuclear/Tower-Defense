using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get{return isPlaceable;}}
    [SerializeField] Tower tower;
    GridManager gridManager;
    Vector2Int coordinates;
    Pathfinding pathfinding;
    // Start is called before the first frame update
    private void Awake() {
        gridManager = FindObjectOfType<GridManager>();
        pathfinding = FindObjectOfType<Pathfinding>();
    }
    void Start()
    {
        if(gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
            if(!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown() {
        if(!pathfinding.WillBlockPath(coordinates) && gridManager.GetNode(coordinates).isWalkable) {
         //Debug.Log(transform.name);
        bool alreadyPlaced = tower.CreateTower(transform.position);
        // Instantiate(tower,transform.position,Quaternion.identity);
         if(alreadyPlaced)
         {
            gridManager.BlockNode(coordinates);
            pathfinding.NotifyReceivers();
         }
         
        }
    }
 
  
}
