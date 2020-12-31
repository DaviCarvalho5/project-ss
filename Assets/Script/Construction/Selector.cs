using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
  void Update()
  {
    // X moviment
    if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > this.transform.position.x + WorldSettings.cellDiameter)
    {
      this.transform.position = new Vector2(this.transform.position.x + WorldSettings.cellDiameter, this.transform.position.y);
    }
    else if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < this.transform.position.x)
    {
      this.transform.position = new Vector2(this.transform.position.x - WorldSettings.cellDiameter, this.transform.position.y);
    }

    // Y moviment
    if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y > this.transform.position.y + WorldSettings.cellDiameter)
    {
      this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + WorldSettings.cellDiameter);
    }
    else if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y < this.transform.position.y)
    {
      this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - WorldSettings.cellDiameter);
    }

    // X limit
    if (this.transform.position.x < 0)
    {
      this.transform.position = new Vector2(0f, this.transform.position.y);
    }
    else if (this.transform.position.x > WorldSettings.worldWidthinCells * WorldSettings.cellDiameter - WorldSettings.cellDiameter)
    {
      this.transform.position = new Vector2(WorldSettings.worldWidthinCells * WorldSettings.cellDiameter - WorldSettings.cellDiameter, this.transform.position.y);
    }

    // Y limit
    if (this.transform.position.y < 0)
    {
      this.transform.position = new Vector2(this.transform.position.x, 0f);
    }
    else if (this.transform.position.y > WorldSettings.worldHeightinCells * WorldSettings.cellDiameter - WorldSettings.cellDiameter)
    {
      this.transform.position = new Vector2(this.transform.position.x, WorldSettings.worldHeightinCells * WorldSettings.cellDiameter - WorldSettings.cellDiameter);
    }
  }
}
