using UnityEngine;
using System;

[System.Serializable]
public class ListOfItems : MonoBehaviour
{
  public ItemSprite[] sprites;
  public static Sprite nullSprite = null;

  public static Item voiditem = new Item(
   itemName: "void",
   description: "",
   maxStack: 1,
   sprite: nullSprite
   );

  public static BuildableItem woodenBox = new BuildableItem(
    name: "Wooden Box",
    description: "Buildable. Is it, man... A wooden box",
    maxStack: 64,
    sprite: nullSprite,
    isTwoHands: false,
    spriteOnHand: nullSprite,
    size: new Vector2Int(1, 1),
    buildSprite: null
    );

  public static BuildableItem stoneBlock = new BuildableItem(
  name: "Stone Block",
  description: "Buildable. A big block of stone",
  maxStack: 64,
  sprite: nullSprite,
  isTwoHands: false,
  spriteOnHand: nullSprite,
  size: new Vector2Int(1, 1),
  buildSprite: null
  );

  public static GunItem pistol = new GunItem(
    name: "Pistol",
    description: "Civillian gun. Pow pow pow!",
    maxStack: 1,
    sprite: nullSprite,
    isTwoHands: false,
    spriteOnHand: nullSprite,
    damage: 1f,
    timeForNextShot: 0.3f,
    realoadTime: 1,
    ammunitionType: "civillian"
    );

  public static ToolItem hammer = new ToolItem(
    name: "Hammer",
    description: "Tool. Now you're the Thor.",
    maxStack: 1,
    sprite: nullSprite,
    isTwoHands: false,
    spriteOnHand: nullSprite,
    type: "hammer"
  );

  private void Awake()
  {
    Item[] itens = {
      woodenBox,
      pistol,
      stoneBlock,
      hammer
    };

    for (int i = 0; i < itens.Length; i++)
    {
      Sprite sprite = Array.Find(sprites, sprite => sprite.itemName == itens[i].itemName).sprite;
      itens[i].setSprite(sprite);

      if (itens[i] is HandableItem)
      {
        (itens[i] as HandableItem).setSpriteOnHand(sprite);
      }

      if (itens[i] is BuildableItem)
      {
        (itens[i] as BuildableItem).setBuildSprite(sprite);
      }
    }
  }
}
