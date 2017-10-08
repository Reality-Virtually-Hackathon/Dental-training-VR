using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class DrillTool : MonoBehaviour, Interfaces.IPickable, Interfaces.IUsable {

    [SerializeField]
    Transform hitDist;
    [SerializeField]
    private Transform drillBit;
    [SerializeField]
    private Transform t;

    HapticFeedback h;
    AudioSource a;
    Rigidbody b;
    ToothEventEmitter de;

    IEnumerator inst = null;
    IEnumerator inst2 = null;

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
        h = null;
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
        //de.shouldRun = true; // may be just enabled will be sufficient?
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

    IEnumerator RaycastDrill()
    {

        while (true)
        {

            Debug.Log("DRILLING");
            RaycastHit hit;

            if (Physics.Raycast(t.position, t.forward, out hit, 0.005f))
            {

                Debug.Log(hit.transform.name);

                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("CavityTooth"))
                {

                    float distance = hit.distance;
                    CalculateHaptic(hit);
                    CavityToothManager c = hit.transform.gameObject.GetComponent<CavityToothManager>();

                    if (c.currentStep == Enums.CavityProcedureSteps.DrillCavity)
                    {
                        c.OnActionCompleted();
                    }

                }
            }

            yield return null;
        }
    }

    public void OnUse()
    {
        a.enabled = true;
        h.startHaptic = true;
        inst2 = RaycastDrill();
        StartCoroutine(inst2);
    }

    public void OnStopUse()
    {
        a.enabled = false;
        // h.invoked = false;
        h.shouldStopHaptic = true;
        h.hapticScaleFactor = 1f;
        StopCoroutine(inst2);
    }


    void CalculateHaptic(RaycastHit hit)
    {

        if (hit.distance >= (0.0) && hit.distance < (0.02))
        {

            h.hapticScaleFactor = 2f;
            if((drillBit.position - hitDist.position).magnitude > 0.04f)
            {
                h.hapticScaleFactor = 7f;
            }else if((drillBit.position - hitDist.position).magnitude > 0.01f)
            {
                h.hapticScaleFactor = 10f;
            }
        } else 
            h.hapticScaleFactor = 1f;
    }
}
