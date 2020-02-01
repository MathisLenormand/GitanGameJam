﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class Responsive : MonoBehaviour
{
    [SerializeField] private Vector3Reference topLeft;
    [SerializeField] private Vector3Reference bottomRight;

    // Start is called before the first frame update
    void Start()
    {
        Rect screenRect = Camera.main.pixelRect;

        Vector3 tempTopLeft = Camera.main.ScreenToWorldPoint(new Vector3(screenRect.xMin, screenRect.yMin, 0));
        tempTopLeft.z = 0;
        topLeft.Value = tempTopLeft;

        Vector3 tempBottomRight = Camera.main.ScreenToWorldPoint(new Vector3(screenRect.xMin, screenRect.yMax, 0));
        tempBottomRight.z = 0;

        bottomRight.Value = tempBottomRight;
    }

    []
    private void OnDrawGizmos()
    {
        
    }
}
