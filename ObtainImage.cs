using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObtainImage : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject obtain;
    [SerializeField] GameObject textBar;
    [SerializeField] Text text;

    public void OnPointerClick(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();

        if(eventData.button == PointerEventData.InputButton.Left)
        {
            obtain.SetActive(true);
            textBar.SetActive(true);
            text.text = "아이템을 사용하시겠습니까?";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void hideGuide()
    {
        obtain.SetActive(false);
        textBar.SetActive(false);
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
