using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CavityToothManager : MonoBehaviour {

    Enums.CavityProcedureSteps currentStep;

    [SerializeField]
    GameObject filling;

	// Use this for initialization
	void Awake () {
        currentStep = Enums.CavityProcedureSteps.DrillCavity;
	}

    public void OnActionCompleted()
    {
        switch (currentStep)
        {
            case Enums.CavityProcedureSteps.DrillCavity:

                currentStep = Enums.CavityProcedureSteps.FillCavity;

                break;
            case Enums.CavityProcedureSteps.FillCavity:

                currentStep = Enums.CavityProcedureSteps.PackCavity;

                break;
            case Enums.CavityProcedureSteps.PackCavity:

                currentStep = Enums.CavityProcedureSteps.ScoopWaste;

                break;
            case Enums.CavityProcedureSteps.ScoopWaste:

                currentStep = Enums.CavityProcedureSteps.UVLight;

                break;
            case Enums.CavityProcedureSteps.UVLight:
                break;
        }
    }
	


	// Update is called once per frame
	void Update () {
		
	}
}
