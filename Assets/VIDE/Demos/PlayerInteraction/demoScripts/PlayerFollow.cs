using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [SerializeField] private Transform PlayerTransform;
    private Vector3 _cameraOffset;
    [Range(0.01f, 2.0f)]
    [SerializeField] private float SmoothFactor = 1.0f;

    [SerializeField]  private bool LookAtPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        _cameraOffset = transform.position - PlayerTransform.position;
    }

    //Called after Update
    void LateUpdate(){
        Vector3 newPos = PlayerTransform.position + _cameraOffset;
        transform.position = Vector3.Slerp(transform.position,newPos,SmoothFactor);

        if (LookAtPlayer)
            transform.LookAt(PlayerTransform);
    }
}
