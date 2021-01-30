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
    [SerializeField] GameObject textSuccess;

    [SerializeField] GameObject picture;
    [SerializeField] GameObject bookcase;
    [SerializeField] GameObject tower;

    RaycastHit hitInfo;
    ItemScript itemBar;

    ItemWindowScript itemWindow;
    MoveScript moveScript;
    ObtainImage obtainClass;
    cabinetAnim cabinet;

    GameObject nowGameObject;
    GameObject gameObject;

    Sprite[] sprites;

    bool interacting;
    bool click;
    bool itemUsing;

    int screwNum;

    private void Start()
    {
        interacting = false;
        click = false;
        itemUsing = false;

        screwNum = 0;

        no.onClick.AddListener(ClickNo);
        yes.onClick.AddListener(ClickYes);

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
        if (hitInfo.transform.CompareTag("box"))
        {
            crosshairInter();
            if (Input.GetMouseButtonDown(0))
            {
                text.text = "박스를 치우시겠습니까? (y/n)";
                TextBar.SetActive(true);
                click = true;
            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                Obtain.SetActive(false);
                choice.SetActive(false);
                TextBar.SetActive(false);
                interacting = false;
                hitInfo.collider.gameObject.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                interacting = false;
            }

        }
        else if (hitInfo.transform.CompareTag("picture"))
        {
            crosshairInter();

            if (Input.GetMouseButtonDown(0))
                justThingGuide("그림입니다");
        }
        else if (hitInfo.transform.CompareTag("interacte"))
        {
            crosshairInter();

            obtainItemGuide("책", 0);
            inputKey("book");
        }
        else if (hitInfo.transform.CompareTag("silverkey"))
        {
            crosshairInter();

            obtainItemGuide("은색키", 3);
            inputKey("silverKey");
        }
        else if (hitInfo.transform.CompareTag("goldkey"))
        {
            crosshairInter();

            obtainItemGuide("골드키", 1);
            inputKey("goldKey");
        }
        else if (hitInfo.transform.CompareTag("cabinet"))
        {
            crosshairInter();

            if (Input.GetMouseButtonDown(0))
            {
                if (hitInfo.transform.localPosition.z < -45)
                {
                    hitInfo.transform.GetComponent<Animator>().SetTrigger("cabinetclose");
                }
                else
                {
                    checkUsing();
                    if (itemUsing) // 실버키가 사용중이고 캐비닛이 선택되었다면 캐비닛 문을 여는 애니메이션 실행
                    {
                        if (gameObject.transform.name == "silverKey(Clone)")
                        {
                            hitInfo.transform.GetComponent<Animator>().SetTrigger("cabinetopen");
                            StartCoroutine("showUsingSuccessT");

                            itemWindow.deleteAnim();
                            nowGameObject.transform.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
                            if (nowGameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.activeSelf)
                                nowGameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                        }
                        else
                        {
                            justThingGuide("열쇠가 필요합니다.");
                        }
                    }
                    else
                    {
                        justThingGuide("열쇠가 필요합니다.");
                    }
                }
            }
        }
        else if (hitInfo.transform.CompareTag("pliers"))
        {
            crosshairInter();

            obtainItemGuide("드라이버", 2);
            inputKey("pliers");
        }
        else if (hitInfo.transform.CompareTag("screw"))
        {
            crosshairInter();

            if (Input.GetMouseButtonDown(0))
            {
                checkUsing();
                if (itemUsing) 
                {
                    if (gameObject.transform.name == "plier(Clone)")
                    {
                        screwNum += 1;

                        hitInfo.transform.GetComponent<Animator>().SetTrigger("put");
                        StartCoroutine("showUsingSuccessT");
                        
                    }
                }

                if (screwNum >= 2)
                {
                    StartCoroutine("waitaMinute");

                    itemWindow.deleteAnim();
                    nowGameObject.transform.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
                    if (nowGameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.activeSelf)
                        nowGameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }
        else if (hitInfo.transform.CompareTag("book"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                checkUsing();
                if (itemUsing)
                {
                    if (gameObject.transform.name == "book(Clone)")
                    {
                        StartCoroutine("showUsingSuccessT");
                        bookcase.transform.GetComponent<Animator>().SetTrigger("open");

                        itemWindow.deleteAnim();
                        nowGameObject.transform.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
                        if (nowGameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.activeSelf)
                            nowGameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
            }
                
        }
        else if (hitInfo.transform.CompareTag("right"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                hitInfo.transform.GetComponent<Animator>().SetTrigger("put");
                tower.transform.GetComponent<Animator>().SetTrigger("open");
            }
        }
        else if (hitInfo.transform.CompareTag("button"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                hitInfo.transform.GetComponent<Animator>().SetTrigger("put");
            }
        }
        else if (hitInfo.transform.CompareTag("InputKey"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                justThingGuide("골드키가 필요합니다");
            }
        }
        else
        {
            NotContact();
        }
    }

    public void crosshairInter()
    {
        go_InteractiveCrosshair.SetActive(true);
        go_NomalCrosshair.SetActive(false);
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
        }
    }

    public void justThingGuide(string bartext)
    {
        text.text = bartext;
        TextBar.SetActive(true);

    }

    public void obtainvisual(string name, int imageNum, string cloneName)
    {
        Obtain.SetActive(true);
        ObtainImage.sprite = sprites[imageNum];
        ObtainText.text = name;
        panel.SetActive(true);
        choice.SetActive(true);

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

        if (itemUsing)
        {
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
        Obtain.SetActive(false);
        TextBar.SetActive(false);
        interacting = false;
        panel.SetActive(false);
        choice.SetActive(false);
        itemWindow.deleteAnim();
        nowGameObject.transform.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
        if(nowGameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.activeSelf)
            nowGameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
  

    void checkUsing()
    {
        gameObject = GameObject.Find("UI").transform.GetChild(1).gameObject;
        int num = 0;

        for (int i =2; i<gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.activeSelf)
            {             
                num += 1;
                gameObject = gameObject.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject;
                break;
            }
        }

        if (num == 0)
        {
            itemUsing = false;
        }
        else
        {
            itemUsing = true;
        }
    }

    IEnumerator showUsingText()
    {
        textUsing.SetActive(true);
        textUsing.gameObject.GetComponent<Animator>().SetTrigger("show");
        yield return new WaitForSeconds(1.3F);
        textUsing.gameObject.GetComponent<Animator>().SetTrigger("delete");
        yield return new WaitForSeconds(1.3F);
        textUsing.SetActive(false);
    }

    IEnumerator showUsingSuccessT()
    {
        textSuccess.SetActive(true);
        textSuccess.gameObject.GetComponent<Animator>().SetTrigger("show");
        yield return new WaitForSeconds(1.3F);
        textSuccess.gameObject.GetComponent<Animator>().SetTrigger("delete");
        yield return new WaitForSeconds(1.3F);
        textSuccess.SetActive(false);
    }

    IEnumerator waitaMinute()
    {
        yield return new WaitForSeconds(1.3F);
        picture.transform.GetComponent<Animator>().SetTrigger("fall");
    }

}
