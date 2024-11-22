using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class StableCam : MonoBehaviour
{
    public Transform cam;
    public Player player;
    private Vector3 start;
    private bool isInitialized = false;

    private void Start()
    {
            start = new Vector3(0f, 15f, -5f);
            cam.transform.position = start;
            Debug.Log("Start Position: " + start);
            isInitialized = true;
    }

    void OnEnable()
    {
        // Get the camera's current position
        var playerPos = player.transform.position;
        Vector3 camPosition = start;

        // Add the player's X and Y positions to the camera's current X and Y
        camPosition.x += playerPos.x;
        camPosition.y += playerPos.y;
        camPosition.z += playerPos.z;

        // Update the camera's position
        cam.transform.position = camPosition;
    }
}