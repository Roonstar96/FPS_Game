using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum DoorType
{
    green,
    red,
    blue
}
public class KeycardDoor : MonoBehaviour
{
    [SerializeField] private DoorType _doorType;

    [SerializeField] private GameObject _door;
    [SerializeField] private Transform _closed;
    [SerializeField] private Transform _open;
    [SerializeField] private float _speed;
    [SerializeField] private bool _unlocked;

    private float _timer;

    private void Awake()
    {
        _door.transform.position = _closed.position;
        _unlocked = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (_doorType)
            {
                case (DoorType.green):
                    {
                        if (PlayerMovement._greenCard || PlayerMovement._blackCard )
                        {
                            _unlocked = true;
                            StartCoroutine(OpenDoor());
                        }
                        break;
                    }
                case (DoorType.red):
                    {
                        if (PlayerMovement._redCard || PlayerMovement._blackCard)
                        {
                            _unlocked = true;
                            StartCoroutine(OpenDoor());
                        }
                        break;
                    }
                case (DoorType.blue):
                    {
                        if (PlayerMovement._blueCard || PlayerMovement._blackCard)
                        {
                            _unlocked = true;
                            StartCoroutine(OpenDoor());
                        }
                        break;
                    }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (_unlocked)
            {
                StartCoroutine(CloseDoor());
            }
        }
    }

    private IEnumerator OpenDoor()
    {
        _timer = float.Epsilon;
        while (_timer < _speed)
        {
            Vector3 newPosition = Vector3.Lerp(_closed.position, _open.position, _timer / _speed);
            _door.transform.position = newPosition;
            _timer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator CloseDoor()
    {
        _timer = float.Epsilon;
        while (_timer < _speed)
        {
            Vector3 newPosition = Vector3.Lerp(_open.position, _closed.position, _timer / _speed);
            _door.transform.position = newPosition;
            _timer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
}
