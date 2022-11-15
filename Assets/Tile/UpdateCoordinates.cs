using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class UpdateCoordinates : MonoBehaviour
{
    TextMeshPro label;
    Vector2Int coordinates;
   // Waypoint waypoint;
    bool labelActive = false;
    [SerializeField] Color defaultColor = Color.black;
    [SerializeField] Color blockedColor = Color.red;
    [SerializeField] Color pathColor = Color.white;
    [SerializeField] Color exploredColor = Color.cyan;
    GridManager grid;
    
    // Start is called before the first frame update
   private void Awake() {
       label = GetComponent<TextMeshPro>();
       label.enabled = false;
       grid = FindObjectOfType<GridManager>();
      // waypoint = GetComponentInParent<Waypoint>();
        DisplayCoordinates();
   }

    // Update is called once per frame
    void Update()
    {
        if(!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateTileName();
        }
        ColorCoordinates();
        ToggleLabels();
    }
    void ColorCoordinates()
    {
        if(grid == null) {return;}
        Node node = grid.GetNode(coordinates);
        if(node == null) {return;}
        if(!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if(node.isPath)
        {
            label.color = pathColor;
        }
        else if(node.isExplored)
        {
            label.color = exploredColor;
        }
        else if (node.isWalkable) label.color = defaultColor;
        // if(waypoint.IsPlaceable)  { 
        //     label.color = defaultColor;

        // }else label.color = blockedColor;
    }
    void ToggleLabels() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            labelActive = !labelActive;
            label.enabled = labelActive;
        }
    }
    private void DisplayCoordinates()
    {
        if(grid == null) {return;}
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / grid.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z /grid.UnityGridSize);
        label.text = $"{coordinates.x},{coordinates.y}";
    }
    void UpdateTileName() {
        transform.parent.name = $"Tile({coordinates.x},{coordinates.y})";
    }
}
