using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float camSpeed;
    [SerializeField] float lookLimitY;

    float mouseX;
    float mouseY;

    // Update is called once per frame
    void Update()
    {
        positionMoving();
        rotationMoving();
    }

    void rotationMoving()
    {
        mouseX += Input.GetAxis("Mouse X");
        mouseY += Input.GetAxis("Mouse Y");
        mouseY = Mathf.Clamp(mouseY, -lookLimitY, lookLimitY);

        this.transform.rotation = Quaternion.Euler(new Vector3(-(this.transform.rotation.x + mouseY), this.transform.rotation.y + mouseX, 0) * camSpeed);
    }

    void positionMoving()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirY = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirY;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * moveSpeed;

        this.transform.position = this.transform.position + _velocity * Time.deltaTime;
    }
}
