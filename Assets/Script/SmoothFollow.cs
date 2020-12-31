using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
  [SerializeField]
  private Transform target;

  [SerializeField]
  [Range(0.01f, 0.25f)]
  private float followSpeed;

  void Start()
  {
    followSpeed = 0.15f;
  }

  void FixedUpdate()
  {
    Vector3 cameraPostion = new Vector3(this.transform.position.x, this.transform.position.y, -10f);
    Vector3 targetPosition = new Vector3(target.position.x, target.position.y, -10f);
    this.transform.position = Vector3.Lerp(cameraPostion, targetPosition, followSpeed);
  }
}
