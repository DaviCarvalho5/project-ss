using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
  public string itemName;
  public bool handUsable;
  public int maxStack;
  public Sprite sprite;
  public bool buildable;
  public bool gun;

  public Item(string _itemName, bool _handUsable, int _maxStack, Sprite _sprite, bool _buildable, bool _gun = false)
  {
    itemName = _itemName;
    handUsable = _handUsable;
    maxStack = _maxStack;
    sprite = _sprite;
    buildable = _buildable;
    gun = _gun;
  }
}
