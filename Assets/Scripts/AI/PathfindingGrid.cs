using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingGrid : MonoBehaviour
{

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

    public int blockingFeather;             // The feathering of the blocking nodes
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

                Collider2D[] cast = Physics2D.OverlapCircleAll(worldPosition, nodeRadius * blockingFeather, blockingLayer);

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

    public Node GetPathableNode(Vector2 worldPoint)
    {
        List<Node> OPEN = new List<Node>();
        HashSet<Node> CLOSED = new HashSet<Node>();

        Node currentNode = NodeFromWorldPoint(worldPoint);
        if (!currentNode.isBlocker)
            return currentNode;

        OPEN.Add(currentNode);
        int count = 0;

        while (OPEN.Count > 0 && count < 1000)
        {
            count++;
            currentNode = OPEN[0];

            OPEN.Remove(currentNode);
            CLOSED.Add(currentNode);

            List<Node> neighbours = GetNeighbouringNodes(currentNode);

            foreach (Node n in neighbours)
            {
                if (!n.isBlocker)
                    return n;

                if (!CLOSED.Contains(n))
                {                   
                    OPEN.Add(n);
                }
            }
        }

        return null;
    }

    public Node NodeAtPosition(int x, int y)
    {
        return nodeArray[x, y];
    }

    public bool BlockingNodeAt(int x, int y)
    {
        if (x < 0 || y < 0 || x > gridSizeX || y > gridSizeY)
            return true;

        return nodeArray[x, y].isBlocker;
    }

    public List<Node> GetNeighbouringNodes(Node node)
    {
        List<Node> neighbours = new List<Node>();

        int checkX, checkY;

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                checkX = node.gridX + x;
                checkY = node.gridY + y;
                if (checkX > 0 && checkX < gridSizeX)
                {
                    if (checkY > 0 && checkY < gridSizeY)
                    {
                        neighbours.Add(nodeArray[checkX, checkY]);
                    }
                }
            }
        }

        return neighbours;
    }

    public List<Node> GetDirectedNeighbouringNodes(Node node)
    {
        if (node.parentNode != null)
        {
            List<Node> neighbouringNodes = new List<Node>();

            int dirX = Mathf.Clamp(node.gridX - node.parentNode.gridX, -1, 1);
            int dirY = Mathf.Clamp(node.gridY - node.parentNode.gridY, -1, 1);

            int checkX, checkY;

            if (dirX != 0 && dirY != 0)
            {
                checkX = node.gridX + dirX;
                checkY = node.gridY + dirY;

                if (checkX > 0 && checkX < gridSizeX)
                {
                    if (checkY > 0 && checkY < gridSizeY)
                    {
                        if (!nodeArray[checkX, checkY].isBlocker)
                        {
                            neighbouringNodes.Add(nodeArray[checkX, checkY]);
                        }
                    }
                }

                checkX = node.gridX + dirX;
                checkY = node.gridY;

                if (checkX > 0 && checkX < gridSizeX)
                {
                    if (checkY > 0 && checkY < gridSizeY)
                    {
                        if (!nodeArray[checkX, checkY].isBlocker)
                        {
                            neighbouringNodes.Add(nodeArray[checkX, checkY]);
                        }
                    }
                }

                checkX = node.gridX;
                checkY = node.gridY + dirY;

                if (checkX > 0 && checkX < gridSizeX)
                {
                    if (checkY > 0 && checkY < gridSizeY)
                    {
                        if (!nodeArray[checkX, checkY].isBlocker)
                        {
                            neighbouringNodes.Add(nodeArray[checkX, checkY]);
                        }
                    }
                }
            }
            else
            {
                bool move = false, movePositive = false, moveNegative = false;

                if (dirX != 0)                  // Left / right
                {
                    checkX = node.gridX + dirX;
                    checkY = node.gridY;

                    if (checkX > 0 && checkX < gridSizeX)
                    {
                        move = nodeArray[checkX, checkY].isBlocker;

                        checkY = node.gridY - 1;
                        if (checkY > 0)
                        {
                            moveNegative = nodeArray[checkX, checkY].isBlocker;
                        }

                        checkY = node.gridY + 1;
                        if (checkY < gridSizeY)
                        {
                            movePositive = nodeArray[checkX, checkY].isBlocker;
                        }
                    }

                    if (move)
                    {
                        neighbouringNodes.Add(nodeArray[checkX, node.gridY]);
                        if (moveNegative)
                        {
                            neighbouringNodes.Add(nodeArray[checkX, node.gridY - 1]);
                        }
                        if (movePositive)
                        {
                            neighbouringNodes.Add(nodeArray[checkX, node.gridY + 1]);
                        }
                    }
                    if (moveNegative)
                    {
                        neighbouringNodes.Add(nodeArray[node.gridX, node.gridY - 1]);
                    }
                    if (movePositive)
                    {
                        neighbouringNodes.Add(nodeArray[node.gridX, node.gridY + 1]);
                    }
                }
                else if (dirY != 0)             // Up / down
                {
                    checkX = node.gridX;
                    checkY = node.gridY + dirY;

                    if (checkY > 0 && checkY < gridSizeY)
                    {
                        move = nodeArray[checkX, checkY].isBlocker;

                        checkX = node.gridX - 1;
                        if (checkX > 0)
                        {
                            moveNegative = nodeArray[checkX, checkY].isBlocker;
                        }

                        checkX = node.gridX + 1;
                        if (checkX < gridSizeX)
                        {
                            movePositive = nodeArray[checkX, checkY].isBlocker;
                        }
                    }

                    if (move)
                    {
                        neighbouringNodes.Add(nodeArray[node.gridX, checkY]);
                        if (moveNegative)
                        {
                            neighbouringNodes.Add(nodeArray[node.gridX - 1, checkY]);
                        }
                        if (movePositive)
                        {
                            neighbouringNodes.Add(nodeArray[node.gridX + 1, checkY]);
                        }
                    }
                    if (moveNegative)
                    {
                        neighbouringNodes.Add(nodeArray[node.gridX - 1, node.gridY]);
                    }
                    if (movePositive)
                    {
                        neighbouringNodes.Add(nodeArray[node.gridX + 1, node.gridY]);
                    }
                }
            }

            return neighbouringNodes;
        }
        else
        {
            return GetNeighbouringNodes(node);
        }
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

                        Collider2D[] cast = Physics2D.OverlapCircleAll(worldPosition, nodeRadius * blockingFeather, blockingLayer);

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