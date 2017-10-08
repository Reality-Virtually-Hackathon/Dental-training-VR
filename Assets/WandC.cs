using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WandC : MonoBehaviour
{
    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    private GameObject pickup;

    // Use this for initialization
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (controller == null)
        //{
        //    Debug.Log("Controller not initialized");
        //    return;
        //}

        //if (controller.GetPressDown(gripButton) && pickup != null)
        //{
        //    pickup.transform.parent = this.transform;
        //    pickup.GetComponent<Rigidbody>().useGravity = false;
        //}
        //if (controller.GetPressUp(gripButton) && pickup != null)
        //{
        //    pickup.transform.parent = null;
        //    pickup.GetComponent<Rigidbody>().useGravity = true;
        //}
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 5f))
        {
            if(hit.transform.gameObject.name == "MenuBg")
                SceneManager.LoadScene("IntroSteps", LoadSceneMode.Single);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        //pickup = collider.gameObject;
        //var sc = pickup.GetComponent<VRStandardAssets.Menu.MenuButton>().m_SceneToLoad;
        //SceneManager.LoadScene("IntroSteps", LoadSceneMode.Single);
    }

    private void OnTriggerExit(Collider collider)
    {
        pickup = null;
    }
}