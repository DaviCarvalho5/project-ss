using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAtTime : MonoBehaviour
{
  float waitTime;
  void Start()
  {
    waitTime = 5f;
    StartCoroutine(fadeAndDestroy(waitTime));
  }
  IEnumerator fadeAndDestroy(float t)
  {
    yield return new WaitForSeconds(t);
    Destroy(gameObject);
  }

  void FixedUpdate()
  {
    float alpha = GetComponent<SpriteRenderer>().color.a - Time.deltaTime / (waitTime);
    GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha);
  }

}
