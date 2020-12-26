using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour
{
    [SerializeField] Camera cam;

    [SerializeField] GameObject go_NomalCrosshair;
    [SerializeField] GameObject go_InteractiveCrosshair;

    [SerializeField] GameObject TextBar;
    [SerializeField] Text text;

    [SerializeField] GameObject Obtain;

    RaycastHit hitInfo;
    ItemScript itemBar;

    ItemWindowScript itemWindow;
    ObtainImage obtainImageS;

    bool interacting;

    private void Start()
    {
        itemBar = FindObjectOfType<ItemScript>();
        interacting = false;
        itemWindow = FindObjectOfType<ItemWindowScript>();
        obtainImageS = FindObjectOfType<ObtainImage>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckObject();
        inputKey();
    }

    void CheckObject()
    {
        Vector3 t_MousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

        //ScreenPpintToRay는 2d상의 마우스 위치를 카메라가 보고있는 3d상의 위치값으로 바꿔줌
        if(Physics.Raycast(cam.ScreenPointToRay(t_MousePos), out hitInfo, 5))
        {
            Contact();
        }
        else
        {
            NotContact();
        }
    }

    void Contact()
    {
        if (hitInfo.transform.CompareTag("key"))
        {
            go_InteractiveCrosshair.SetActive(true);
            go_NomalCrosshair.SetActive(false);
            if (Input.GetMouseButtonDown(0))
            {
                text.text = "열쇠가 필요합니다";
                TextBar.SetActive(true);
            }
            
        }
        else if (hitInfo.transform.CompareTag("box"))
        {
            go_InteractiveCrosshair.SetActive(true);
            go_NomalCrosshair.SetActive(false);
            if (Input.GetMouseButtonDown(0))
            {
                text.text = "박스를 치우시겠습니까?";
                TextBar.SetActive(true);
            }
        }
        else if (hitInfo.transform.CompareTag("picture"))
        {
            go_InteractiveCrosshair.SetActive(true);
            go_NomalCrosshair.SetActive(false);
            if (Input.GetMouseButtonDown(0))
            {
                text.text = "그림입니다";
                TextBar.SetActive(true);
            }
        }
        else if (hitInfo.transform.CompareTag("interacte"))
        {
            go_InteractiveCrosshair.SetActive(true);
            go_NomalCrosshair.SetActive(false);

            if (Input.GetMouseButtonDown(0))
            {
                interacting = true;
                Obtain.SetActive(true);
                text.text = "아이템을 획득하시겠습니까?";
                TextBar.SetActive(true);
            }
        }
        else
        {
            NotContact();
        }
    }

    void NotContact()
    {
        go_InteractiveCrosshair.SetActive(false);
        go_NomalCrosshair.SetActive(true);
        if (!interacting)
        {
            TextBar.SetActive(false);
        }
        
    }

    void inputKey()
    {
        if (interacting)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                itemBar.Click();
                Obtain.SetActive(false);
                TextBar.SetActive(false);
                interacting = false;
                itemWindow.StartCoroutine("obtainItem");
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                Obtain.SetActive(false);
                TextBar.SetActive(false);
                interacting = false;
            }
        }  
        
    }

}
