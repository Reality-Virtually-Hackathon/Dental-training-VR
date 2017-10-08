using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToothEventEmitter : MonoBehaviour {

    public Transform tool;
    public Transform toothToRepair;

    [Header("Event when you are .5 mm far from any mesh")]
    public UnityEvent closeToMesh;

    [Header("Event when you are .5 mm from given tooth")]
    public UnityEvent closeToToothToBeRepaired;

    [Header("Event when you are .1 mm or less far from any mesh")]
    public UnityEvent aboutToHitMesh;

    [Header("Event when you are .1 mm far from any mesh")]
    public UnityEvent aboutToHitTooth;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
