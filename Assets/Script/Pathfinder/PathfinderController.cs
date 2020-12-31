using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfinderController : MonoBehaviour
{
  [SerializeField] private GameObject gridSprite;
  [SerializeField] private LayerMask unwalkableLayer;
  [SerializeField] private bool renderGrid;
  public GameObject enemy;
  public static Grid worldGrid;
  void Start()
  {
    worldGrid = new Grid(WorldSettings.worldWidthinCells, WorldSettings.worldHeightinCells, WorldSettings.cellRadius, gridSprite, unwalkableLayer, renderGrid);

    Findpath((Vector2)enemy.transform.position, worldGrid.targetPosition);
    worldGrid.DrawPath();
  }

  void OnDrawGizmos()
  {
    Gizmos.color = new Color(1, 1, 1, 0.1f);
    Gizmos.DrawCube(new Vector3(WorldSettings.worldWidthinCells * WorldSettings.cellRadius, WorldSettings.worldHeightinCells * WorldSettings.cellRadius, 0), new Vector3(WorldSettings.worldWidthinCells * WorldSettings.cellDiameter, WorldSettings.worldHeightinCells * WorldSettings.cellDiameter, 0));
  }

  public void Findpath(Vector2 _startGridPosition, Vector2 _targetGridPosition)
  {
    Debug.Log("Findpath");
    Vector2 startPosition = Grid.WorldPositionToGridPosition(_startGridPosition * WorldSettings.cellDiameter);
    Vector2 targetPosition = Grid.WorldPositionToGridPosition(_targetGridPosition * WorldSettings.cellDiameter);
    List<Node> openSet = new List<Node>();
    HashSet<Node> closedSet = new HashSet<Node>();

    Node startNode = new Node(true, startPosition, (int)_startGridPosition.x, (int)_startGridPosition.y);
    Node targetNode = new Node(true, targetPosition, (int)_targetGridPosition.x, (int)_targetGridPosition.y);

    openSet.Add(startNode);

    while (openSet.Count > 0)
    {
      Debug.Log("While");
      Debug.Log(startNode.walkable == targetNode.walkable);
      Node curretNode = openSet[0];

      for (int i = 0; i < openSet.Count; i++)
      {
        if (openSet[i].fCost < curretNode.fCost || openSet[i].fCost == curretNode.fCost && openSet[i].hCost < curretNode.hCost)
        {
          curretNode = openSet[i];
        }
      }

      openSet.Remove(curretNode);
      closedSet.Add(curretNode);

      if (curretNode == targetNode)
      {
        Debug.Log("Cur = Target");
        RetracePath(startNode, targetNode);
        return;
      }

      foreach (Node neighbour in worldGrid.GetNeighbours(curretNode))
      {
        if (!neighbour.walkable || closedSet.Contains(curretNode))
        {
          continue;
        }

        int newMovimentCostForNeighbour = curretNode.gCost + getNodesDistance(curretNode, neighbour);
        if (newMovimentCostForNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
        {
          Debug.Log("a");
          neighbour.gCost = newMovimentCostForNeighbour;
          neighbour.hCost = getNodesDistance(neighbour, targetNode);
          neighbour.parent = curretNode;

          if (!openSet.Contains(neighbour))
          {
            openSet.Add(neighbour);
          }
        }
      }
    }
  }

  static void RetracePath(Node startNode, Node endNode)
  {
    List<Node> path = new List<Node>();
    Node currentNode = endNode;
    while (currentNode != startNode)
    {
      path.Add(currentNode);
      currentNode = currentNode.parent;
    }

    path.Reverse();
    Debug.Log(path);
    worldGrid.path = path;
  }

  static int getNodesDistance(Node nodeA, Node nodeB)
  {
    int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
    int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

    if (distX < distY)
      return 14 * distX + 10 * (distY - distX);
    return 14 * distY + 10 * (distX - distY);
  }
}
