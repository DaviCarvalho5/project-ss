using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
  Vector2 mousePosition, lookDirection;
  float targetAngle;
  Camera mainCamera;
  Rigidbody2D rb;

  void Start()
  {
    mainCamera = Camera.main;
    rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
  }

  void FixedUpdate()
  {
    if (PlayerAttributes.isLive && !GameStates.isInventoryOpen)
    {
      LookToMousePosition();
    }
  }

  void LookToMousePosition()
  {
    mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    lookDirection = mousePosition - rb.position;

    targetAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

    transform.rotation = Quaternion.Euler(0, 0, targetAngle);
  }
}
