using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController cont;
    [SerializeField] private Transform playerRotation;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float sensitivity;

    private float mouseX;
    private float moveX;
    private float moveZ;
    private Vector3 moveChar;

    private void Awake()
    {

    }

    private void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        playerRotation.Rotate(Vector3.up * mouseX);

        moveChar = transform.right * moveX + transform.forward * moveZ;

        cont.Move(moveChar * playerSpeed * Time.deltaTime);
    }
}
