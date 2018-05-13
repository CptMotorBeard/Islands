using UnityEngine;

public class Node {

    public int gridX;               // Grid position
    public int gridY;

    public Vector3 position;        // Real world position

    public bool isBlocker;          // If this node is blocked
    
    public Node parentNode;         // Parent node for the A* Algorithm

    public int gCost;               // Costs for the A* Algorithm
    public int hcost;
    public int fCost { get { return gCost + hcost; } }

    public Node (bool _isBlocker, Vector3 _position, int _gridX, int _gridY)
    {
        isBlocker = _isBlocker;

        position = _position;

        gridX = _gridX;
        gridY = _gridY;
    }
}