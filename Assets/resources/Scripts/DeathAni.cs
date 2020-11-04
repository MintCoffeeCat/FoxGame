using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAni : MonoBehaviour
{
    [Tooltip("音效")]
    public AudioSource deathAudio;
    private bool isInit { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        while (!isInit) continue;
        if(deathAudio != null)
        {
            this.deathAudio.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setAudio(string s)
    {
        AudioClip clip = Resources.Load<AudioClip>(s);
        if (clip != null)
        {
            this.deathAudio = this.gameObject.AddComponent<AudioSource>();
            this.deathAudio.clip = clip;
            this.deathAudio.volume = 0.3f;
        }
        this.isInit = true;
    }
    public void Kill()
    {

        if(this.deathAudio != null)
        {
            while (deathAudio.isPlaying) continue;
        }
        Destroy(this.gameObject);
    }
}
