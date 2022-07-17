using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CheckControllerButtons : MonoBehaviour
{
    private InputDevice rightHandController;
    private InputDevice leftHandController;


    private void Update()
    {
        RightHand();
        LeftHand();
    }

    public void RightHand()
    {
        List<InputDevice> devices = new List<InputDevice>();

        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            rightHandController = devices[0];
        }
        rightHandController.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerButtonValue);
        if (triggerButtonValue == true)
        {
            Debug.Log("Pressing Right Trigger");
        }
        rightHandController.TryGetFeatureValue(CommonUsages.gripButton, out bool gripButtonValue);
        if (gripButtonValue == true)
        {
            Debug.Log("Pressing Right Grip");
        }
    }
    public void LeftHand()
    {
        List<InputDevice> devices = new List<InputDevice>();

        InputDeviceCharacteristics leftControllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            leftHandController = devices[0];
        }
        leftHandController.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerButtonValue);
        if (triggerButtonValue == true)
        {
            Debug.Log("Pressing Left Trigger");
        }
        leftHandController.TryGetFeatureValue(CommonUsages.gripButton, out bool gripButtonValue);
        if (gripButtonValue == true)
        {
            Debug.Log("Pressing Left Grip");
        }
    }
}
