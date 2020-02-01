using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTouch : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch finger1 = Input.GetTouch(0);

            Debug.Log(finger1.deltaPosition);
        }
    }
}
