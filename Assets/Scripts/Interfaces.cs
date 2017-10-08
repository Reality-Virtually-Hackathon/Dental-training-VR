using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interfaces : MonoBehaviour {

    public interface IPickable
    {
        void OnPickup(Transform t, GameObject owner);
        void OnDrop();
    }

    public interface IUsable
    {
        void OnUse();
        void OnStopUse();
    }
}
