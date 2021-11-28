using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class Projectile:MonoBehaviour
 {
    public float elapsedTime = 0;
    public float timeSinceCollision = 5;
    public float fadeOutVelocity = 0.05f;
    private bool collided = false;

    void OnCollisionEnter(Collision collision)
    {
        collided = true;
    }

    IEnumerator FadeOutProjectile()
    {
        while (gameObject.GetComponent<Renderer>().material.color.a > 0)
        {
            Color color = gameObject.GetComponent<Renderer>().material.color;
            float fadeAmount = color.a - (fadeOutVelocity * Time.deltaTime);
            color = new Color(color.r, color.g, color.b, fadeAmount);
            gameObject.GetComponent<Renderer>().material.color = color;
            yield return null;
        }

        if (gameObject.GetComponent<Renderer>().material.color.a <= 0)
        {
            Destroy(gameObject);
            yield return null;
        }
    }

    void Update()
    {
        if (collided)
        {
            elapsedTime += Time.deltaTime;
            StartCoroutine(FadeOutProjectile());
        }
    }
}    