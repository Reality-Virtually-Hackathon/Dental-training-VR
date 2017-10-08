using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CavityToothManager : MonoBehaviour {

    public Enums.CavityProcedureSteps currentStep;

    [SerializeField]
    GameObject cavityToothStart;
    [SerializeField]
    GameObject healedTooth;
    [SerializeField]
    GameObject filling;
    [SerializeField]
    GameObject drilledTooth;

	// Use this for initialization
	void Awake () {
        currentStep = Enums.CavityProcedureSteps.DrillCavity;
	}

    public void OnActionCompleted()
    {
        switch (currentStep)
        {
            case Enums.CavityProcedureSteps.DrillCavity:

                cavityToothStart.SetActive(false);
                drilledTooth.SetActive(true);
                currentStep = Enums.CavityProcedureSteps.FillCavity;
                Debug.Log("COMPLETED");

                break;
            case Enums.CavityProcedureSteps.FillCavity:

                Debug.Log("TOOTH FILLED");
                filling.SetActive(true);
                currentStep = Enums.CavityProcedureSteps.PackCavity;

                break;
            case Enums.CavityProcedureSteps.PackCavity:

                filling.SetActive(false);
                healedTooth.SetActive(true);
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
