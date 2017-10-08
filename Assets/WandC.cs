using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WandC : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }

    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        //DrawLine(transform.position, transform.position + (transform.forward.normalized * 5f), Color.green);
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10f))
        {
            Debug.Log(hit.transform.name);
            if(hit.transform.gameObject.name == "MenuItemMaze" && controller.GetPressUp(triggerButton))
            {
                SceneManager.LoadScene("IntroSteps", LoadSceneMode.Single);
            }else if(hit.transform.gameObject.name == "MainSceneButton" && controller.GetPressUp(triggerButton))
            {
                SceneManager.LoadScene("MikeSceneModified", LoadSceneMode.Single);
            }

            
        }
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
}