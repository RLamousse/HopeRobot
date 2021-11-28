using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform respawnPoint;
    public Transform checkpoint1;
    public Transform checkpoint2;
    public GameObject player;
    public static LevelManager instance; 
    public int nbLives;
    public int nbInitialLives = 5;
    public UnityEngine.Vector3 lastCheckpoint; 

    void Start()
    {
        lastCheckpoint = respawnPoint.position;
        
    }

    private void Awake() {
        instance = this;
        nbLives = nbInitialLives;
    }
    
    public void respawn() {
        nbLives -= 1;
        kill();
        if(nbLives < 1) {
            restart();
        } else {
            Health.instance.LoseHeart(1, nbLives);
            Instantiate(player, lastCheckpoint, Quaternion.identity);
            resetBattery();
            resetHealthBar();
            resetPowerUps();
        }
        Debug.Log("couco");
    }


    void kill() {
        Destroy(GameObject.Find("Character"));
        Destroy(GameObject.Find("Character(Clone)"));
    }

    public void restart() {
        nbLives = nbInitialLives;
        //Health.instance.resetHearts();
        lastCheckpoint = respawnPoint.position;
        Instantiate(player, respawnPoint.position, Quaternion.identity);
        resetBattery();
        resetPowerUps();
        resetCheckpoints();
        resetHealthBar();
        Health.instance.ResetHearts();
        Timer.instance.EndTimer();
        Timer.instance.StartTimer();
        Timer.instance.isStandby = false;
    }
    
    List<GameObject> GetAllChilds(GameObject Go) {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i< Go.transform.childCount; i++)
        {
            list.Add(Go.transform.GetChild(i).gameObject);
        }

        return list;
    }

    void enablePowerUps(List<GameObject> childs) {
        foreach(GameObject objet in childs)
        {
            objet.SetActive(true);
        }
    }
    void resetPowerUps() {
        GameObject shieldPU = GameObject.Find("ShieldPowerUps");
        enablePowerUps(GetAllChilds(shieldPU));
    }

    void resetHealthBar() {
        HealthBar.instance.healthSlider.value = 1;
    }

    void resetCheckpoints() {
        GameObject checkpointsPU = GameObject.Find("Checkpoints");
        enablePowerUps(GetAllChilds(checkpointsPU));
    }

    void resetBattery() {
        if(BatteryProgressBar.instance != null)
        {
            BatteryProgressBar.instance.batterySlider.value = 1f;
            BatteryProgressBar.instance.batteryValue = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
