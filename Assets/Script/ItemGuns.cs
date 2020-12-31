using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGuns : Item
{
  public int damage;
  public ItemGuns(string _itemName, bool _handUsable, int _maxStack, Sprite _sprite, bool _buildable, int _damage, bool _gun = true)
  : base(_itemName, _handUsable, _maxStack, _sprite, _buildable, _gun)
  {
    damage = _damage;
  }
}
