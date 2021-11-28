using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMotion : MonoBehaviour
{
    public GameObject projectile;
    public Vector3 velocity = new Vector3(0, 700f, 200f);
    public float elapsedTime = 3;
    public float lifetime = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > lifetime)
        {
            GameObject ball = Instantiate(projectile, transform.position,
                                                     transform.rotation);

            ball.GetComponent<Rigidbody>().AddRelativeForce(velocity);

            elapsedTime = 0;
        }
    }
}
