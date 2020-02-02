using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : UnitySingletonPersistent<MusicPlayer>
{
    float musicTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().Play();
    }

    private void Update()
    {
         DontDestroyOnLoad(this.gameObject);
        musicTime = GetComponent<AudioSource>().time;
    }

    private void OnLevelWasLoaded(int level)
    {
        GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().time = musicTime;
    }

}
