using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ExcavatorTool : MonoBehaviour, Interfaces.IPickable {

    [SerializeField]
    private Transform drillBit;

    Rigidbody b;

    IEnumerator inst = null;

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
}
