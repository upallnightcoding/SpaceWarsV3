using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceCntrl : MonoBehaviour
{
    private AudioSource audioSource = null;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnSound(AudioClip audioClip)
    {
        StartCoroutine(PlaySound(audioClip));
    }

    private IEnumerator PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);

        yield return null;
    }

    private void OnEnable()
    {
        EventManager.Instance.OnSound += OnSound;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnSound -= OnSound;
    }
}
