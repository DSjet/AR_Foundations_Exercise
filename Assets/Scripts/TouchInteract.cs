using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInteract : MonoBehaviour
{
    private Animator _animator;

    private float initialDistance;
    private Vector3 initialScale;

    private float first_point;
    private float last_point;
    private float swipeDistance;

    BlinkingObject blinkScript;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        ScaleObjectByPinch();
        RotateImageBySwipe();

    }

    private void RotateImageBySwipe()
    {
        if (Input.touchCount == 1)
        {
            // Stop Blinking When The User Interacts with Object
            blinkScript.StopBlinking();

            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                _animator.enabled = false;
                first_point = touch.position.x;
                last_point = touch.position.x;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                last_point = touch.position.x;
                swipeDistance = last_point - first_point;
                transform.localRotation = Quaternion.Euler(0, swipeDistance, 0);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                _animator.enabled = true;
            }
        }
    }

    private void ScaleObjectByPinch()
    {
        // detect pinch
        if (Input.touchCount == 2)
        {
            // Stop Blinking When The User Interacts with Object
            blinkScript.StopBlinking();
            if (!_animator.enabled)
            {
                _animator.enabled = true;
            }
            // get the 2 touches
            var touchZero = Input.GetTouch(0);
            var touchOne = Input.GetTouch(1);

            // ignore if one of the touches is cancelled or ended
            if (touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled || touchOne.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled)
            {
                return;
            }

            // calculate the initial distance when the touch began
            if (touchZero.phase == TouchPhase.Began && touchOne.phase == TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                initialScale = transform.localScale;
            }

            // get the distance after the initial touch
            else
            {
                var currentDistance = Vector2.Distance(touchZero.position, touchOne.position);

                // ignore if small changes is occured
                if (Mathf.Approximately(initialDistance, 0))
                {
                    return;
                }

                // scale the object according to the factor
                var factor = currentDistance / initialDistance;
                transform.localScale = initialScale * factor;
            }
        }
    }
}

