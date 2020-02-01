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
        AddForce(waterEnviro.Gravity * Time.deltaTime);

        Move();
    }
    #endregion

    #region Mode Stuck
    public void SetModeStuck()
    {
        doAction = DoActionStuck;

        currentState = _stuckState;

        ResetForce();
    }

    protected void DoActionStuck()
    {

    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentState == _normalState && collision.CompareTag("Borders"))
        {
            SetModeStuck();
        }
    }

    protected void Update()
    {
        doAction();
    }

    public void SwipeReaction (Vector3 swipe)
    {
        if (currentState == _stuckState)
            SetModeNormal();

        if (normalizeSwipe)
            AddForce(swipe.normalized * currentEnviro.DashPower, true);
        else
            AddForce(swipe * currentEnviro.DashPower, true);
    }
}
