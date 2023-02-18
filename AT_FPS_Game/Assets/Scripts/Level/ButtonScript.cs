using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    [Header("Button aesthetics")]
    [SerializeField] private MeshRenderer _meshRen; 
    [SerializeField] private Material _offMat;
    [SerializeField] private Material _onMat;

    [SerializeField] private bool _isOn;

    public bool OnBool { get => _isOn; set => _isOn = value; } 

    private void Awake()
    {
        _meshRen.material = _offMat;
        _isOn = false;
    }

    void Update()
    {
        if (_isOn)
        {
            _meshRen.material = _onMat;
        }
    }
}
