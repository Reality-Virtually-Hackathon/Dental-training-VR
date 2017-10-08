using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FillingTool : MonoBehaviour, Interfaces.IPickable, Interfaces.IUsable {

    [SerializeField]
    private Transform t;
    [SerializeField]
    private Transform hitDist;

    HapticFeedback h;
    Rigidbody b;
    ToothEventEmitter de;

    IEnumerator inst = null;
    IEnumerator inst2 = null;

    void Awake()
    {
        b = GetComponent<Rigidbody>();
    }

    public void OnDrop()
    {
        b.useGravity = true;
        StopCoroutine(inst);
        inst = null;
    }

    public void OnPickup(Transform t, GameObject owner)
    {
        Debug.Log("pickingup");
        b.useGravity = false;

        h = owner.GetComponent<HapticFeedback>();

        inst = RigidbodyPosition(t);

        StartCoroutine(inst);

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

    IEnumerator RaycastFill()
    {

        while (true)
        {
            RaycastHit hit;

            if (Physics.Raycast(t.position, t.forward, out hit, 0.1f))
            {

                Debug.Log(hit.transform.name);


                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("CavityTooth"))
                {
                    CavityToothManager c = hit.transform.gameObject.GetComponent<CavityToothManager>();
                    CalculateHaptic(hit,c);
                }
            }

            yield return null;
        }
    }

    public void OnStopUse()
    {
        StopCoroutine(inst2);
    }

    public void OnUse()
    {
        inst2 = RaycastFill();
        StartCoroutine(inst2);
    }

    void CalculateHaptic(RaycastHit hit, CavityToothManager c)
    {

        if (hit.distance >= (0.0) && hit.distance < (0.02))
        {
            if((t.position - hitDist.position).magnitude > 0.01f)
            {
                if (c.currentStep == Enums.CavityProcedureSteps.FillCavity)
                {
                    c.OnActionCompleted();
                }
            }
        }
    }
}
