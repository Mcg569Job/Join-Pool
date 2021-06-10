using UnityEngine;

public class FinishObject : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] FX;

      private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            foreach (ParticleSystem particle in FX)
                particle.Play();
    }
}
