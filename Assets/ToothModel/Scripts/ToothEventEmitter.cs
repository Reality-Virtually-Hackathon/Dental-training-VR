using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class EventWithTransformParameter : UnityEvent<Transform> { }

public class ToothEventEmitter : MonoBehaviour
{

    public Transform toolTransformation;
    public Transform toothToRepairTransformation;

    public bool shouldRun = false;

    public float rayCastDist = 0.005f; // todo - not sure what exactly this value should be? We are not talking in mm, are we ?, all this means is ray cast until 5mm only ? may be we need to increase it?

    [Header("Event when you are .5 mm far from any mesh")]
    public EventWithTransformParameter closeToMesh;

    [Header("Event when you are .5 mm from given tooth")]
    public EventWithTransformParameter closeToToothToBeRepaired;

    [Header("Event when you are .1 mm or less far from any mesh")]
    public EventWithTransformParameter aboutToHitMesh;

    [Header("Event when you are .1 mm far from any mesh")]
    public EventWithTransformParameter aboutToHitTooth;

    [Header("Event when you have touched mesh for first time")]
    public EventWithTransformParameter onEnterMesh;

    [Header("Event when you have touched tooth for first time")]
    public EventWithTransformParameter onEnterToothToBeRepaired;

    [Header("Event when you exit mesh that you entered last")]
    public EventWithTransformParameter onExitMesh;

    [Header("Event when you exit tooth that you entered last")]
    public EventWithTransformParameter onExitToothToBeRepaired;

    [Header("Event when you are inside mesh you entered last, event is fired after tool have moved .1 mm inside the mesh")]
    public EventWithTransformParameter onInsideMesh;

    [Header("Event when you are inside to be repaired tooth, event is fired after tool have moved .1 mm inside the tooth")]
    public EventWithTransformParameter onInsideTooth;

    private Transform lastMeshEntered = null;

    private float factor = 1.0f;

   // private LineRenderer ray = new LineRenderer();

    // Use this for initialization
    void Start()
    {
        factor = rayCastDist / .005f;
    }

    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.startColor = color;
        lr.endColor = color;
        lr.startWidth = .001f;
        lr.endWidth = .001f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }

    // Update is called once per frame // we may need to move this code to fixedUpdate -todo
    void Update()
    {
        if (toolTransformation == null || !shouldRun)
            return;
        RaycastHit hit;
        //Debug.DrawLine(toolTransformation.position, toolTransformation.forward * 5000, Color.green); //  temp / remove - todo after debugging // why doesn't this draw?
        DrawLine(toolTransformation.position, toolTransformation.position + (toolTransformation.forward.normalized* rayCastDist), Color.green);
        if (Physics.Raycast(toolTransformation.position, toolTransformation.forward, out hit, rayCastDist))
        {
            Debug.Log(hit.distance + " " + hit.transform.gameObject.name);
   
            if (lastMeshEntered != null && (hit.distance >= (.01*factor) || hit.transform != lastMeshEntered))
            {
                if (onExitMesh != null)
                {
                    onExitMesh.Invoke(lastMeshEntered);
                }

                if (onExitToothToBeRepaired != null)
                {
                    onExitToothToBeRepaired.Invoke(lastMeshEntered);
                }
                lastMeshEntered = null;

                // do we need to return? probably not? what happens if we exit some tooth & enter other?, we need both event - correct?
            }

            if (hit.distance > (.0045f * factor) && hit.distance < rayCastDist)
            {
                if (closeToMesh != null)
                {
                    closeToMesh.Invoke(hit.transform);
                }

                if (hit.transform == toothToRepairTransformation)
                {
                    if (closeToToothToBeRepaired != null)
                    {
                        closeToToothToBeRepaired.Invoke(hit.transform);
                    }
                }
            }

            if (hit.distance > (0.02 * factor) && hit.distance < (factor*.1))
            {
                if (aboutToHitMesh != null)
                {
                    aboutToHitMesh.Invoke(hit.transform);
                }

                if (hit.transform == toothToRepairTransformation)
                {
                    if (aboutToHitTooth != null)
                    {
                        aboutToHitTooth.Invoke(hit.transform);
                    }
                }
            }

            if (hit.distance >= (0.0 * factor) && hit.distance < (0.01 * factor))
            {
                lastMeshEntered = hit.transform;

                if (onEnterMesh != null)
                {
                    onEnterMesh.Invoke(hit.transform);
                }

                if (hit.transform == toothToRepairTransformation)
                {
                    if (onEnterToothToBeRepaired != null)
                    {
                        onEnterToothToBeRepaired.Invoke(hit.transform);
                    }
                }
            }

            if (lastMeshEntered != null && hit.transform == lastMeshEntered /*&& hit.distance < 0.0*/) // not sure about how to express last condition?
            {
                if (onInsideMesh != null)
                {
                    onInsideMesh.Invoke(hit.transform); // for now gives the transform object but we probably also need how much have we travelled compared to last time? or how much inside the tool is inside tooth ? - todo?
                }

                if (hit.transform == toothToRepairTransformation)
                {
                    if (onInsideTooth != null)
                    {
                        onInsideTooth.Invoke(hit.transform); // for now gives the transform object but we probably also need how much have we travelled compared to last time? or how much inside the tool is inside tooth ? - todo?
                    }
                }
            }
        }
    }
}
