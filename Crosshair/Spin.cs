using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] float spinSpeed;
    [SerializeField] Vector3 spinDir; //회전시킬 방향


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(spinDir * spinSpeed * Time.deltaTime);
    }
}
