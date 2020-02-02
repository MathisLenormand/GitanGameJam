using FMODUnity;
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
    [SerializeField] private FloatReference waterLevel;

    [SerializeField] private float _timeBetweenSpawn = 1f;
    private float _elapsedTime = 0f;
    [SerializeField] private FloatReference survivalTime;

    [SerializeField] private CollectibleScript collectiblePrefab;
    [SerializeField] private float collectibleOffset = 1f;
    [SerializeField] private CodePourFaireFonctionnerLaBreche breachPrefab;
    [SerializeField] private float breachOffset = 1f;
    [SerializeField] private float breachBottomOffset = 4f;

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

        ClearLevel();

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

        survivalTime.Value = 0;
    }

    protected void DoActionNormal()
    {
        if (_elapsedTime >= _timeBetweenSpawn)
        {
            _elapsedTime %= _timeBetweenSpawn;

            SpawnCollectible();

            SpawnBreach();
        }

        _elapsedTime += Time.deltaTime;

        survivalTime.Value += Time.deltaTime;
    }
    #endregion

    public void SpawnCollectible ()
    {
        CollectibleScript coll = Instantiate(collectiblePrefab, transform);
        coll.transform.position = new Vector3(Random.Range(bottomLeft.Value.x + collectibleOffset, topRight.Value.x - collectibleOffset), Random.Range(bottomLeft.Value.y + collectibleOffset, topRight.Value.y- collectibleOffset));
    }

    public void SpawnBreach ()
    {
        float border = Random.Range(0, 3);

        CodePourFaireFonctionnerLaBreche breach = Instantiate(breachPrefab, transform);

        RuntimeManager.PlayOneShot("event:/SD/SFX/SFX_Wall_Crack", transform.position);

        switch (border)
        {
            case 0:
                breach.transform.position = new Vector3(bottomLeft.Value.x, Random.Range(bottomLeft.Value.y + breachOffset, waterLevel.Value - breachOffset));
                breach.transform.rotation = Quaternion.AngleAxis(0, transform.up);
                breach.ActivateSideParticles();
                break;
            case 1:
                breach.transform.position = new Vector3(topRight.Value.x, Random.Range(bottomLeft.Value.y + breachOffset, waterLevel.Value - breachOffset));
                breach.transform.rotation = Quaternion.AngleAxis(180, transform.up);
                breach.ActivateSideParticles();
                break;
            case 2:
                breach.transform.position = new Vector3(Random.Range(bottomLeft.Value.x + breachBottomOffset, topRight.Value.y - breachBottomOffset), bottomLeft.Value.y);
                breach.transform.rotation = Quaternion.AngleAxis(90, transform.forward);
                breach.ActivateBottomParticles();
                break;
        }
    }

    private void ClearLevel ()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void Update()
    {
        doAction();
    }
}
