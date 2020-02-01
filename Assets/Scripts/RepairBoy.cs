using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;
using System;

public class RepairBoy : MobileObjects
{
    [Header("Controls")]
    [SerializeField] private Vector3Reference _touchPosition;
    [SerializeField] private Vector3GameEvent _swipeEvent;

    [SerializeField] private bool normalizeSwipe;

    [Header("States")]
    [SerializeField] private PlayerStateParameters _normalState;
    [SerializeField] private PlayerStateParameters _stuckState;
    private PlayerStateParameters currentState;

    private Action doAction;

    override protected void Start()
    {
        base.Start();

        SetModeNormal();
    }

    #region Mode Void
    public void SetModeVoid ()
    {
        doAction = DoActionVoid;

        currentState = null;
    }

    protected void DoActionVoid ()
    {

    }
    #endregion

    #region Mode Normal
    public void SetModeNormal()
    {
        doAction = DoActionNormal;

        currentState = _normalState;
    }

    protected void DoActionNormal()
    {
        AddForce(waterEnviro.Gravity);

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        if (screenPosition.x <= 0 || screenPosition.x >= Camera.main.pixelWidth)
        {
            SetModeStuck();
        }

        if (screenPosition.y < 0 || screenPosition.y > Camera.main.pixelHeight)
        {
            SetModeStuck();
        }

        Move();
    }
    #endregion

    #region Mode Stuck
    public void SetModeStuck()
    {
        doAction = DoActionStuck;

        currentState = _stuckState;

        Debug.Log("STUCK");
    }

    protected void DoActionStuck()
    {

    }
    #endregion

    protected void Update()
    {
        doAction();
    }

    public void SwipeReaction (Vector3 swipe)
    {
        if (normalizeSwipe)
            AddForce(swipe.normalized * currentEnviro.DashPower, true);
        else
            AddForce(swipe * currentEnviro.DashPower, true);
    }
}
