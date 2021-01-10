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
    int itemNum;
    Sprite[] sprites;

    ObtainImage itemImage;

    void Start()
    {
        anim = GetComponent<Animation>();
        item = new Image[5];
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
                instantiateItem(itemNum, image[3]);
                item[itemNum].transform.localScale = new Vector3(0.3f, 1.5f, 0.2f);
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
        item[itemNum] = Instantiate(image, new Vector3(-400f + (itemNum * 130f), 25f, 0), Quaternion.identity);
        item[itemNum].transform.SetParent(this.transform, false);
        item[itemNum].transform.localScale = new Vector3(0.2f, 0.8f, 0.2f);
    }
}
