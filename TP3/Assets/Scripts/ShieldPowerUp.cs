using System.Collections;
using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{
    public GameObject pickupEffect;
    public GameObject player;

    private GameObject effect;
    private RobotController robotController;

    private void Start()
    {
        if(player != null)
        {
            robotController = player.GetComponent<RobotController>();
        }
    }

    private void Update()
    {
        // if(robotController != null)
        // {
        //     if (robotController.hasShield)
        //     {
        //         hidePowerUpAnimation();
        //     }
        //     else
        //     {
        //     showPowerUpAnimation();
        //     }
        // }
    }

    void OnTriggerEnter(Collider player)
    {
        if (player.CompareTag("Player"))
        {
            StartCoroutine(PickUpShield(player));
        }
    }

    IEnumerator PickUpShield(Collider player)
    {   
        player.GetComponent<RobotController>().hasShield = true;
        gameObject.SetActive(false);
        yield return null;
    }


    // void hidePowerUpAnimation()
    // {
    //     powerUpDisabled(true);
    //     effect = Instantiate(pickupEffect, transform.position, transform.rotation);
    // }

    // void showPowerUpAnimation()
    // {
    //     powerUpDisabled(false);
    //     Destroy(effect);
    // }

    // void powerUpDisabled(bool disabled)
    // {
    //     gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = !disabled;
    //     gameObject.GetComponent<Collider>().enabled = !disabled;
    // }
}
