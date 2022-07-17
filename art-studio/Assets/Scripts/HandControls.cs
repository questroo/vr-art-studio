using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandControls : MonoBehaviour
{
    [SerializeField] private InputDeviceCharacteristics inputDeviceCharacteristics;

    [SerializeField] private GameObject handModelPrefab;

    private InputDevice handController;
    private Animator handAnimator;
    private GameObject spawnedHandModel;

    private void Start()
    {
        spawnedHandModel = Instantiate(handModelPrefab, transform);
        handAnimator = spawnedHandModel.GetComponent<Animator>();
    }

    private void Update()
    {
        if (!handController.isValid)
        {
            InitializeControllers();
        }
        else
        {
            Controls();
            UpdateHandAnimation();
        }

    }
    private void InitializeControllers()
    {
        List<InputDevice> devices = new List<InputDevice>();

        InputDeviceCharacteristics controllerCharacteristics = inputDeviceCharacteristics | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            handController = devices[0];
        }
    }
    private void Controls()
    {
        handController.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerButtonValue);
        handController.TryGetFeatureValue(CommonUsages.gripButton, out bool gripButtonValue);

        if (triggerButtonValue == true && gripButtonValue == true)
        {
            Debug.Log($"Fist made with {inputDeviceCharacteristics}.");
        }
    }
    void UpdateHandAnimation()
    {
        if (handController.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }
        if (handController.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }
}