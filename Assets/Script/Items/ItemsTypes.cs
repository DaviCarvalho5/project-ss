using UnityEngine;

public class ItemsTypes
{

}

public class Item
{
  public string itemName;
  public string description;
  public int maxStack;
  public Sprite sprite;

  public Item(
    string itemName,
    string description,
    int maxStack,
    Sprite sprite)
  {
    this.itemName = itemName;
    this.description = description;
    this.maxStack = maxStack;
    this.sprite = sprite;
  }

  public void setSprite(Sprite sprite)
  {
    this.sprite = sprite;
  }

  public Sprite getSprite()
  {
    return this.sprite;
  }

}


public class HandableItem : Item
{
  bool isTwoHands;
  Sprite spriteOnHand;
  public HandableItem(
    string name,
    string description,
    int maxStack,
    Sprite sprite,
    bool isTwoHands,
    Sprite spriteOnHand)
  : base(name, description, maxStack, sprite)
  {
    this.isTwoHands = isTwoHands;
    this.spriteOnHand = spriteOnHand;
  }


  public void setSpriteOnHand(Sprite sprite)
  {
    this.spriteOnHand = sprite;
  }
}

public class ToolItem : HandableItem
{
  string type;
  public ToolItem(
    string name,
    string description,
    int maxStack,
    Sprite sprite,
    bool isTwoHands,
    Sprite spriteOnHand,
    string type)
  : base(name, description, maxStack, sprite, isTwoHands, spriteOnHand)
  {
    this.type = type;
  }
}

public class BuildableItem : HandableItem
{
  public Vector2Int size;
  public Sprite buildSprite;
  public BuildableItem(
    string name,
    string description,
    int maxStack,
    Sprite sprite,
    bool isTwoHands,
    Sprite spriteOnHand,
    Vector2Int size,
    Sprite buildSprite)
  : base(name, description, maxStack, sprite, isTwoHands, spriteOnHand)
  {
    this.size = size;
    this.buildSprite = buildSprite;
  }

  public void setBuildSprite(Sprite sprite)
  {
    this.buildSprite = sprite;
  }
}

public class WeponItem : HandableItem
{
  public float damage;
  public float timeForNextAttack;
  public WeponItem(
    string name,
    string description,
    int maxStack,
    Sprite sprite,
    bool isTwoHands,
    Sprite spriteOnHand,
    float damage,
    float timeForNextAttack)
  : base(name, description, maxStack, sprite, isTwoHands, spriteOnHand)
  {
    this.damage = damage;
    this.timeForNextAttack = timeForNextAttack;
  }
}

public class GunItem : HandableItem
{
  public float damage;
  public float timeForNextShot;
  public float realoadTime;
  public string ammunitionType;
  public GunItem(
    string name,
    string description,
    int maxStack,
    Sprite sprite,
    bool isTwoHands,
    Sprite spriteOnHand,
    float damage,
    float timeForNextShot,
    float realoadTime,
    string ammunitionType)
  : base(name, description, maxStack, sprite, isTwoHands, spriteOnHand)
  {
    this.damage = damage;
    this.timeForNextShot = timeForNextShot;
    this.realoadTime = realoadTime;
    this.ammunitionType = ammunitionType;
  }
}
