using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;
using System;

public class RepairBoy : MobileObjects
{
    [Header("Controls")]
    [SerializeField] private Vector3GameEvent _swipeEvent;

    [SerializeField] private bool normalizeSwipe;

    [Header("Collision")]
    [SerializeField] private LayerMask bordersLayer;

    [Header("States")]
    [SerializeField] private PlayerStateParameters _normalState;
    [SerializeField] private PlayerStateParameters _stuckState;
    [SerializeField] private PlayerStateParameters _deathState;
    private PlayerStateParameters currentState;

    private Action doAction;

    [Header("Matter")]
    [SerializeField] private float START_MATTER = 3f;
    [SerializeField] private float MAX_MATTER = 5f;
    [SerializeField] private float MIN_MATTER = 1f;
    [SerializeField] private AnimationCurve matterScaleCurve;
    private float deltaMatter = 0;

    [Header("Danger Feedback")]
    [SerializeField] private AnimationCurve dangerCurve;
    [SerializeField] private float START_DANGER_MATTER = 2f;
    private float deltaDangerMatter = 0;

    private Material bodyMaterial;

    [Header("Dash")]
    [SerializeField] private float dashNumbers = 2;
    private float currentDashNumber = 0;

    [Header("Asphyxie")]
    [SerializeField] private float timeBeforeAsphyxie = 7f;
    private float currentTimeBeforeAsphyxie = 0;

    [Header("Stuck State Parameters")]
    [SerializeField] private float timeBeforeRelease = 2f;
    private float currentTimeBeforeRelease = 0f;

    [Header("Game Events")]
    [SerializeField] private GameEvent end;

    private float _currentMatter = 1f;
    public float CurrentMatter { 
        get { return _currentMatter; } 
        set
        {
            /*if (value < MIN_MATTER)
            {
                //Debug.Log("Death");
            }*/

            _currentMatter = Mathf.Clamp(value, MIN_MATTER, MAX_MATTER);

            ScalePlayerDependingOfMatter();

            if (value <= START_DANGER_MATTER)
            {
                SetDangerAmountDependingOfMatter();
            }
        }
    }

    [ContextMenu("DEBUG_Add1Matter")]
    public void DEBUG_AddMatter()
    {
        CurrentMatter ++;
    }

    [ContextMenu("DEBUG_Remove1Matter")]
    public void DEBUG_RemoveMatter()
    {
        CurrentMatter--;
    }

    override protected void Start()
    {
        base.Start();

        deltaMatter = MAX_MATTER - MIN_MATTER;

        deltaDangerMatter = START_DANGER_MATTER - MIN_MATTER;

        CurrentMatter = START_MATTER;

        bodyMaterial = GetComponent<MeshRenderer>().material;

        SetModeVoid();
    }

    #region Mode Void
    public void SetModeVoid ()
    {
        doAction = DoActionVoid;

        currentState = null;

        transform.position = new Vector3(0.6f, -0.2f, 0);
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
        AddForce(currentEnviro.Gravity * Time.deltaTime);

        Move();
    }
    #endregion

    #region Mode Stuck
    public void SetModeStuck()
    {
        doAction = DoActionStuck;

        currentState = _stuckState;

        ResetForce();

        currentDashNumber = 0;

        currentTimeBeforeRelease = 0;
    }

    protected void DoActionStuck()
    {
        if (currentTimeBeforeRelease >= timeBeforeRelease)
            SetModeNormal();

        currentTimeBeforeRelease += Time.deltaTime;
    }
    #endregion

    #region Mode Death
    [ContextMenu("KILL")]
    public void SetModeDeath()
    {
        doAction = DoActionDeath;

        currentState = _deathState;

        ResetForce();

        end.Raise();
    }

    protected void DoActionDeath()
    {
        
    }
    #endregion

    private void ScalePlayerDependingOfMatter ()
    {
        float ratio = (_currentMatter - MIN_MATTER) / deltaMatter;

        float currentScale = matterScaleCurve.Evaluate(ratio);

        transform.localScale = new Vector3(currentScale, currentScale, currentScale);
    }

    private void SetDangerAmountDependingOfMatter ()
    {
        float ratio = 1 - (_currentMatter - MIN_MATTER) / deltaDangerMatter;

        float currentAmount = dangerCurve.Evaluate(ratio);

        SetDangerFeedbackValue(currentAmount);
    }

    private void SetDangerFeedbackValue (float value)
    {
        bodyMaterial.SetFloat("Amount", value);
    }

    override protected void Update()
    {
        base.Update();

        doAction();

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, transform.localScale.x / 2, velocity.normalized, velocity.magnitude, bordersLayer);

        if (hit)
        {
            SetModeStuck();
        }
    }

    public void SwipeReaction (Vector3 swipe)
    {
        if (currentDashNumber >= dashNumbers)
            return;

        Vector3 dash = swipe.normalized * currentEnviro.DashPower;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dash.normalized, transform.localScale.x + dash.magnitude * Time.deltaTime, bordersLayer);

        if (hit)
            return;

        if (currentState == _stuckState)
            SetModeNormal();

        AddForce(swipe.normalized * currentEnviro.DashPower, true);

        currentDashNumber++;

        // Si on veut faire demi tour pour que la taille du swipe ait une importance
        //AddForce(swipe * currentEnviro.DashPower, true);
    }
}
