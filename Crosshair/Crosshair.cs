using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] Transform tf_Crosshair;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(960, 600, true);
    }

    // Update is called once per frame
    void Update()
    {
        CrosshairMoving();
    }

    void CrosshairMoving()
    {
        tf_Crosshair.localPosition = new Vector2(Input.mousePosition.x - Screen.width, Input.mousePosition.y - Screen.height );

        float t_cursorPosX = tf_Crosshair.localPosition.x;
        float t_cursorPosY = tf_Crosshair.localPosition.y;

        t_cursorPosX = Mathf.Clamp(t_cursorPosX, -Screen.width + 30, -Screen.width +930);
        t_cursorPosY = Mathf.Clamp(t_cursorPosY, -Screen.height + 30, -Screen.height +570);

        tf_Crosshair.localPosition = new Vector2(t_cursorPosX, t_cursorPosY);
    }
}
