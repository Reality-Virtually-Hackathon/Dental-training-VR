using System.Collections;
using UnityEngine;
using UnityEditor;

namespace ViveController
{
    [CustomEditor(typeof(GrabController))]
    public class GrabControllerEditor : ControllerObjectEditor
    {
        public override void OnInspectorGUI()
        {
            GrabController grabController = (GrabController)target;
            if (!grabController.dismiss && DependencyCheck(grabController))
			{
				if (GUILayout.Button("Dismiss"))
					grabController.dismiss = true;
			}
        }
    }
}
