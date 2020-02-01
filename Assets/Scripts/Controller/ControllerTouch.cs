using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class ControllerTouch : MonoBehaviour
{
    [SerializeField] private Vector3Reference touchPosition;
    [SerializeField] Vector3GameEvent swipeEvent;

    [SerializeField] private float minimalSwipeTime = 10f;
    private float currentSwipeTime = 0f;
    private bool isSwiping = false;
    private Vector3 swipeStartPosition = new Vector3();

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector3 position = Camera.main.ScreenToWorldPoint(touch.position);

            position.z = 0;

            touchPosition.Value = position;

            if (touch.phase == TouchPhase.Began)
            {
                isSwiping = true;

                currentSwipeTime = 0;

                swipeStartPosition = position;
            }  

            if (isSwiping)
            {
                currentSwipeTime += Time.deltaTime;

                if (touch.phase == TouchPhase.Ended)
                {
                    isSwiping = false;

                    if (currentSwipeTime < minimalSwipeTime)
                        return;

                    Vector3 swipe = position - swipeStartPosition;

                    swipeEvent.Raise(swipe);
                }
            }
        }
    }
}
