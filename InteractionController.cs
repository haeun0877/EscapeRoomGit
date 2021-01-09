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
    [SerializeField] Text ObtainText;

    RaycastHit hitInfo;
    ItemScript itemBar;

    ItemWindowScript itemWindow;
    MoveScript moveScript;

    Sprite[] sprites;

    bool interacting;
    bool click;

    private void Start()
    {
        itemBar = FindObjectOfType<ItemScript>();
        interacting = false;
        click = false;
        itemWindow = FindObjectOfType<ItemWindowScript>();
        moveScript = FindObjectOfType<MoveScript>();

        sprites = Resources.LoadAll<Sprite>("Item");

        Cursor.visible = false;
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
            justThingGuide("열쇠가 필요합니다");
            
        }
        else if (hitInfo.transform.CompareTag("box"))
        {
            go_InteractiveCrosshair.SetActive(true);
            go_NomalCrosshair.SetActive(false);
            if (Input.GetMouseButtonDown(0))
            {
                moveScript.click = true;// 캐릭터의 시선 움직임을 고정시킴
                text.text = "박스를 치우시겠습니까? (y/n)";
                TextBar.SetActive(true);
                click = true; 
            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                moveScript.click = false;
                Obtain.SetActive(false);
                TextBar.SetActive(false);
                interacting = false;
                hitInfo.collider.gameObject.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                moveScript.click = false;
                interacting = false;
            }

        }
        else if (hitInfo.transform.CompareTag("picture"))
        {
            justThingGuide("그림입니다");
        }
        else if (hitInfo.transform.CompareTag("interacte"))
        {
            go_InteractiveCrosshair.SetActive(true);
            go_NomalCrosshair.SetActive(false);

            obtainItemGuide("책", 0);
            inputKey("book");
        }
        else if (hitInfo.transform.CompareTag("silverkey"))
        {
            go_InteractiveCrosshair.SetActive(true);
            go_NomalCrosshair.SetActive(false);

            obtainItemGuide("은색키", 2);
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

    void obtainItemGuide(string name, int imageNum)
    {
        if (Input.GetMouseButtonDown(0))
        {
            interacting = true;
            Obtain.SetActive(true);
            ObtainImage.sprite = sprites[imageNum];
            text.text = name + " 획득하시겠습니까? (y/n)";
            TextBar.SetActive(true);
            ObtainText.text = name;
        }
    }

    void justThingGuide(string bartext)
    {
        go_InteractiveCrosshair.SetActive(true);
        go_NomalCrosshair.SetActive(false);
        if (Input.GetMouseButtonDown(0))
        {
            text.text = bartext;
            TextBar.SetActive(true);
        }
    }

}
