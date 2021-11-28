using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer instance;

    public Text timeCounter; 
    private TimeSpan timePlaying;
    private bool timerRunning; 
    private float timeRemaining; 
    [SerializeField] public float initialMinutesRemaing = 1.3f; 
    [SerializeField] public float multiplier = 10f;
    public bool isStandby = false;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        timeCounter.text = "Temps restant à l'humanité: 00:00.00";
        timerRunning = false;
        StartTimer(); 
    }

    public void StartTimer() {
        timerRunning = true;
        timeRemaining = initialMinutesRemaing * 60;
        StartCoroutine(UpdateTimer());
    }

    IEnumerator UpdateTimer() {
        while(timeRemaining > 0f && timerRunning) {
            if(!isStandby) timeRemaining -= Time.deltaTime ;
            else timeRemaining -= Time.deltaTime * multiplier;
            timePlaying = TimeSpan.FromSeconds(timeRemaining);
            string timePlayingString = "Temps restant à l'humanité: " + timePlaying.ToString("mm':'ss':'ff");
            timeCounter.text = timePlayingString;

            yield return null;
        }
        //Timer is over 
        GameObject robot = GameObject.Find("robotSphere");
        Destroy(robot);
        LevelManager.instance.restart();
    }

    public void EndTimer() {
        timerRunning = false;
    }

    void ResumeTimer() {
        timerRunning = true;
    }
}
