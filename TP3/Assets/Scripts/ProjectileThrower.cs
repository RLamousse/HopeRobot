using UnityEngine;

public class ProjectileThrower : MonoBehaviour
{
    public GameObject projectile;

    private float elapsedTime;
    public float intervalBetween = 2;
    public float force = 30;

    [Range(-90, 90)]
    public float angle = 45;
    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > intervalBetween)
        {
            ThrowProjectile(force, angle);
            elapsedTime = 0;
        }
    }

    public void ThrowProjectile(float force, float angle)
    {
        GameObject monster = GameObject.Find("StoneMonster");
        monster.GetComponent<Animation>().CrossFade("Anim_Attack");

        float z = Mathf.Cos(angle * Mathf.PI / 180) * force;
        float y = Mathf.Sin(angle * Mathf.PI / 180) * force;
        GameObject ball = Instantiate(projectile, transform.position,
                                                    transform.rotation);
        ball.GetComponent<Rigidbody>().AddRelativeForce(0, y, z);
    }
}
