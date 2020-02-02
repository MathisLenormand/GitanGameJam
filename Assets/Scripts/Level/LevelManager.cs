using ScriptableObjectArchitecture;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] private Vector3Reference topRight;
    [SerializeField] private Vector3Reference bottomLeft;

    [SerializeField] private float _timeBetweenSpawn = 1f;
    private float _elapsedTime = 0f;

    [SerializeField] private CollectibleScript collectiblePrefab;
    [SerializeField] private float collectibleOffset = 1f;
    [SerializeField] private CodePourFaireFonctionnerLaBreche breachPrefab;

    private Action doAction;

    private void Start()
    {
        SetModeVoid();
    }

    #region Mode Void
    [ContextMenu("ClearLevel")]
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

            SpawnCollectible();
        }

        _elapsedTime += Time.deltaTime;
    }
    #endregion

    public void SpawnCollectible ()
    {
        CollectibleScript coll = Instantiate(collectiblePrefab, transform);
        coll.transform.position = new Vector3(Random.Range(bottomLeft.Value.x + collectibleOffset, topRight.Value.x - collectibleOffset), Random.Range(bottomLeft.Value.y + collectibleOffset, topRight.Value.y- collectibleOffset));
    }


    private void Update()
    {
        doAction();
    }
}
