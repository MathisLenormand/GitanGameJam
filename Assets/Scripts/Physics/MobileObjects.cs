using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileObjects : MonoBehaviour
{
    [Header("Enviro")]
    [SerializeField] protected EnvironmentParameters airEnviro;
    [SerializeField] protected EnvironmentParameters waterEnviro;
    protected EnvironmentParameters currentEnviro;

    [SerializeField] private FloatReference waterHeight;

    protected Vector3 acceleration = new Vector3();
    protected Vector3 velocity = new Vector3();

    virtual protected void Start()
    {
        currentEnviro = waterEnviro;
    }

    virtual protected void Update()
    {
        currentEnviro = transform.position.y <= waterHeight.Value ? waterEnviro : airEnviro;
    }

    public void Move ()
    {
        velocity += acceleration;

        velocity *= currentEnviro.Friction;

        transform.position += velocity * Time.deltaTime;
    }

    public void AddForce (Vector3 forceToAdd, bool resetForces = false)
    {
        if (resetForces)
            ResetForce();

        acceleration += forceToAdd * Time.deltaTime;
    }

    public void ResetForce ()
    {
        acceleration = new Vector3();
        velocity = new Vector3();
    }
}
