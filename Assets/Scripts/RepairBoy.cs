using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class RepairBoy : MonoBehaviour
{
    [SerializeField] Vector3Reference touchPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            transform.position = touchPosition.Value;
        }
    }
}
