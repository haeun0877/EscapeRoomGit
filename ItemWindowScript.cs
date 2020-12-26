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
    Image item;


    void Start()
    {
        anim = GetComponent<Animation>();
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

        item = Instantiate(book, new Vector3(-400f, -10f, 0), Quaternion.identity);
        item.transform.SetParent(this.transform, false);
        item.transform.localScale = new Vector3(0.2f, 1f, 0.2f);
    }

    IEnumerator showItemButton()
    {
        yield return new WaitForSeconds(0.5f);
        ItemButton.SetActive(true);
    }

}
