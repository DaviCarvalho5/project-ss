using UnityEngine;

public class InvItem
{
  public Item item;
  public int quant;
  public GameObject rep;

  public InvItem(Item _item, int _quant, GameObject _rep = null)
  {
    item = _item;
    quant = _quant;
    rep = _rep;
  }
}
