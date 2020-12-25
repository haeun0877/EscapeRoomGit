using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] Camera cam;

    [SerializeField] GameObject go_NomalCrosshair;
    [SerializeField] GameObject go_InteractiveCrosshair;

    RaycastHit hitInfo;

    // Update is called once per frame
    void Update()
    {
        CheckObject();
    }

    void CheckObject()
    {
        Vector3 t_MousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

        //ScreenPpintToRay는 2d상의 마우스 위치를 카메라가 보고있는 3d상의 위치값으로 바꿔줌
        if(Physics.Raycast(cam.ScreenPointToRay(t_MousePos), out hitInfo, 5))
        {
            Contact();
        }
        else
        {
            NotContact();
        }
    }

    void Contact()
    {
        if (hitInfo.transform.CompareTag("key")|| hitInfo.transform.CompareTag("box")|| hitInfo.transform.CompareTag("picture"))
        {
            go_InteractiveCrosshair.SetActive(true);
            go_NomalCrosshair.SetActive(false);
        }
        else
        {
            NotContact();
        }
    }

    void NotContact()
    {
        go_InteractiveCrosshair.SetActive(false);
        go_NomalCrosshair.SetActive(true);
    }
}
