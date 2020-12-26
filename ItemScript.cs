using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemScript : MonoBehaviour
{
    ItemWindowScript itemWindow;
    Button item;

    private void Start()
    {
        itemWindow = FindObjectOfType<ItemWindowScript>();
        item = this.transform.GetComponent<Button>();
    }

    private void Update()
    {
        item.onClick.AddListener(Click);
    }
    public void Click()
    {
        itemWindow.showAnim();
        this.gameObject.SetActive(false);
    }
}
