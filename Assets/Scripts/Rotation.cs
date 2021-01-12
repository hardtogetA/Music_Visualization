using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    void FixedUpdate()
    {
        GetComponent<RectTransform>().Rotate(0, 0, .15f);
    }
}
