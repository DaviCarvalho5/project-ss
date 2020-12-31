using UnityEngine;

public class CreateSlots : MonoBehaviour
{
  public GameObject slotGameObject;
  public Vector2Int slotsCount;
  public string zone;
  void Start()
  {
    int id = 0;
    for (int i = 0; i < slotsCount.y; i++)
    {
      for (int j = 0; j < slotsCount.x; j++)
      {
        GameObject slot = Instantiate(slotGameObject);
        slot.GetComponent<InventorySlot>().id = id;
        slot.GetComponent<InventorySlot>().zone = zone;
        slot.transform.SetParent(transform);
        slot.transform.localScale = Vector3.one;
        id++;
      }
    }
  }

  void Update()
  {

  }
}
