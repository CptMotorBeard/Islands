using UnityEngine;

public class Node {

    public int gridX;               // Grid position
    public int gridY;

    public Vector3 position;        // Real world position

    public bool isBlocker;          // If this node is blocked
    public bool isClosed;           // If this node has been looked at for pathfinding
    public bool isOpen;             // If this node has been added to open list
    
    public Node parentNode;         // Parent node for the A* Algorithm

    public int gCost;               // Costs for the A* Algorithm
    public int hCost;
    public int fCost { get { return gCost + hCost; } }

    public Node (bool _isBlocker, Vector3 _position, int _gridX, int _gridY)
    {
        isBlocker = _isBlocker;

        position = _position;

        gridX = _gridX;
        gridY = _gridY;

        isClosed = false;
        isOpen = false;
    }
}