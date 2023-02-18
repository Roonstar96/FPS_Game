using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDoor : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private GameObject _door;
    [SerializeField] private GameObject[] _buttons;

    [Header("Door specific variables")]
    [SerializeField] private Transform _closed;
    [SerializeField] private Transform _open;
    [SerializeField] private float _speed;

    private float _timer;
    private bool _allButtonsPressed;

    private void Awake()
    {
        _door.transform.position = _closed.position;
    }

    private void Update()
    {
        ButtonCheck();
    }

    private void ButtonCheck()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            var bb = _buttons[i].GetComponent<ButtonScript>().OnBool;

            if (!bb)
            {
                _allButtonsPressed = false;
                break;
            }
            else
            {
                _allButtonsPressed = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (_allButtonsPressed)
            {
                StartCoroutine(OpenDoor());
            }
            else
            {
                return;
            }
        }
    }

    private IEnumerator OpenDoor()
    {
        _timer = float.Epsilon;
        while (_timer < _speed)
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, _open.position, _timer / _speed);
            _door.transform.position = newPosition;
            _timer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        Destroy(gameObject.GetComponent<ButtonDoor>());
    }

}
