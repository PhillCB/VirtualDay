using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using System.Diagnostics;

public class ActivateRays : MonoBehaviour
{
    public GameObject leftTeleportation;

    public InputActionProperty leftActivate;

    public InputActionProperty leftCancel;
    // Update is called once per frame
    void Update()
    {
        leftTeleportation.SetActive(leftActivate.action.ReadValue<float>() > 0.1f );
    }
}
