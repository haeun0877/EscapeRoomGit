using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObtainImage : MonoBehaviour, IPointerClickHandler
{

    InteractionController controller;

    public void OnPointerClick(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();

        if(eventData.button == PointerEventData.InputButton.Left)
        {
            controller.obtainvisual();
        }
        if (this.gameObject.name == "frame(Clone)")
        {
            //프레임을 클릭하면 색깔 어둡게
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
