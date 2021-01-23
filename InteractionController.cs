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

    [SerializeField] GameObject panel;

    [SerializeField] GameObject choice;
    [SerializeField] Button yes;
    [SerializeField] Button no;

    [SerializeField] GameObject textUsing;

    RaycastHit hitInfo;
    ItemScript itemBar;

    ItemWindowScript itemWindow;
    MoveScript moveScript;
    ObtainImage obtainClass;
    cabinetAnim cabinet;

    GameObject nowGameObject;

    Sprite[] sprites;

    bool interacting;
    bool click;
    bool itemUsing;

    private void Start()
    {
        interacting = false;
        click = false;
        itemUsing = false;

        itemBar = FindObjectOfType<ItemScript>();
        itemWindow = FindObjectOfType<ItemWindowScript>();
        moveScript = FindObjectOfType<MoveScript>();
        obtainClass = FindObjectOfType<ObtainImage>();
        cabinet = FindObjectOfType<cabinetAnim>();

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
                choice.SetActive(false);
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
        moveScript.click = false;
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
                panel.SetActive(false);
                choice.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                Obtain.SetActive(false);
                TextBar.SetActive(false);
                interacting = false;
                panel.SetActive(false);
                choice.SetActive(false);
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
            panel.SetActive(true);
            choice.SetActive(false);
            moveScript.click = true;
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

    public void obtainvisual(string name, int imageNum, string cloneName)
    {
        Obtain.SetActive(true);
        ObtainImage.sprite = sprites[imageNum];
        ObtainText.text = name;
        panel.SetActive(true);
        choice.SetActive(true);
        no.onClick.AddListener(ClickNo);
        yes.onClick.AddListener(ClickYes);

        //nowGameObject는 현재 클릭된 게임오브젝트의 frame을 찾기위해 만들어짐
        nowGameObject = GameObject.Find("UI");
        nowGameObject = nowGameObject.transform.GetChild(1).gameObject;
        
        for(int i =2; i<nowGameObject.transform.childCount; i++)
        {
            if (nowGameObject.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.name == cloneName)
            {
                nowGameObject = nowGameObject.transform.GetChild(i).gameObject;
            }
        }
    }

    void ClickYes()
    {
        checkUsing();

        moveScript.click = false;

        if (itemUsing)
        {
            textUsing.SetActive(true);
            StartCoroutine("showUsingText");

        }
        else
        {
            Obtain.SetActive(false);
            TextBar.SetActive(false);
            interacting = false;
            panel.SetActive(false);
            choice.SetActive(false);
            nowGameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            nowGameObject.transform.parent.GetComponent<Image>().color = new Color(171 / 255f, 70 / 255f, 8 / 255f);
        }

    }

    void ClickNo()
    {
        moveScript.click = false;

        Obtain.SetActive(false);
        TextBar.SetActive(false);
        interacting = false;
        panel.SetActive(false);
        choice.SetActive(false);
        itemWindow.deleteAnim();
        nowGameObject.transform.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
        nowGameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
        
    }
  

    void checkUsing()
    {
        GameObject gameObject = GameObject.Find("UI").transform.GetChild(1).gameObject;
        int num = 0;

        for (int i =2; i<gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.activeSelf)
            {             
                num += 1;
            }
        }

        if (num == 0)
        {
            itemUsing = false;
        }else
        {
            itemUsing = true;
        }
    }

    IEnumerator showUsingText()
    {
        yield return new WaitForSeconds(1F);
        textUsing.SetActive(false);
    }
}
