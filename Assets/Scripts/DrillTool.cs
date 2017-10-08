using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class DrillTool : MonoBehaviour, Interfaces.IPickable, Interfaces.IUsable {

    [SerializeField]
    private Transform drillBit;

    HapticFeedback h;
    AudioSource a;
    Rigidbody b;
    ToothEventEmitter de;

    IEnumerator inst = null;

    void Awake()
    {
        b = GetComponent<Rigidbody>();
        a = GetComponent<AudioSource>();

        a.enabled = false;
    }

    public void OnDrop()
    {
        b.useGravity = true;
        StopCoroutine(inst);
        inst = null;
        de.shouldRun = false;
        de.toolTransformation = null;
        de = null;
    }

    public void OnPickup(Transform t, GameObject owner)
    {
        Debug.Log("pickingup");
        b.useGravity = false;

        h = owner.GetComponent<HapticFeedback>();

        inst = RigidbodyPosition(t);

        StartCoroutine(inst);

        de = owner.GetComponent<ToothEventEmitter>();
        de.toolTransformation = drillBit;
        de.shouldRun = true; // may be just enabled will be sufficient?
    }

    IEnumerator RigidbodyPosition(Transform t)
    {
        while (true)
        {
            b.position = t.position;
            b.rotation = t.rotation;
            yield return null;
        }

    }

    public void OnUse()
    {
        a.enabled = true;
        h.startHaptic = true;
    }

    public void OnStopUse()
    {
        a.enabled = false;
       // h.invoked = false;
        h.shouldStopHaptic = true;
    }
	
}
