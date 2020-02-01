using ScriptableObjectArchitecture;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] private Vector3Reference topLeft;
    [SerializeField] private Vector3Reference bottomRight;
    [SerializeField] private FloatReference screenWidth;
    [SerializeField] private FloatReference screenHeight;

    [SerializeField] private float _timeBetweenSpawn = 1f;
    private float _elapsedTime = 0f;

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
    [ContextMenu("LaunchLevel")]
    public void SetModeNormal()
    {
        doAction = DoActionNormal;

        _elapsedTime = 0;
    }

    protected void DoActionNormal()
    {
        if (_elapsedTime >= _timeBetweenSpawn)
        {
            _elapsedTime %= _timeBetweenSpawn;

            Debug.Log("Spawn");
        }

        _elapsedTime += Time.deltaTime;
    }
    #endregion

    private void Update()
    {
        doAction();
    }
}
