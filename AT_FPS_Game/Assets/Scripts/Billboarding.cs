using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarding : MonoBehaviour
{
    private Vector3 camDirection;

    void Update()
    {
        camDirection = Camera.main.transform.forward;
        camDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(camDirection);
    }
}
