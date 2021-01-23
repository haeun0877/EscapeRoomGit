using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cabinetAnim : MonoBehaviour
{
    [SerializeField] Camera cam;
    RaycastHit hitInfo;
    int num;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        num = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CheckObject();
    }

    void CheckObject()
    {
        Vector3 t_MousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

        //ScreenPpintToRay는 2d상의 마우스 위치를 카메라가 보고있는 3d상의 위치값으로 바꿔줌
        if (Physics.Raycast(cam.ScreenPointToRay(t_MousePos), out hitInfo, 5))
        {
            Contact();
        }
        else
        {
            //NotContact();
        }
    }

    void Contact()
    {
        if (hitInfo.transform.gameObject == this.gameObject)
        {
            if (Input.GetMouseButtonDown(0))
                startAnim();
        }
    }

    public void startAnim()
    {
        num += 1;
        Debug.Log(this.gameObject.tag + "open");

        if (num % 2 == 1)
        {
            this.gameObject.GetComponent<Animator>().SetTrigger(this.gameObject.tag+"oepn");
        }
        else
        {
            this.gameObject.GetComponent<Animator>().SetTrigger(this.gameObject.tag+"close");
        }
    }

}
