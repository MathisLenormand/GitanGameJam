using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileObjects : MonoBehaviour
{
    [Header("Enviro")]
    [SerializeField] protected EnvironmentParameters airEnviro;
    [SerializeField] protected EnvironmentParameters waterEnviro;
    protected EnvironmentParameters currentEnviro;

    [Header("Physics")]
    [SerializeField] protected float mass = 1f;
    protected Vector3 acceleration = new Vector3();
    protected Vector3 velocity = new Vector3();

    protected void Start()
    {
        currentEnviro = waterEnviro;
    }

    protected void Update()
    {
        AddForce(currentEnviro.Gravity);

        velocity += acceleration;

        velocity *= currentEnviro.Friction;

        transform.position += velocity * Time.deltaTime;
    }

    public void AddForce (Vector3 forceToAdd, bool resetForces)
    {
        if (resetForces)
            acceleration = new Vector3();

        acceleration += forceToAdd * Time.deltaTime;
    }
}
