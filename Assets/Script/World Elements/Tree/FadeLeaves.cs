using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeLeaves : MonoBehaviour
{

  float alphaTarget = 1f;
  SpriteRenderer sr;

  void Start()
  {
    sr = GetComponent<SpriteRenderer>();
  }

  void Update()
  {
    if (Mathf.Abs(sr.color.a - alphaTarget) > 0.01f)
    {
      float a = Mathf.Lerp(sr.color.a, alphaTarget, 0.1f);
      sr.color = new Color(1, 1, 1, a);
    }
  }
  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      alphaTarget = 0.20f;
    }
  }

  void OnTriggerExit2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      alphaTarget = 1.0f;
    }
  }
}
