using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float lerpIndex = 0.1f;
    // Start is called before the first frame update
    Vector3 posOffset;
    void Start()
    {
        posOffset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        var pos = Vector3.Lerp(transform.position, target.position+ posOffset, lerpIndex);
        transform.position = pos;
    }
}
