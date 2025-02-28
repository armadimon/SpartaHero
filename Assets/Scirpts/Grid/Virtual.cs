using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virtual : MonoBehaviour
{
    private CinemachineVirtualCamera cam;
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();

        cam.m_Follow = FindAnyObjectByType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
