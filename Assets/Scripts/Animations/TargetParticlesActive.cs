using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetParticlesActive : MonoBehaviour
{
    private GameObject target;

    public void ActiveParticle()
    {
        target = Gather.instance.currentTarget;
        if (Gather.instance.gatherProcess && target != null)
        {
            ParticleSystem particleSystem = target.GetComponentInChildren<ParticleSystem>();
            if (particleSystem != null)
            {
                particleSystem.Play(); // Start the particle system to make it flash

                // Optionally, you can stop the particle system after a short delay to ensure it just blinks.
                StartCoroutine(StopParticleSystem(particleSystem, 0.1f)); // Adjust the delay as needed
            }
        }
    }

    private IEnumerator StopParticleSystem(ParticleSystem particleSystem, float delay)
    {
        yield return new WaitForSeconds(delay);
        particleSystem.Stop();
    }
}