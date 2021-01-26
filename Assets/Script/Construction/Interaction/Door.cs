using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
  public bool isOpen = false;
  float rotZ;
  private void Start()
  {
    rotZ = transform.rotation.z;
  }

  public void ChangeState()
  {
    SetOpen(!isOpen);
  }

  public void SetOpen(bool state)
  {
    isOpen = state;
    UpdatePosition();
  }

  public void UpdatePosition()
  {
    if (!isOpen)
    {
      transform.rotation = Quaternion.Euler(0, 0, rotZ);
      GetComponent<BoxCollider2D>().isTrigger = false;
    }
    else
    {
      transform.rotation = Quaternion.Euler(0, 0, rotZ + 90f);
      GetComponent<BoxCollider2D>().isTrigger = true;
    }
  }
}
