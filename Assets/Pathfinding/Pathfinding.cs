using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{


    [SerializeField] Vector2Int startingCoordinates;
    public Vector2Int StartingCoordinates { get{return startingCoordinates;}}
    [SerializeField] Vector2Int destinationCoordinates;
    public Vector2Int DestinationCoordinates { get { return destinationCoordinates; } }
    Node startingNode;
    Node destinationNode;
    Node currentNode;
    Queue<Node> frontier = new Queue<Node>();
    Dictionary<Vector2Int,Node> visited = new Dictionary<Vector2Int, Node>();
    Vector2Int[] directions = {Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down};
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid;
    void Awake() 
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null)
        {
            grid = gridManager.Grid;
        }
        //ExploreNeighbours();
       
    }
    // Start is called before the first frame update
    void Start()
    {
        
        BuildNewPath();
    }

    public List<Node> BuildNewPath()
    {
        return BuildNewPath(startingCoordinates);
    }
    public List<Node> BuildNewPath(Vector2Int coordinates)
    {
        frontier.Clear();
        visited.Clear();
        gridManager.ResetPath();
        BreadthFirstSearch(coordinates);
        return BuildPath();
    }

    void ExploreNeighbours() 
    {
        List<Node> neighbours  = new List<Node>();

        for(int i = 0; i < directions.Length; i++)
        {
            
            Vector2Int neighborCoords = currentNode.coordinates + directions[i];
            if(grid.ContainsKey(neighborCoords))
            {
                neighbours.Add(grid[neighborCoords]);

                 //grid[neighborCoords].isExplored = true;
                // grid[currentNode.coordinates].isPath = true;
            }
        }
        foreach(Node neighbour in neighbours)
        {
            if(!visited.ContainsKey(neighbour.coordinates) && neighbour.isWalkable)
            {
                //Debug.Log(neighbour + "will be added to queue and visited");
                neighbour.connectedTo = currentNode;
                frontier.Enqueue(neighbour);
                visited.Add(neighbour.coordinates, neighbour);
               
            }
        }
    }
   void BreadthFirstSearch(Vector2Int coordinates)
   {
       startingNode = gridManager.Grid[startingCoordinates];
       destinationNode = gridManager.GetNode(destinationCoordinates);
       bool isRunning = true;
       frontier.Enqueue(grid[coordinates]);
       visited.Add(coordinates, grid[coordinates]);
        while(isRunning && frontier.Count > 0)
        {
            currentNode = frontier.Dequeue();
            currentNode.isExplored = true;
            ExploreNeighbours();
            if(currentNode.coordinates == destinationNode.coordinates)
            {
               //Debug.Log(currentNode.coordinates + " Done");
                isRunning = false;
            }
        }
   }
   List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node curNode = destinationNode;
        path.Add(curNode);
        curNode.isPath = true;
        
        while(curNode.connectedTo != null)
        {
            curNode = curNode.connectedTo;
            path.Add(curNode);
            curNode.isPath = true;
           
        }
        path.Reverse();
        return path;
    }
    public bool WillBlockPath(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates))
        {
            bool previousState = grid[coordinates].isWalkable;
            grid[coordinates].isWalkable = false;
            List<Node> newPath = BuildNewPath();
            grid[coordinates].isWalkable = previousState;
            if(newPath.Count <= 1)
            {
                BuildNewPath();
                return true;
            }
        }
        return false;
    }
    public void NotifyReceivers()
    {
        BroadcastMessage("FindPath",false,SendMessageOptions.DontRequireReceiver);
    }
}
