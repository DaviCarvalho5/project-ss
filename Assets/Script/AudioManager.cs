using UnityEngine.Audio;

using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
  public Sound[] sounds;
  void Awake()
  {
    foreach (Sound s in sounds)
    {
      s.source = gameObject.AddComponent<AudioSource>();
      s.source.clip = s.clip;
      s.source.volume = s.volume;
    }
  }

  public void Play(string name)
  {
    Sound s = Array.Find(sounds, sound => sound.name == name);
    s.source.Play();
  }

  public void PlayLoop(string name, float pitch = 1f)
  {
    Sound s = Array.Find(sounds, sound => sound.name == name);
    s.source.pitch = pitch;
    if (!s.source.isPlaying)
    {
      s.source.loop = true;
      s.source.Play();
    }
  }

  public void Stop(string name)
  {
    Sound s = Array.Find(sounds, sound => sound.name == name);
    if (s.source.isPlaying)
    {
      s.source.Stop();
    }
  }

  public AudioSource GetSound(string name)
  {
    Sound s = Array.Find(sounds, sound => sound.name == name);
    return s.source;
  }
}
