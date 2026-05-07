using UnityEngine;

public class WaterPumpController : MonoBehaviour
{
    public ParticleSystem waterParticles;

    public void StartWatering()
    {
        if (waterParticles != null)
        {
            waterParticles.Play();  // Avvia il getto d'acqua
        }
    }

    public void StopWatering()
    {
        if (waterParticles != null)
        {
            waterParticles.Stop();  // Ferma il getto d'acqua
        }
    }
}
