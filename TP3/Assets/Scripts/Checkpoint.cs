using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    
    public GameObject pickupEffect;
    private float animationDelay = 3f;
    public Transform correspondingSpawn;

    public LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject managerObject = GameObject.Find("LevelManager");
        levelManager = managerObject.GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
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
        //GameObject LevelManager = GameObject.Find("LevelManager");
        //Component LevelManager = LevelManagerObject.GetComponent<LevelManager>();
        levelManager.lastCheckpoint = correspondingSpawn.position;

        gameObject.SetActive(false);
        enableNoPowerUpAnimation();
    }

    void enableNoPowerUpAnimation() {
        GameObject effect = Instantiate(pickupEffect, transform.position, transform.rotation);
        Destroy(effect, animationDelay);
    }

    // void powerUpDisabled(bool disabled)
    // {
    //     gameObject.GetComponent<MeshRenderer>().enabled = !disabled;
    //     gameObject.GetComponent<Collider>().enabled = !disabled;
    // }
}
