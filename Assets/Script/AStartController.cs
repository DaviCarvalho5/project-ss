using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AStartController : MonoBehaviour
{
  // O tamanho do mundo está fixo
  GridGraph gridGraph;
  private void Start()
  {
    gridGraph = AstarPath.active.data.gridGraph;
  }
  public static Vector2 WorldPositionToGridPosition(Vector2 worldPosition)
  {
    int x = Mathf.RoundToInt(worldPosition.x / WorldSettings.cellDiameter);
    int y = Mathf.RoundToInt(worldPosition.y / WorldSettings.cellDiameter);

    return new Vector2(x, y);
  }

  public void UpdateNodeWalk(int x, int y, bool walkable)
  {
    gridGraph.GetNode(x, y).Walkable = walkable;
    Debug.Log("Graph updated");
  }
}
