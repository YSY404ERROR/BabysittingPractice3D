using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCam : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private float LookSensitivity = 0.2f;
    [SerializeField] private float LookAngleLimit = 90f;

    [Header("References")]
    [SerializeField] private Transform orientation;

    private float lookAngle;
    private float yawAngle;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    } 

    private void LateUpdate()
    {
        if (Mouse.current == null) return;

        // getting mouse input
        Vector2 mouseDelta = new Vector2(Mouse.current.delta.x.ReadValue(), Mouse.current.delta.y.ReadValue()); 

        lookAngle -= mouseDelta.y * LookSensitivity;
        yawAngle += mouseDelta.x * LookSensitivity;

        lookAngle = Mathf.Clamp(lookAngle, -LookAngleLimit, LookAngleLimit);

        // rotate cam and orientaton
        transform.rotation = Quaternion.Euler(lookAngle, yawAngle, 0);
        orientation.rotation = Quaternion.Euler(0, yawAngle, 0);
    }
}
