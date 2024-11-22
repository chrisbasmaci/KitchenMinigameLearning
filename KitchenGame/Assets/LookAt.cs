using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{

    public Transform player;
    private Vector3 _lookat;

    public void Start()
    {
        _lookat = player.forward;
    }

    void Update()
    {
        // transform.position = player.position;
        // transform.forward = _lookat;
    }


}
