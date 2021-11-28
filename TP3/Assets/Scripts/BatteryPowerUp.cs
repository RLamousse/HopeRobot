using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPowerUp : MonoBehaviour
{
    public GameObject pickupEffect;

    void OnTriggerEnter (Collider player)
    {
        if (player.CompareTag("Player")) {
            StartCoroutine(ResetPlayerBattery());
        }
    }

    IEnumerator ResetPlayerBattery () 
    {
        GameObject batteryObject = GameObject.Find("Battery");
        BatteryProgressBar batteryScript = batteryObject.GetComponent<BatteryProgressBar>();
        //Debug.Log(battery);
        batteryScript.batteryValue = 1.0f;

        powerUpDisabled(true);
        enableNoPowerUpAnimation();

        yield return new WaitForSeconds(5);

        powerUpDisabled(false);
    }

    void enableNoPowerUpAnimation()
    {
        GameObject effect = Instantiate(pickupEffect, transform.position, transform.rotation);
        Destroy(effect, 5);
    }

    void powerUpDisabled(bool disabled)
    {
        gameObject.GetComponent<MeshRenderer>().enabled = !disabled;
        gameObject.GetComponent<Collider>().enabled = !disabled;
    }
}
