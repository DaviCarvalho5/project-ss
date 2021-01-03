using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  private float scaleStep;
  private float scaleTarget;
  private float cameraDistance;
  private float cameraScale;
  void Start()
  {
    scaleStep = 3.5f;
    scaleTarget = Camera.main.GetComponent<Camera>().orthographicSize;
  }

  void Update()
  {
    cameraScale = Camera.main.GetComponent<Camera>().orthographicSize;
    cameraDistance = Mathf.Abs(cameraScale - scaleTarget);

    if (cameraDistance > 0.01f)
    {
      Camera.main.GetComponent<Camera>().orthographicSize = Mathf.Lerp(cameraScale, scaleTarget, 0.1f);
    }

    scaleTarget = Mathf.Clamp(cameraScale - (Input.mouseScrollDelta.y * scaleStep), 2f, 12f);
  }
}
