using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private AudioClip _explosionAudio;
    [SerializeField]
    private AudioSource _audioSource;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("Audio source is null");
        }
        else
        {
            _audioSource.clip = _explosionAudio;
        }
        _audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
