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
    [SerializeField, Range(.1f, 100f)] float sensitivity = 25;
    [SerializeField] Camera camera;
    float cameraVerticleRotation = 0f;

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
        bool sprinting = Input.GetKey(KeyCode.LeftShift);
        //movement inputs
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * (sprinting ? sprintSpeed : movementSpeed) * Time.deltaTime;
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

        //get mouse movement
        float inputX = Input.GetAxis("Mouse X") * sensitivity / 10;
        float inputY = Input.GetAxis("Mouse Y") * sensitivity / 10;

        //transform the camera up or down
        cameraVerticleRotation -= inputY;
        cameraVerticleRotation = Mathf.Clamp(cameraVerticleRotation, -90f, 90f);
        camera.transform.localEulerAngles = Vector3.right * cameraVerticleRotation;

        //rotate the player left or right
        transform.Rotate(Vector3.up * inputX);
    }

}
