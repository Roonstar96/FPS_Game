using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement and camera settings")]
    [SerializeField] private CharacterController _cont;
    [SerializeField] private Transform _playerRotation;
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _sensitivity;

    private float mouseX;
    private float moveX;
    private float moveZ;
    private Vector3 moveChar;

    public static bool _greenCard;
    public static bool _redCard;
    public static bool _blueCard;
    public static bool _blackCard;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        _playerRotation.Rotate(Vector3.up * mouseX);
        moveChar = transform.right * moveX + transform.forward * moveZ;
        _cont.Move(moveChar * _playerSpeed * Time.deltaTime);
    }
}
