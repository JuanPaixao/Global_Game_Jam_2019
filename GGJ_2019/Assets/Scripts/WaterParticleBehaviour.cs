using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterParticleBehaviour : MonoBehaviour
{
    private GrassBehaviour _grassBehaviour;
    public List<ParticleCollisionEvent> collisionEvents;
    private ParticleSystem _particle;
    void Awake()
    {
        _particle = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    private void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = _particle.GetCollisionEvents(other, collisionEvents);
        for (int i = 0; i < numCollisionEvents; i++)
        {
            if (other.gameObject.CompareTag("Grass"))
            {
                _grassBehaviour.GetComponent<GrassBehaviour>();
                _grassBehaviour.isGood=true;
                Debug.Log(other.gameObject.name);
            }
        }
    }
}
