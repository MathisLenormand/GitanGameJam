using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class ControllerTouch : MonoBehaviour
{
    [SerializeField] private Vector3Reference touchPosition;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector3 position = Camera.main.ScreenToWorldPoint(touch.position);

            position.z = 0;

            touchPosition.Value = position;
        }
    }
}
