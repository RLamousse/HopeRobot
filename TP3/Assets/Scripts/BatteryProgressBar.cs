using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryProgressBar : MonoBehaviour
{
    public static BatteryProgressBar instance;
    
    public Slider batterySlider;
    private float nextActionTime = 0f;
    private float period = 0.1f;
    public float batteryValue = 1f;
    public float walkingDecreaseFactor = 0.003f;
    public float rollingDecreaseFactor = 0.015f;
    public float standbyIncreaseFactor = 0.005f;
    public bool isStandby = false;

    private void Awake() {
        instance = this;
    }

    void Start()
    {
        batterySlider = gameObject.GetComponent<Slider>();
        nextActionTime = Time.time;
    }

    void Update() {
        if (Time.time > nextActionTime) {
            nextActionTime += period;
            manageBattery();
        }
    }


    public void manageBattery() {
        GameObject robot = GameObject.Find("robotSphere");
        RobotController robotController = robot.GetComponent<RobotController>();
        if (batterySlider.value > 0 && batterySlider.value <= 1) {
            if(isStandby) batteryValue += standbyIncreaseFactor;
            else if (robotController.isRolling) batteryValue -= rollingDecreaseFactor;
            else batteryValue -= walkingDecreaseFactor;
            
            batterySlider.value = batteryValue;
        }
        else 
        {
            LevelManager.instance.respawn();
            batterySlider.value = 1.0f;
            batteryValue = 1.0f;
        }
    }
}
