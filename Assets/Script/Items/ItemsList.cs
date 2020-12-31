using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsList : MonoBehaviour
{
  [HideInInspector] public static Item woodBox, gun, zero;
  [SerializeField] Sprite woodBoxSprite;
  [SerializeField] Sprite gunSprite;
  void Awake()
  {
    zero = new Item(
        _itemName: "zero",
        _handUsable: false,
        _maxStack: 1,
        _sprite: null,
        _buildable: false
    );

    woodBox = new Item(
        _itemName: "Wood Box",
        _handUsable: false,
        _maxStack: 64,
        _sprite: woodBoxSprite,
        _buildable: true
    );

    gun = new ItemGuns(
        _itemName: "gun",
        _handUsable: true,
        _maxStack: 1,
        _sprite: gunSprite,
        _buildable: false,
        _gun: true,
        _damage: 1
    );
  }
}
