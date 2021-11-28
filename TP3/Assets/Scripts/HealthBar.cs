using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    // Le multiplicateur qui permettra de déterminer le nombre de vies perdues selon la vitesse
    [SerializeField]
    float fallDamageMultiplier = 1;

    // La vitesse minimale de la chute ou le robot commencera à perdre des points de vie
    [SerializeField]
    float minFallDamageSpeed = 10f;
    public Slider healthSlider;

    public static HealthBar instance;
    void Start()
    {
        healthSlider = gameObject.GetComponent<Slider>();
        instance = this;
        healthSlider.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ApplyDamage(Collision coll)
    {
        float damage = 0f;
        if (coll.gameObject.tag == "StoneMonsterProjectile")
        {
            ProjectileController projectile = coll.gameObject.GetComponent<ProjectileController>();
            if (projectile != null)
            {
                damage = (float) coll.gameObject.GetComponent<ProjectileController>().collisionDamage;
            }
        }
        
        if (coll.relativeVelocity.y > minFallDamageSpeed)
        {
            damage = (coll.relativeVelocity.y * fallDamageMultiplier)/100;
            Debug.Log(coll.relativeVelocity.y);
            Debug.Log(fallDamageMultiplier);
            Debug.Log(damage);
        }

        if (RobotController.instance.hasShield && damage > 0)
        {
            RobotController.instance.hasShield = false;
            return;
        }

        healthSlider.value -= damage;
        

        if(healthSlider.value <= 0) {
            LevelManager.instance.respawn();
        }
    }
}
