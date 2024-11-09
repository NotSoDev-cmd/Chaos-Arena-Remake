using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    private List<KeyValuePair<AudioSource, string>> sources;
    // Start is called before the first frame update
    void Start()
    {
        sources = new List<KeyValuePair<AudioSource, string>>();
        foreach(Sound s in sounds) {
            AudioSource a = gameObject.AddComponent<AudioSource>();
            a.clip = s.clip;
            a.volume = s.volume;
            a.pitch = s.pitch;
            sources.Add(new KeyValuePair<AudioSource, string>(a, s.name));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(string name) {
       // Debug.Log(name);
        foreach(KeyValuePair<AudioSource, string> pair in sources) {
            if (pair.Value == name) {
                //Debug.Log("found");
                pair.Key.Play();
            }
        }
    }
}

[System.Serializable]
public struct Sound {
    public string name;
    public AudioClip clip;
    public float volume;
    public float pitch;
}