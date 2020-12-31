using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
  private int gridWidth, gridHeight;
  private Node[,] grid;
  private float cellRadius, cellDiameter;
  private GameObject gridSprite;
  private LayerMask unwalkableLayer;
  private List<GameObject> objectsCreated;
  public Vector2 targetPosition; // At world position insted grid position
  private bool renderGrid;

  public List<Node> path;

  public Grid(int _gridWidth, int _gridHeight, float _cellRadius, GameObject _gridSprite, LayerMask _unwalkableLayer, bool _renderGrid = false)
  {
    gridWidth = _gridWidth;
    gridHeight = _gridHeight;
    cellRadius = _cellRadius;
    cellDiameter = _cellRadius * 2;
    gridSprite = _gridSprite;
    unwalkableLayer = _unwalkableLayer;
    objectsCreated = new List<GameObject>();
    renderGrid = _renderGrid;

    grid = new Node[gridWidth, gridHeight];

    for (int x = 0; x < gridWidth; x++)
    {
      for (int y = 0; y < gridHeight; y++)
      {
        Vector2 worldPosition = new Vector2(x * cellDiameter, y * cellDiameter);
        grid[x, y] = new Node(true, worldPosition, x, y);
        grid[x, y].walkable = !Physics2D.OverlapCircle(new Vector2(worldPosition.x + cellRadius, worldPosition.y + cellRadius), cellRadius * 0.9f, unwalkableLayer);
      }
    }

    RenderGrid();
  }

  private void RenderGrid()
  {
    if (renderGrid)
    {
      foreach (Node n in grid)
      {
        GameObject oc = Instantiate(gridSprite, new Vector3(n.worldPosition.x, n.worldPosition.y, 0), Quaternion.identity);
        objectsCreated.Add(oc);
      }
      UpdateGridRender();
    }

  }

  public void UpdateGridPosition(int x, int y, Node val)
  {
    x = (int)Mathf.Clamp(x, 0, WorldSettings.worldWidthinCells - 1f);
    y = (int)Mathf.Clamp(y, 0, WorldSettings.worldHeightinCells - 1f);
    grid[x, y] = val;
    UpdateGridRender();
  }

  public void DrawPath()
  {
    foreach (Node n in path)
    {
      if (n.parent.worldPosition != null)
      {
        Debug.DrawLine(n.worldPosition, n.parent.worldPosition);
      }
    }
  }

  public void UpdateGridRender()
  {
    if (renderGrid)
    {
      foreach (Node n in grid)
      {
        foreach (GameObject g in objectsCreated)
        {
          if (n.worldPosition == (Vector2)g.transform.position)
          {
            if (targetPosition == n.worldPosition)
            {
              g.GetComponent<SpriteRenderer>().color = new Color(1, 1, 0, 0.5f);
            }
            else
            {
              g.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            }
            if (!n.walkable)
            {
              g.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);
            }
          }
        }
      }
    }
  }

  public void SetTargetPosition(Vector2 val)
  {
    val.x = Mathf.Clamp(val.x, 0, WorldSettings.worldWidthinCells * WorldSettings.cellDiameter - WorldSettings.cellDiameter);
    val.y = Mathf.Clamp(val.y, 0, WorldSettings.worldHeightinCells * WorldSettings.cellDiameter - WorldSettings.cellDiameter);
    targetPosition = val;
  }

  public static Vector2 WorldPositionToGridPosition(Vector2 worldPosition)
  {
    int x = Mathf.RoundToInt(worldPosition.x / WorldSettings.cellDiameter);
    int y = Mathf.RoundToInt(worldPosition.y / WorldSettings.cellDiameter);

    return new Vector2(x, y);
  }

  public List<Node> GetNeighbours(Node node)
  {
    List<Node> neighbours = new List<Node>();

    for (int x = -1; x <= 1; x++)
    {
      for (int y = -1; y <= 1; y++)
      {
        if (x == 0 && y == 0)
        {
          continue;
        }

        int checkX = node.gridX + x;
        int checkY = node.gridY + y;

        if (checkX >= 0 && checkX < WorldSettings.worldWidthinCells && checkY >= 0 && checkY < WorldSettings.worldHeightinCells)
        {
          neighbours.Add(grid[checkX, checkY]);
        }
      }
    }

    return neighbours;
  }
}
