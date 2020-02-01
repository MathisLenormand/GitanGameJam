using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class Responsive : MonoBehaviour
{
    [SerializeField] private Vector3Reference topLeft;
    [SerializeField] private Vector3Reference bottomRight;

    [SerializeField] private ResponsiveCollliders respCollider;

    // Start is called before the first frame update
    void Awake()
    {
        Vector3 tempTopLeft = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); ;
        tempTopLeft.z = 0;
        topLeft.Value = tempTopLeft;

        Vector3 tempBottomRight = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); ;
        tempBottomRight.z = 0;

        bottomRight.Value = tempBottomRight;

        respCollider.SetColliders();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(topLeft.Value, 0.5f);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(bottomRight.Value, 0.5f);
    }
}
