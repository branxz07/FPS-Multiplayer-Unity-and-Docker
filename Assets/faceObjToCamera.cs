using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faceObjToCamera : MonoBehaviour
{

    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
