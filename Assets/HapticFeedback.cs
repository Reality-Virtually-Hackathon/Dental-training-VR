using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HapticFeedback : MonoBehaviour {

    SteamVR_TrackedController controller;
    SteamVR_TrackedObject trackedObject;

    public bool shouldStopHaptic { get; set; }
    SteamVR_Controller.Device device = null;

    private bool _startHap = false;
    public bool startHaptic { get { return _startHap; } set
        {
            _startHap = value;
            if (device != null)
            {
                InvokeRepeating("LaunchHaptic", 0.0f, 0.01f);
                _startHap = false;
            }
        } }

    //public bool invoked = false;


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
            device = null;
        }
           
    }

    void HapticFeedbackProvider(object sender, ClickedEventArgs e)
    {
        var index = trackedObject.index;
        device = SteamVR_Controller.Input((int)trackedObject.index);
        if (_startHap)
        {
            InvokeRepeating("LaunchHaptic", 0.0f, 0.01f);
            _startHap = false;
        }

        //if(invoked)
        //{
        //    shouldStopHaptic = true;
        //    invoked = false;
        //} else
        //{
        //    invoked = true;
        //}
        
    }
}
