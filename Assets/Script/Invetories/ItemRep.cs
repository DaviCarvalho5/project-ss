using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRep : MonoBehaviour
{
  public Item item;
  public int quant;
  public bool taked;

  private void Update()
  {
    if (taked)
    {
      float x = Input.mousePosition.x + 100f * UIManager.scaleFactor;
      float y = Input.mousePosition.y + 100f * UIManager.scaleFactor;
      transform.position = new Vector2(x, y);
    }
  }
}
