using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   public Transform player;
   public Vector3 offset;

    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
        transform.position = player.position + offset;
    }
}
