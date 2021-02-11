using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thunderSound : MonoBehaviour
{
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
        StartCoroutine("thunder");
    }

    IEnumerator thunder()
    {
        while (true)
        {
            yield return new WaitForSeconds(10F);
            audioSource.volume = 0.06f;
            audioSource.Play();
            yield return new WaitForSeconds(10F);
        }
    }
}
