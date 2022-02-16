using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public GameObject John;

    void Update()
    {
        if (John != null)
        {
        Vector3 position = transform.position; // obtain position
        position.x = John.transform.position.x; // move towards John
        transform.position = position; // update position deppending on the position of John
        }
    }
}
