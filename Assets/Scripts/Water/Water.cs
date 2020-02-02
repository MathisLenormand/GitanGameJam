using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;
using System;

public class Water : MonoBehaviour
{
    [SerializeField] private FloatReference _waterLevel;
    [SerializeField] private FloatReference _waterHeight;
    [SerializeField] private FloatReference _waterHeightRelative;

    [SerializeField] private FloatReference _screenWidth;
    [SerializeField] private FloatReference _screenHeight;

    [SerializeField, Range(0f, 1f)] private float defeatWaterLevel = 0.1f;
    [SerializeField] private GameEvent waterLevelTooLow;

    private Action doAction;

    private void Start()
    {
        SetModeVoid();
    }

    #region Mode Void
    public void SetModeVoid()
    {
        doAction = DoActionVoid;
    }

    protected void DoActionVoid()
    {

    }
    #endregion

    #region Mode Normal
    public void SetModeNormal()
    {
        doAction = DoActionNormal;

        _waterLevel.Value = 1;
    }

    protected void DoActionNormal()
    {
        if (_waterLevel.Value <= defeatWaterLevel)
        {
            waterLevelTooLow.Raise();
        }

        _waterHeight.Value = _waterLevel.Value * _screenHeight.Value;
        _waterHeightRelative.Value = _waterHeight.Value - _screenHeight.Value / 2;

        transform.position = new Vector3(0, _waterHeight.Value - _screenHeight.Value, 1);
    }
    #endregion

    private void Update()
    {
        doAction();
    }
}
