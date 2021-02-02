using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObtainImage : MonoBehaviour, IPointerClickHandler
{
    InteractionController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<InteractionController>();
    }

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
                controller.obtainvisual("골드키", 2, "goldKey(Clone)");
                this.gameObject.transform.parent.GetComponent<Image>().color = new Color(170 / 255f, 70 / 255f, 8 / 255f);
            }
            if (this.gameObject.name == "silverKey(Clone)")
            {
                controller.obtainvisual("은색키", 4, "silverKey(Clone)");
                this.gameObject.transform.parent.GetComponent<Image>().color = new Color(170 / 255f, 70 / 255f, 8 / 255f);
            }
            if (this.gameObject.name == "plier(Clone)")
            {
                controller.obtainvisual("드라이버", 3, "plier(Clone)");
                this.gameObject.transform.parent.GetComponent<Image>().color = new Color(170 / 255f, 70 / 255f, 8 / 255f);
            }
            if (this.gameObject.name == "crystal(Clone)")
            {
                controller.obtainvisual("크리스탈", 1, "crystal(Clone)");
                this.gameObject.transform.parent.GetComponent<Image>().color = new Color(170 / 255f, 70 / 255f, 8 / 255f);
            }
        }
    }
}
