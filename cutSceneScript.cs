using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;

public class cutSceneScript : MonoBehaviour
{
    public PlayableDirector PlayableDirector;

    // Start is called before the first frame update
    void Start()
    {
        PlayableDirector.gameObject.SetActive(true);
        PlayableDirector.Play();
        StartCoroutine("gameStart");

    }

    IEnumerator gameStart()
    {
        yield return new WaitForSeconds(13F);
        SceneManager.LoadScene("Main");
    }
}
