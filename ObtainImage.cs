using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObtainImage : MonoBehaviour, IPointerClickHandler
{
    InteractionController controller;
    string name;
    int spriteNum;

    public void OnPointerClick(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();

        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if (this.gameObject.name == "book(Clone)")
            {
                controller.obtainvisual("책", 0, "book(Clone)");
                this.gameObject.transform.parent.GetComponent<Image>().color = new Color(170 / 255f, 70 / 255f, 8 / 255f);
            }
            if (this.gameObject.name == "goldKey(Clone)")
            {
                controller.obtainvisual("골드키", 1, "goldKey(Clone)");
                this.gameObject.transform.parent.GetComponent<Image>().color = new Color(170 / 255f, 70 / 255f, 8 / 255f);
            }
            if (this.gameObject.name == "silverKey(Clone)")
            {
                controller.obtainvisual("은색키", 3, "silverKey(Clone)");
                this.gameObject.transform.parent.GetComponent<Image>().color = new Color(170 / 255f, 70 / 255f, 8 / 255f);
            }
            if (this.gameObject.name == "plier(Clone)")
            {
                controller.obtainvisual("드라이버", 2, "plier(Clone)");
                this.gameObject.transform.parent.GetComponent<Image>().color = new Color(170 / 255f, 70 / 255f, 8 / 255f);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<InteractionController>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {

        }
        if (Input.GetKeyDown(KeyCode.N))
        {

        }
    }

}
