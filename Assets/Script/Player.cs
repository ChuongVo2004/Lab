using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    // Biến bổ sung để lưu trữ tham chiếu đến camera FreeLook
    public Cinemachine.CinemachineFreeLook freeLookCamera;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            // Lấy input từ người dùng
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            // Chuyển đổi input thành di chuyển
            moveDirection = new Vector3(moveHorizontal, 0.0f, moveVertical);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            // Xử lý nhảy
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // Áp dụng trọng lực
        moveDirection.y -= gravity * Time.deltaTime;

        // Di chuyển nhân vật
        controller.Move(moveDirection * Time.deltaTime);

        // Lấy góc nhìn hiện tại của camera và xoay nhân vật theo góc nhìn đó
        RotateToCameraDirection();
    }

    void RotateToCameraDirection()
    {
        // Lấy hướng của camera
        Vector3 cameraForward = freeLookCamera.transform.forward;
        cameraForward.y = 0; // Chỉ lấy hướng trên mặt phẳng ngang
        Quaternion targetRotation = Quaternion.LookRotation(cameraForward);

        // Xoay nhân vật theo hướng của camera
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);
    }
}
