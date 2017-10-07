using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ViveController
{
    [CustomEditor(typeof(HapticController))]
    public class HapticControllerEditor : ControllerObjectEditor
    {
        public override void OnInspectorGUI()
        {
            HapticController hapticController = (HapticController)target;
            if (!hapticController.dismiss && DependencyCheck(hapticController))
			{
				if (GUILayout.Button("Dismiss"))
					hapticController.dismiss = true;
			}
        }
    }
}
