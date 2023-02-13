using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDoor : MonoBehaviour
{
    [SerializeField] private GameObject _door;
    [SerializeField] private Transform _closed;
    [SerializeField] private Transform _open;
    [SerializeField] private float _speed;

    private float _timer;

    private void Awake()
    {
        _door.transform.position = _closed.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(OpenDoor());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(CloseDoor());
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
