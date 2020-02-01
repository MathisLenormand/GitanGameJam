using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class Water : MonoBehaviour
{
    [SerializeField] private FloatReference _waterLevel;
    [SerializeField] private FloatReference _waterHeight;
    [SerializeField] private FloatReference _waterHeightRelative;

    [SerializeField] private FloatReference _screenWidth;
    [SerializeField] private FloatReference _screenHeight;

    private void Update()
    {
        _waterHeight.Value = _waterLevel.Value * _screenHeight.Value;
        _waterHeightRelative.Value = _waterHeight.Value - _screenHeight.Value / 2;

        transform.position =  new Vector3(0, _waterHeight.Value - _screenHeight.Value, 0);
    }
}
