using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Transform respawnPoint;
    public GameObject player;
    public static LevelManager instance; 
    public int nbLives;
    public int nbInitialLives = 5;
    public UnityEngine.Vector3 lastCheckpoint; 

    void Start()
    {
        Debug.Log("position initiale respawn point");
        Debug.Log(lastCheckpoint);
        lastCheckpoint = respawnPoint.position;
        
    }

    private void Awake() {
        instance = this;
        nbLives = nbInitialLives;
    }
    
    public void respawn() {
        Debug.Log("position mort");
        Debug.Log(player.transform.position);
        nbLives -= 1;
        if(nbLives < 1) {
            restart();
        } else {
            respawnLastCheckpoint();
        }
    }

    void respawnLastCheckpoint () 
    {
        GameObject robot = GameObject.Find("robotSphere");
        robot.transform.position = lastCheckpoint;
        RobotController.instance.reset();
        Health.instance.LoseHeart(1, nbLives);
        resetBattery();
        resetHealthBar();
        resetPowerUps();
    }


    private void pauseGame() {
        Time.timeScale = 0;
    }

    private void resumeGame() {
        Time.timeScale = 1;
    }

    public void restart() {
        // nbLives = nbInitialLives;
        // //Health.instance.resetHearts();
        // lastCheckpoint = respawnPoint.position;
        // Instantiate(player, respawnPoint.position, Quaternion.identity);
        // resetBattery();
        // resetPowerUps();
        // resetCheckpoints();
        // resetHealthBar();
        // Health.instance.ResetHearts();
        // Timer.instance.EndTimer();
        // Timer.instance.StartTimer();
        // Timer.instance.isStandby = false;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
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
