using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class HandController : MonoBehaviour {

    [SerializeField]
    private Transform gripPos;

    SteamVR_TrackedController _controller;

    GameObject potentialItem;
    GameObject currentItem;

    Interfaces.IPickable p;

    // Use this for initialization
    void Awake () {
        _controller = GetComponent<SteamVR_TrackedController>();
        _controller.TriggerClicked += HandleTriggerPress;
        _controller.TriggerUnclicked += HandleTriggerRelease;
        _controller.Gripped += HandleGripPress;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (currentItem != null)
            return;

        if(currentItem == null)
            potentialItem = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        potentialItem = null;
    }

    void HandleTriggerRelease(object sender, ClickedEventArgs e)
    {
        if (currentItem != null)
        {
            MonoBehaviour m = currentItem.gameObject.GetComponent<MonoBehaviour>();
            if (m is Interfaces.IUsable)
            {
                Interfaces.IUsable usable = (Interfaces.IUsable)m;
                usable.OnStopUse();
            }
        }
    }

    void HandleTriggerPress(object sender, ClickedEventArgs e)
    {

        if (potentialItem == null && currentItem == null)
            return;

        if(potentialItem != null && currentItem == null)
        {
            MonoBehaviour m = potentialItem.gameObject.GetComponent<MonoBehaviour>();
            if (m is Interfaces.IPickable)
            {
                Interfaces.IPickable pickable = (Interfaces.IPickable)m;
                pickable.OnPickup(gripPos, gameObject);
            }

            currentItem = potentialItem;
        }

        if(currentItem != null)
        {
            MonoBehaviour m = currentItem.gameObject.GetComponent<MonoBehaviour>();
            if (m is Interfaces.IUsable)
            {
                Interfaces.IUsable usable = (Interfaces.IUsable)m;
                usable.OnUse();
            }
        }
    }

    void HandleGripPress(object sender, ClickedEventArgs e)
    {

        if (currentItem == null && potentialItem == null)
            return;

        if(currentItem != null)
        {
            MonoBehaviour m = currentItem.gameObject.GetComponent<MonoBehaviour>();

            if (m is Interfaces.IPickable)
            {
                Interfaces.IPickable pickable = (Interfaces.IPickable)m;
                pickable.OnDrop();
            }

            currentItem = null;
        }
    }
}
