using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cabinetAnim : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] AudioClip[] audio;
    AudioSource audioSource;
    RaycastHit hitInfo; 
    GameObject choice;

    InteractionController interController;
    int num;

    // Start is called before the first frame update
    void Start()
    {
        interController = FindObjectOfType<InteractionController>();
        audioSource = this.GetComponent<AudioSource>();
        cam = Camera.main;
        num = 0; 
        choice = GameObject.Find("UI").transform.GetChild(5).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        CheckObject();
    }

    void CheckObject()
    {
        Vector3 t_MousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

        //ScreenPpintToRay는 2d상의 마우스 위치를 카메라가 보고있는 3d상의 위치값으로 바꿔줌
        if (Physics.Raycast(cam.ScreenPointToRay(t_MousePos), out hitInfo, 5))
        {
            Contact();
        }
    }

    void Contact()
    {
        interController.crosshairInter();
        if (hitInfo.transform.gameObject == this.gameObject)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (this.gameObject.tag != "cabinet")
                {
                    startAnim();
                }
            }
        }
    }

    public void startAnim()
    {
        if (!choice.activeSelf)
        {
            num += 1;
            if (num % 2 == 1)
            {
                audioSource.clip = audio[0];
                audioSource.Play();
                this.gameObject.GetComponent<Animator>().SetTrigger(this.gameObject.tag + "open");
            }
            else
            {
                audioSource.clip = audio[1];
                audioSource.Play();
                this.gameObject.GetComponent<Animator>().SetTrigger(this.gameObject.tag + "close");
            }
        }
    }
}
