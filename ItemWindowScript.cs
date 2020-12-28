using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemWindowScript : MonoBehaviour
{
    private Animation anim;
    [SerializeField] Image book;
    [SerializeField] Button escButton;
    [SerializeField] GameObject ItemButton;
    Image[] item;
    int itemNum;


    void Start()
    {
        anim = GetComponent<Animation>();
        item = new Image[5];
        itemNum = 0;
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
    
    IEnumerator obtainItem()
    {
        yield return new WaitForSeconds(0.5f);

        if (itemNum <= item.Length)
        {
            item[itemNum] = Instantiate(book, new Vector3(-400f + (itemNum * 100f), -10f, 0), Quaternion.identity);
            item[itemNum].transform.SetParent(this.transform, false);
            item[itemNum].transform.localScale = new Vector3(0.2f, 1f, 0.2f);
            item[itemNum].GetComponent<Animation>().Play("spawnImage");
        }

        itemNum += 1;
    }

    IEnumerator showItemButton()
    {
        yield return new WaitForSeconds(0.5f);
        ItemButton.SetActive(true);
    }

}
