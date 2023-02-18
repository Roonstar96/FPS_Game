using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private void Awake()
    {
        _player = GameObject.Find("Player").transform;
    }
    void Update()
    {
        transform.LookAt(_player);
    }
}
