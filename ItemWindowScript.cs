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
    Image[] item;
    Image[] frame;
    int itemNum;
    Sprite[] sprites;
    ObtainImage itemImage;

    void Start()
    {
        anim = GetComponent<Animation>();
        item = new Image[5];
        frame = new Image[5];
        itemNum = 0;

        itemImage = FindObjectOfType<ObtainImage>();
        sprites = Resources.LoadAll<Sprite>("Item");
    }

    private void Update()
    {
        escButton.onClick.AddListener(deleteAnim);
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
                instantiateFrame(itemNum, image[3]);

                if (imageName == "book")
                {
                    instantiateItem(itemNum, image[0]);
                }
                if (imageName == "silverKey")
                {
                    instantiateItem(itemNum, image[1]);
                }
                if (imageName == "goldKey")
                {
                    instantiateItem(itemNum, image[2]);
                }
            }
        }

        itemNum += 1;
    }

    IEnumerator showItemButton()
    {
        yield return new WaitForSeconds(0.5f);
        ItemButton.SetActive(true);
        //itemImage.hideGuide();
    }

    void instantiateItem(int itemNum, Image image)
    {
        item[itemNum] = Instantiate(image, new Vector3(0, -2f, 0), Quaternion.identity);
        item[itemNum].transform.SetParent(frame[itemNum].transform, false);
        item[itemNum].transform.localScale = new Vector3(0.6f, 0.6f, 0.2f);
    }

    void instantiateFrame(int itemNum, Image image)
    {
        frame[itemNum] = Instantiate(image, new Vector3(-400f + (itemNum * 130f), 0, 0), Quaternion.identity);
        frame[itemNum].transform.SetParent(this.transform, false);
        frame[itemNum].transform.localScale = new Vector3(0.3f, 1.4f, 0.2f);
    }
}
