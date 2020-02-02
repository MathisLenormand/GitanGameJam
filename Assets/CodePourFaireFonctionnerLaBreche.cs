using FMODUnity;
using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodePourFaireFonctionnerLaBreche : MonoBehaviour
{
    [SerializeField] private float mattervalue;
    [SerializeField] private float waterLossPerSeconds = 0.01f;
    [SerializeField] private FloatReference _waterHeight;
    [SerializeField] private FloatReference _waterLevel;

    [Header("Particules")]
    [SerializeField] private GameObject sideParticleSystem;
    [SerializeField] private GameObject bottomParticleSystem;

    private void Update()
    {
        _waterLevel.Value -= waterLossPerSeconds * Time.deltaTime;

        if (transform.position.y > _waterHeight.Value)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            RuntimeManager.PlayOneShot("event:/SD/SFX/SFX_Hit_Crack", transform.position);

            //collision.gameObject.GetComponent<RepairBoy>().matterlevel += mattervalue;
            Destroy(gameObject);

            collision.GetComponent<RepairBoy>().CurrentMatter -= mattervalue;
        }
    }

    public void ActivateSideParticles ()
    {
        sideParticleSystem.SetActive(true);
    }

    public void ActivateBottomParticles ()
    {
        bottomParticleSystem.SetActive(true);
    }
}
