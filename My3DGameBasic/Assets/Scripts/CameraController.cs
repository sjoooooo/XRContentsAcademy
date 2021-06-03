using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 offset;
    Transform playerPos;
    void Start()
    {
        playerPos = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        this.transform.position = playerPos.transform.TransformPoint(offset);
        this.transform.LookAt(playerPos);
    }
}
