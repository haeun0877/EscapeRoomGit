using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startScript : MonoBehaviour
{
    [SerializeField] Button StartB;
    [SerializeField] Button Explain;
    [SerializeField] GameObject explainW;
    [SerializeField] Button closeB;

    // Start is called before the first frame update
    void Start()
    {
        Explain.onClick.AddListener(showExplainW);
        StartB.onClick.AddListener(startScene);
        closeB.onClick.AddListener(closeW);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void showExplainW()
    {
        if (!explainW.activeSelf)
            explainW.SetActive(true);
    }

    void startScene()
    {
        SceneManager.LoadScene("Main");
    }

    void closeW()
    {
        explainW.SetActive(false);
    }
}
