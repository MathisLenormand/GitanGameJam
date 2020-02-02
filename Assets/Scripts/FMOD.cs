using FMODUnity;
using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMOD : MonoBehaviour
{
    [SerializeField] private FloatReference waterLevel;
    [SerializeField] private StudioGlobalParameterTrigger trigger;

    private void Update()
    {
        trigger.value = waterLevel.Value * 100;

        trigger.TriggerParameters();
    }
}
