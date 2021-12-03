using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMonster : MonoBehaviour
{

    [SerializeField] public float timeBeforeRespawn = 4f;

    void Start()
    {
        GetComponent<Animation>().CrossFade ("Anim_Idle");
    }

    void OnTriggerEnter (Collider player) 
    {
        
        if (player.CompareTag("Player")) {
            Pickup(player);
        }
    }

    void Pickup (Collider player) 
    {
        RobotController playerControler =  player.GetComponent<RobotController>();

        if(playerControler.isSpeedBoosted && playerControler.isRolling) {

            // faire marcher cette animation
            GetComponent<Animation>().CrossFade ("Anim_Death");
            
            //gameObject.SetActive(false);
            Debug.Log("ici");
        } else {
            LevelManager.instance.respawn();
        }
    }
}
