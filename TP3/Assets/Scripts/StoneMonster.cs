using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMonster : MonoBehaviour
{


    void Start()
    {
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
        Debug.Log(playerControler.isSpeedBoosted);
        Debug.Log(playerControler.isRolling);
        if(playerControler.isSpeedBoosted && playerControler.isRolling) {
            GetComponent<Animation>().CrossFade ("Anim_Death");
            Destroy(gameObject.transform.parent);
        } else {
            LevelManager.instance.respawn();
        }
    }
    
}
