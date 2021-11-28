using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class ProjectileController : MonoBehaviour
 {
    public float elapsedTime = 0;
    public float timeSinceCollision = 5;
    public float fadeOutVelocity = 0.05f;

    public float damage = 0.25f;
    private bool collided = false;

    // Le nombre de vies perdues par collision
    public float collisionDamage;

    void Start() {
        collisionDamage = damage;
    }
    void OnCollisionEnter(Collision collision)
    {
        collided = true;
    }

    void Update()
    {
        if (collided)
        {
            elapsedTime += Time.deltaTime;
            Destroy(gameObject);
        }
    }
}    