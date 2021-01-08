using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemWindowScript : MonoBehaviour
{
    private Animation anim;
    [SerializeField] Image image;
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

        if (imageName == "book")
        {
            image.GetComponent<Image>().sprite = sprites[0];
        }
        if (imageName == "silverKey")
        {
            image.GetComponent<Image>().sprite = sprites[2];
        }
        if (imageName == "goldKey")
        {
            image.GetComponent<Image>().sprite = sprites[1];
        }

        if (itemNum <= item.Length)
        {
            if (image != null)
            {
                item[itemNum] = Instantiate(image, new Vector3(-400f + (itemNum * 100f), -10f, 0), Quaternion.identity);
                item[itemNum].transform.SetParent(this.transform, false);
                item[itemNum].transform.localScale = new Vector3(0.2f, 1f, 0.2f);
                item[itemNum].transform.GetComponent<Animation>().Play();
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

}
