using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HapticFeedback : MonoBehaviour {

    public SteamVR_TrackedController controller;
    public SteamVR_TrackedObject trackedObject;

    public bool shouldStopHaptic;
    SteamVR_Controller.Device device;

    public bool toggleHapticWhenTriggerPress = false;

    bool invoked = false;


    // Use this for initialization
    void Start () {
        if(controller == null)
        {
            controller = GetComponent<SteamVR_TrackedController>();
        }
        if (trackedObject == null)
        {
            trackedObject = GetComponent<SteamVR_TrackedObject>();
        }
        shouldStopHaptic = false;
        controller.TriggerClicked += HapticFeedbackProvider;
    }
	
	// Update is called once per frame
	void Update () {
    }

    void LaunchHaptic()
    {
        if(device != null)
            device.TriggerHapticPulse(3000);

        if (shouldStopHaptic)
        {
            CancelInvoke();
            shouldStopHaptic = false;
        }
           
    }

    void HapticFeedbackProvider(object sender, ClickedEventArgs e)
    {
        var index = trackedObject.index;
        device = SteamVR_Controller.Input((int)trackedObject.index);
        InvokeRepeating("LaunchHaptic", 0.0f, 0.01f);
        if(invoked && toggleHapticWhenTriggerPress)
        {
            shouldStopHaptic = true;
            invoked = false;
        } else
        {
            invoked = true;
        }
        
    }
}
