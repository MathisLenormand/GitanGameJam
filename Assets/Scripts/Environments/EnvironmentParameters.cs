using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Enviro", menuName = "RepairBoy/Enviro", order = 10)]
public class EnvironmentParameters : ScriptableObject
{
    [SerializeField, Range(0f, 1f)] private float _friction = 0.95f;
    public float Friction { get { return _friction; } }

    [SerializeField] private Vector3 _gravity = new Vector3(0, -10, 0);
    public Vector3 Gravity { get { return _gravity; } }

    [SerializeField] private float _dashPower = 10f;
    public float DashPower { get { return _dashPower; } }
}
