using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{

    #region Singleton
    public static Pathfinding instance;

    void Awake()
    {
        if (instance)
        {
            Debug.LogWarning("Another instance of pathfinding already exists");
            return;
        }

        instance = this;
    }

    #endregion

    PathfindingGrid grid;

    void Start()
    {
        grid = PathfindingGrid.instance;
    }

    public bool JPS(Vector2 startPoint, Vector2 destination)        // Pathfinding using Jump Point Search algorithm
    {
        List<Node> OPEN = new List<Node>();             // Set of nodes to be evaluated
        HashSet<Node> CLOSED = new HashSet<Node>();     // Set of evaluated nodes

        Node startNode = grid.GetPathableNode(startPoint);
        Node targetNode = grid.GetPathableNode(destination);    // Find the start and destination nodes from world points        

        if (startNode == null || targetNode.isBlocker)
        {
            return false;
        }

        startNode.isOpen = true;
        OPEN.Add(startNode);                    // Start by adding the starting node to the open set

        while (OPEN.Count > 0)                  // Loop until there are no more possibilities
        {
            Node currentNode = OPEN[0];

            foreach (Node n in OPEN)
            {
                if (n.fCost < currentNode.fCost || n.fCost == currentNode.hCost && n.hCost < currentNode.hCost)
                    currentNode = n;            // Find the node in OPEN with the lowest f Cost
            }

            OPEN.Remove(currentNode);
            currentNode.isOpen = false;
            currentNode.isClosed = true;
            CLOSED.Add(currentNode);            // Remove the current node from OPEN and add it to CLOSED

            if (currentNode == targetNode)
            {
                foreach (Node n in CLOSED)
                    n.isClosed = false;

                foreach (Node n in OPEN)
                    n.isOpen = false;

                return true;                    // A path has been found
            }

            List<Node> neighbouringNodes = grid.GetDirectedNeighbouringNodes(currentNode);

            foreach (Node n in neighbouringNodes)
            {
                Node jumpPoint = Jump(n, currentNode, targetNode);      // Find our jump points

                if (jumpPoint != null && !jumpPoint.isClosed)
                {
                    int gCost = currentNode.gCost + GetDistance(currentNode, jumpPoint);

                    if (!jumpPoint.isOpen || gCost < jumpPoint.gCost)
                    {
                        jumpPoint.gCost = gCost;                                                    // Set or update the g cost of the jump point
                        jumpPoint.hCost = GetDistance(jumpPoint, targetNode);                       // Set the hCost of the jump point
                        jumpPoint.parentNode = currentNode;                                         // Set the parent of the jump point

                        if (!jumpPoint.isOpen)
                        {
                            jumpPoint.isOpen = true;
                            OPEN.Add(jumpPoint);                                              // If the jump point isn't open, add it
                        }
                    }
                }
            }
        }

        foreach (Node n in CLOSED)
            n.isClosed = false;

        Debug.Log("NO PATH");
        return false;                           // A path has not been found
    }

    public bool AStarPath(Vector2 startPoint, Vector2 destination)     // Pathfinding using A* algorithm
    {
        List<Node> OPEN = new List<Node>();             // Set of nodes to be evaluated
        HashSet<Node> CLOSED = new HashSet<Node>();     // Set of evaluated nodes

        Node startNode = grid.GetPathableNode(startPoint);
        Node targetNode = grid.GetPathableNode(destination);    // Find the start and destination nodes from world points        

        if (startNode == null || targetNode.isBlocker)
        {
            return false;
        }

        startNode.isOpen = true;
        OPEN.Add(startNode);                    // Start by adding the starting node to the open set

        while (OPEN.Count > 0)                  // Loop until there are no more possibilities
        {
            Node currentNode = OPEN[0];

            foreach (Node n in OPEN)
            {
                if (n.fCost < currentNode.fCost || n.fCost == currentNode.hCost && n.hCost < currentNode.hCost)
                    currentNode = n;            // Find the node in OPEN with the lowest f Cost
            }
            
            OPEN.Remove(currentNode);
            currentNode.isOpen = false;
            currentNode.isClosed = true;
            CLOSED.Add(currentNode);            // Remove the current node from OPEN and add it to CLOSED

            if (currentNode == targetNode)
            {
                foreach (Node n in CLOSED)
                    n.isClosed = false;

                foreach (Node n in OPEN)
                    n.isOpen = false;

                return true;                    // A path has been found
            }

            List<Node> neighbouringNodes = grid.GetNeighbouringNodes(currentNode);

            foreach (Node neighbour in neighbouringNodes)
            {
                if (!neighbour.isBlocker && !neighbour.isClosed)
                {
                    int gCost = currentNode.gCost + GetDistance(currentNode, neighbour);

                    if (!neighbour.isOpen || gCost < neighbour.gCost)           // If the new path is shorter or we've never looked at the node before
                    {
                        neighbour.gCost = gCost;                                // Set or update the gCost of the neighbour
                        neighbour.hCost = GetDistance(neighbour, targetNode);   // Set the hCost of the neighbour

                        neighbour.parentNode = currentNode;                     // Set the parent node of the neighbour

                        if (!neighbour.isOpen)
                        {
                            neighbour.isOpen = true;
                            OPEN.Add(neighbour);                                // Add neighbour to open if it's not there already
                        }

                    }
                }
            }
        }

        foreach (Node n in CLOSED)
            n.isClosed = false;

        Debug.Log("NO PATH");
        return false;                           // A path has not been found
    }

    public List<Node> GetPath(Vector2 startPoint, Vector2 destination)
    {
        List<Node> path = new List<Node>();

        Node startNode = grid.GetPathableNode(startPoint);
        Node targetNode = grid.GetPathableNode(destination);    // Find the start and destination nodes from world points

        Node currentNode = targetNode;                             // Start at the end node

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parentNode;                  // Traverse through the nodes parent until back at the start
        }

        path.Reverse();                                            // Reverse the list to get the path in the correct direction

        return path;
    }

    Node Jump(Node testNode, Node currentNode, Node targetNode)
    {
        int dx = testNode.gridX - currentNode.gridX;
        int dy = testNode.gridY - currentNode.gridY;

        if (grid.BlockingNodeAt(testNode.gridX, testNode.gridY))              // Can't jump to or through a blocker
            return null;

        if (testNode == currentNode)
            return null;

        if (testNode == targetNode)                 // If we reach our goal, continue
        {
            return testNode;
        }

        if (dx != 0 && dy != 0)                     // Check for forced neighbours diagonally
        {
            Node horizontal = grid.NodeAtPosition(testNode.gridX + dx, testNode.gridY);
            Node vertical = grid.NodeAtPosition(testNode.gridX, testNode.gridY + dy);

            if (Jump(horizontal, testNode, targetNode) != null || Jump(vertical, testNode, targetNode) != null)
                return targetNode;
        }
        else
        {
            if (dx != 0)                            // Check for forced neighbours horizontally
            {
                if ((!grid.BlockingNodeAt(testNode.gridX, testNode.gridY-1) && grid.BlockingNodeAt(currentNode.gridX, testNode.gridY - 1))
                    || (!grid.BlockingNodeAt(testNode.gridX, testNode.gridY + 1) && grid.BlockingNodeAt(currentNode.gridX, testNode.gridY + 1)))
                {
                    return testNode;
                }
            }
            else if (dy != 0)                       // Check for forced neighbours vertically
            {
                if ((!grid.BlockingNodeAt(testNode.gridX - 1, testNode.gridY) && grid.BlockingNodeAt(testNode.gridX - 1, currentNode.gridY))
                    || (!grid.BlockingNodeAt(testNode.gridX + 1, testNode.gridY) && grid.BlockingNodeAt(testNode.gridX + 1, currentNode.gridY)))
                {
                    return testNode;
                }
            }
        }

        if (!grid.BlockingNodeAt(testNode.gridX + dx, testNode.gridY) && !grid.BlockingNodeAt(testNode.gridX, testNode.gridY + dy))     // Only allow diagonal movement if there is space
        {
            Node diagonal = grid.NodeAtPosition(testNode.gridX + dx, testNode.gridY + dy);
            return Jump(diagonal, testNode, targetNode);
        }
        return null;
    }

    int GetDistance(Node a, Node b)
    {
        int deltaX = Mathf.Abs(a.gridX - b.gridX);
        int deltaY = Mathf.Abs(a.gridY - b.gridY);

        return (deltaX + deltaY);
    }
}
