using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemWindowScript : MonoBehaviour
{
    private Animation anim;
    public Image[] image;
    [SerializeField] Button escButton;
    [SerializeField] GameObject ItemButton;
    [SerializeField] GameObject number;
    Image[] item;
    Image[] frame;
    GameObject[] num;
    int itemNum;
    Sprite[] sprites;
    ObtainImage itemImage;

    bool stop;

    void Start()
    {
        anim = GetComponent<Animation>();
        item = new Image[5];
        frame = new Image[5];
        num = new GameObject[5];
        itemNum = 0;

        itemImage = FindObjectOfType<ObtainImage>();
        sprites = Resources.LoadAll<Sprite>("Item");
    }

    private void Update()
    {
        escButton.onClick.AddListener(deleteAnim);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            deleteAnim();
        }
    }

    public void showAnim()
    {
        anim.Play("showWindow");
    }

    public void deleteAnim()
    {
        anim.Play("deleteWindow");
        StartCoroutine("showItemButton");
    }
    
    IEnumerator obtainItem(string imageName)
    {
        yield return new WaitForSeconds(0.5f);

        if (itemNum <= item.Length)
        {
            if (image != null)
            {
                if (imageName == "book")
                {
                    instantiateItem(itemNum, image[0], "book");
                }
                if (imageName == "silverKey")
                {
                    instantiateItem(itemNum, image[1], "silverKey");
                }
                if (imageName == "goldKey")
                {
                    instantiateItem(itemNum, image[2], "goldKey");
                }
                if (imageName == "pliers")
                {
                    instantiateItem(itemNum, image[4], "pliers");
                }

                instantiateFrame(itemNum, image[3]);
            }
        }
        if(!stop)
            itemNum += 1;
    }

    IEnumerator showItemButton()
    {
        yield return new WaitForSeconds(0.5f);
        ItemButton.SetActive(true);
    }

    void instantiateItem(int itemNum, Image image, string name)
    {
        check(name);

        if (!stop)
        {
            item[itemNum] = Instantiate(image, new Vector3(0, -2f, 0), Quaternion.identity);
            item[itemNum].GetComponent<Animator>().SetTrigger("Spawn");
        }
    }

    void instantiateFrame(int itemNum, Image image)
    {
        if (!stop)
        {
            frame[itemNum] = Instantiate(image, new Vector3(-400f + (itemNum * 130f), 0, 0), Quaternion.identity);
            frame[itemNum].transform.SetParent(this.transform, false);
            frame[itemNum].transform.localScale = new Vector3(0.3f, 1.4f, 0.2f);
            item[itemNum].transform.SetParent(frame[itemNum].transform, false);
        }
    }

    void check(string name)
    {
        if(stop)
            stop = false;

        for (int i = 0; i < itemNum; i++)
        {
            if (item[i].transform.name == name + "(Clone)")
            {
                num[i] = Instantiate(number, new Vector3(0, -2f, 0), Quaternion.identity);
                num[i].transform.SetParent(item[i].transform, false);
                num[i].transform.localScale = new Vector3(0.5f, 0.3f, 0.5f);
                num[i].transform.localPosition = new Vector3(200f, -200f, 0);
                num[i].transform.GetComponent<Animator>().SetTrigger("Spawn");

                stop = true;
            }
        }
    }

}
