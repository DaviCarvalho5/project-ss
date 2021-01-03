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

  private void Awake()
  {
    Sprite woodenBoxSprite = Array.Find(sprites, sprite => sprite.itemName == "Wooden Box").sprite;
    woodenBox.setSprite(woodenBoxSprite);
    woodenBox.setSpriteOnHand(woodenBoxSprite);
    woodenBox.setBuildSprite(woodenBoxSprite);

    Sprite pistolSprite = Array.Find(sprites, sprite => sprite.itemName == "Pistol").sprite;
    pistol.setSprite(pistolSprite);
    pistol.setSpriteOnHand(pistolSprite);
  }
}
