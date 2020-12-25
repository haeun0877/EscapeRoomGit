using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWindowScript : MonoBehaviour
{
    private Animation anim;
   

    void Start()
    {
        anim = GetComponent<Animation>();
        
    }

    public void showAnim()
    {
        anim.Play("showWindow");
    }

    public void deleteAnim()
    {
        anim.Play("deleteWindow");
    }
}
