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

    private float _currentMatter = 1f;
    public float CurrentMatter { 
        get { return _currentMatter; } 
        set
        {
            if (value < MIN_MATTER)
            {
                Debug.Log("Death");
            }

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

        SetModeNormal();

        bodyMaterial = GetComponent<MeshRenderer>().material; 
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
    }

    protected void DoActionStuck()
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
        // ça marche pas ???

        bodyMaterial.SetFloat("Amount", value);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (currentState == _normalState && collision.CompareTag("Borders"))
        {
            SetModeStuck();
        }*/
    }

    override protected void Update()
    {
        base.Update();

        doAction();

        Debug.Log(velocity.magnitude);

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, transform.localScale.x / 2, velocity.normalized, 0.3f, bordersLayer);

        if (hit)
        {
            SetModeStuck();
        }
    }

    public void SwipeReaction (Vector3 swipe)
    {
        if (currentState == _stuckState)
            SetModeNormal();

        AddForce(swipe.normalized * currentEnviro.DashPower, true);

        // Si on veut faire demi tour pour que la taille du swipe ait une importance
        //AddForce(swipe * currentEnviro.DashPower, true);
    }
}
