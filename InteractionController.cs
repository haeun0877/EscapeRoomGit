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
    [SerializeField] Image ObtainImage;

    RaycastHit hitInfo;
    ItemScript itemBar;

    ItemWindowScript itemWindow;

    bool interacting;

    private void Start()
    {
        itemBar = FindObjectOfType<ItemScript>();
        interacting = false;
        itemWindow = FindObjectOfType<ItemWindowScript>();

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
        if (hitInfo.transform.CompareTag("cabinet"))
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
                text.text = "박스를 치우시겠습니까? (y/n)";
                TextBar.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                Obtain.SetActive(false);
                TextBar.SetActive(false);
                interacting = false;
                hitInfo.collider.gameObject.SetActive(false);
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
                text.text = "아이템을 획득하시겠습니까? (y/n)";
                TextBar.SetActive(true);
            }
            inputKey("book");
        }
        else if (hitInfo.transform.CompareTag("silverkey"))
        {
            go_InteractiveCrosshair.SetActive(true);
            go_NomalCrosshair.SetActive(false);

            if (Input.GetMouseButtonDown(0))
            {
                interacting = true;
                Obtain.SetActive(true);
                ObtainImage.sprite = Resources.Load<Sprite>("Item\bookImage");
                text.text = "은색 키를 획득하시겠습니까? (y/n)";
                TextBar.SetActive(true);
            }
            inputKey("silverKey");
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

    void inputKey(string image)
    {
        if (interacting)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                itemBar.Click();
                Obtain.SetActive(false);
                TextBar.SetActive(false);
                interacting = false;
                itemWindow.StartCoroutine("obtainItem", image);
                hitInfo.collider.gameObject.SetActive(false);
               
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
