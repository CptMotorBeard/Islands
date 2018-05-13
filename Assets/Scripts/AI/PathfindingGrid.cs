using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingGrid : MonoBehaviour {

    #region Singleton
    public static PathfindingGrid instance;

    void Awake()
    {
        if (instance)
        {
            Debug.LogWarning("Another instance of pathfinding grid already exists");
            return;
        }
        instance = this;
    }
    #endregion

    public LayerMask blockingLayer;         // Layer for blocking objects
    public Vector2 gridWorldSize;           // Size of pathfinding grid in world units
    public float nodeRadius;                // Size of nodes

    public float spacing;                   // Spacing between each node - for Gizmos only
    public Color blocked;                   // Set the colours for the Gizmo
    public Color walkable;                  

    Node[,] nodeArray;
    float nodeDiameter;
    int gridSizeX;                          // Size of grid in units
    int gridSizeY;

    Vector3 topRight;                     // Store the bottom left of the grid, to transform world position


    void Start()
    {
        nodeDiameter = nodeRadius * 2;

        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);   // Get the size of the grid
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        CreateGrid();
    }

    void CreateGrid()
    {
        nodeArray = new Node[gridSizeX, gridSizeY];

        topRight = transform.position - Vector3.left * gridWorldSize.x / 2 - Vector3.down * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPosition = topRight + Vector3.left * (x * nodeDiameter + nodeRadius) + Vector3.down * (y * nodeDiameter + nodeRadius);
                bool blocking = false;

                Collider2D[] cast = Physics2D.OverlapCircleAll(worldPosition, nodeRadius, blockingLayer);

                foreach (Collider2D c in cast)
                {
                    if (!c.isTrigger)
                    {
                        blocking = true;
                        break;
                    }
                }

                nodeArray[x, y] = new Node(blocking, worldPosition, x, y);
            }
        }
    }

    public Node NodeFromWorldPoint(Vector2 worldPoint)
    {
        int x = -Mathf.RoundToInt((worldPoint.x - topRight.x - nodeRadius) / nodeDiameter);
        int y = -Mathf.RoundToInt((worldPoint.y - topRight.y - nodeRadius) / nodeDiameter);

        return nodeArray[x, y];
    }

    public void UpdateGrid(Vector2 updatePoint, int updateDepth = 1)
    {
        Node updateNode = NodeFromWorldPoint(updatePoint);

        int checkX;
        int checkY;

        for (int x = -updateDepth; x <= updateDepth; x++)
        {
            for (int y = -updateDepth; y <= updateDepth; y++)
            {
                checkX = updateNode.gridX + x;
                checkY = updateNode.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX)
                {
                    if (checkY >= 0 && checkY < gridSizeY)
                    {
                        Vector3 worldPosition = topRight + Vector3.left * (checkX * nodeDiameter + nodeRadius) + Vector3.down * (checkY * nodeDiameter + nodeRadius);
                        bool blocking = false;

                        Collider2D[] cast = Physics2D.OverlapCircleAll(worldPosition, nodeRadius, blockingLayer);

                        foreach (Collider2D c in cast)
                        {                            
                            if (c.enabled && !c.isTrigger)
                            {
                                blocking = true;
                                break;
                            }
                        }
                        nodeArray[checkX, checkY] = new Node(blocking, worldPosition, checkX, checkY);
                    }
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y));

        if (nodeArray != null)
        {
            foreach (Node n in nodeArray)
            {

                Gizmos.color = walkable;
                if (n.isBlocker)
                {
                    Gizmos.color = blocked;
                }
                
                Gizmos.DrawWireCube(n.position, Vector3.one * (nodeDiameter - spacing));                
            }            
        }
    }
}