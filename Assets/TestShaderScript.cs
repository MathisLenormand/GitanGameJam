using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShaderScript : MonoBehaviour
{
    public Material organicmat;
    public bool testamount;
    public bool testblend;
    // Start is called before the first frame update
    void Start()
    {
        organicmat = GetComponent<Renderer>().material;

    }

    // Update is called once per frame
    void Update()
    {
        if (testamount == true)
        {
            organicmat.SetFloat("_Amount", 500);
        }
        else
        {
            organicmat.SetFloat("_Amount", 2);
        }

        if (testblend == true)
        {
            organicmat.SetFloat("_blend", 1);
        }
        else
        {
            organicmat.SetFloat("_blend", 0);
        }
    }
}
