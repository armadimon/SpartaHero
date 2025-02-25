using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustParticleControl : MonoBehaviour
{
    [SerializeField] private bool createDustOnWalk = true;
    [SerializeField] private ParticleSystem dustParticlesSystem;

    public void CreateDustParicles()
    {
        if (createDustOnWalk)
        {
            dustParticlesSystem.Stop();
            dustParticlesSystem.Play();
        }
    }
}
