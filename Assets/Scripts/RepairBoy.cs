using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class RepairBoy : MobileObjects
{
    [Header("Controls")]
    [SerializeField] Vector3Reference touchPosition;
    [SerializeField] Vector3GameEvent swipeEvent;

    public void SwipeReaction (Vector3 swipe)
    {
        Debug.Log(swipe);

        AddForce(swipe.normalized * currentEnviro.DashPower, true);
    }
}
