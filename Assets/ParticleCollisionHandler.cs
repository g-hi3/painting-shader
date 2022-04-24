using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisionHandler : MonoBehaviour
{
    private readonly List<ParticleCollisionEvent> _particleCollisions = new();
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (!other.TryGetComponent<Paintable>(out var paintable))
        {
            return;
        }
        
        var collisionCount = _particleSystem.GetCollisionEvents(other, _particleCollisions);

        for (var i = 0; i < collisionCount; i++)
        {
            var collision = _particleCollisions[i];
            var collisionPosition = collision.intersection;
            var paintColor = Color.blue;
            var radius = 1f;
            paintable.Paint(collisionPosition, paintColor, radius);
        }
    }
}
