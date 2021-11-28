using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    public GameObject pickupEffect;

    [SerializeField] public float speedMultiplier = 1.5f;
    [SerializeField] public float powerUpDuration = 4f;

    void OnTriggerEnter (Collider player) 
    {
        if (player.CompareTag("Player")) {
            StartCoroutine(Pickup(player));
        }
    }

    IEnumerator Pickup (Collider player) 
    {
        RobotController playerControler =  player.GetComponent<RobotController>();
        playerControler.baseSpeed *= speedMultiplier;
        powerUpDisabled(true, playerControler);
        enableNoPowerUpAnimation();

        yield return new WaitForSeconds(powerUpDuration);
        playerControler.baseSpeed /= speedMultiplier;
        powerUpDisabled(false, playerControler);
    }

    void enableNoPowerUpAnimation() {
        GameObject effect = Instantiate(pickupEffect, transform.position, transform.rotation);
        Destroy(effect, powerUpDuration);
    }

    void powerUpDisabled(bool disabled, RobotController playerControler)
    {
        playerControler.isSpeedBoosted = disabled;
        gameObject.GetComponent<MeshRenderer>().enabled = !disabled;
        gameObject.GetComponent<Collider>().enabled = !disabled;
    }
}
