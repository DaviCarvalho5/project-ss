using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
  public Texture2D normalSpriteNS, constructorSpriteNS, pointSpriteNS;
  public static Texture2D normalSprite, constructorSprite, pointSprite;

  private void Awake()
  {
    normalSprite = normalSpriteNS;
    constructorSprite = constructorSpriteNS;
    pointSprite = pointSpriteNS;
  }

  public static void SetCursorType(string type)
  {
    Texture2D cursorSprite = null;

    if (type == "normal")
    {
      cursorSprite = normalSprite;
    }
    if (type == "constructor")
    {
      cursorSprite = constructorSprite;
    }
    if (type == "point")
    {
      cursorSprite = pointSprite;
    }

    Cursor.SetCursor(cursorSprite, Vector2.zero, CursorMode.ForceSoftware);
  }

  public static void UpdateCursor()
  {
    if (GameStates.isInventoryOpen)
    {
      SetCursorType("normal");
      Cursor.visible = true;
    }
    else if (GameStates.isBuilding)
    {
      Cursor.visible = false;
    }
    else
    {
      SetCursorType("point");
      Cursor.visible = true;
    }
  }
}
