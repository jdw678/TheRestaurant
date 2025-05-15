using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{

    [SerializeField] IDisplayableStorer inventory;
    [SerializeField] string playerName;
    [SerializeField, Range(1, 30)] float movementSpeed = 6;
    [SerializeField, Range(1, 30)] float sprintSpeed = 9;
    [SerializeField, Range(1, 30)] float jumpSpeed = 25;
    [SerializeField, Range(1, 100)] float jumpCooldownInMS = 20;
    [SerializeField, Range(.1f, 100f)] float sensitivity = 25;
    [SerializeField] Camera cam;
    [SerializeField] Rigidbody rb;

    float cameraVerticleRotation = 0f;
    [SerializeField] bool isJumping = false;
    [SerializeField] bool isGrounded;
    private void Awake()
    {
        Inventory inventory;
        TryGetComponent<Inventory>(out inventory);

        if(inventory != null)
            this.inventory = inventory;

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //only check once per update
        bool isSprinting = IsSprinting();
        isGrounded = IsGrounded();

        //movement inputs
        if(Input.GetKey(KeyCode.W))
        {
            float speed = (isSprinting ? sprintSpeed : movementSpeed);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * movementSpeed * Time.deltaTime;
        }

        //jump
        if (Input.GetKey(KeyCode.Space))
        {
            //jump and set isJumping true to avoid multiple jumps
            if (isGrounded && !isJumping)
            {
                isJumping = true;

                rb.AddForce(Vector3.up * jumpSpeed * 10);

                //set is jumping false after a certain amount of time
                Invoke("ResetJump", jumpCooldownInMS / 1000);
            }

        }

        //get mouse movement
        float inputX = Input.GetAxis("Mouse X") * sensitivity / 10;
        float inputY = Input.GetAxis("Mouse Y") * sensitivity / 10;

        //transform the camera up or down
        cameraVerticleRotation -= inputY;
        cameraVerticleRotation = Mathf.Clamp(cameraVerticleRotation, -90f, 90f);
        cam.transform.localEulerAngles = Vector3.right * cameraVerticleRotation;

        //rotate the player left or right
        transform.Rotate(Vector3.up * inputX);
    }

    bool IsGrounded()
    {
        Debug.DrawRay(transform.position + new Vector3(0, .1f, 0), -Vector3.up, Color.red, 20);
        return Physics.Raycast(transform.position + new Vector3(0, .1f, 0), -Vector3.up, .2f);
    }

    bool IsSprinting()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }

    void ResetJump()
    {
        isJumping = false;
    }
}
