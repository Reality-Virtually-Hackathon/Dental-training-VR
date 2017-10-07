using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ViveController
{
    [CustomEditor(typeof(GrabObject))]
    public class GrabObjectEditor : ControllerObjectEditor
    {
        public override void OnInspectorGUI()
        {
            GrabObject grabObject = (GrabObject)target;
            if (!grabObject.dismiss && DependencyCheck(grabObject))
			{
				if (GUILayout.Button("Dismiss"))
					grabObject.dismiss = true;
			}
            grabObject.button = (Button)EditorGUILayout.EnumPopup("Button", grabObject.button);
            grabObject.pickupType = (PickupType)EditorGUILayout.EnumPopup("Pickup Type", grabObject.pickupType);
            if (grabObject.pickupType == PickupType.Custom)
            {
                grabObject.position = EditorGUILayout.Vector3Field("Custom Position", grabObject.position);
                grabObject.rotation = getQ(EditorGUILayout.Vector4Field("Custom Rotation", getV(grabObject.rotation)));
            }
            grabObject.grabEvent = (ControllerEvent)EditorGUILayout.EnumPopup("Grab Event", grabObject.grabEvent);
            grabObject.hideController = EditorGUILayout.ToggleLeft("Hide Controller", grabObject.hideController);
        }

        private Quaternion getQ(Vector4 v)
        {
            return new Quaternion(v.x, v.y, v.z, v.w);
        }

        private Vector4 getV(Quaternion q)
        {
            return new Vector4(q.x, q.y, q.z, q.w);
        }
    }
}
