using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioManager Instance;
    private AudioSource bgmSource;//²¥·ÅBGM
    private void Awake()
    {
        Instance = this;
    }
    public void Init()
    {
      bgmSource = gameObject.AddComponent<AudioSource>();

    }
    public void  PlayBgm(string name,bool isLoop = true)
    {

      AudioClip clip = Resources.Load<AudioClip>("Sounds/BGM/" + name);
        bgmSource.clip = clip;
        bgmSource.loop = isLoop;
        bgmSource.Play();
    }
    public void PlayEffect(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sounds/" + name);
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
