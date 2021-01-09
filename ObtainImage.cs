using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObtainImage : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject obtain;

    public void OnPointerClick(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();

        if(eventData.button == PointerEventData.InputButton.Left)
        {
            obtain.SetActive(true);
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
