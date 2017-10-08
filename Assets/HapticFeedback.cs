using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HapticFeedback : MonoBehaviour {

    SteamVR_TrackedController controller;
    SteamVR_TrackedObject trackedObject;

    public bool shouldStopHaptic { get; set; }
    SteamVR_Controller.Device device = null;

    public float hapticScaleFactor = 1.0f;

    private bool _startHap = false;
    public bool startHaptic { get { return _startHap; } set
        {
            _startHap = value;
            if (device != null)
            {
                invokeFcn();
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
            device.TriggerHapticPulse((ushort)(hapticScaleFactor * 300.0f));

        if (shouldStopHaptic)
        {
            CancelInvoke();
            shouldStopHaptic = false;
            device = null;
        }
           
    }

    void invokeFcn()
    {
        InvokeRepeating("LaunchHaptic", 0.0f, 0.01f);
        _startHap = false;
    }

    void HapticFeedbackProvider(object sender, ClickedEventArgs e)
    {
        var index = trackedObject.index;
        device = SteamVR_Controller.Input((int)trackedObject.index);
        if (_startHap)
        {
            invokeFcn();
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
