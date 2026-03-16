using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    // Mostly a Save System, to reset the day if the player hits a fail state

    public event EventHandler<OnTimeIntervalElapsedEventArgs> OnTimeIntervalElapsed;
    //new day event + saving
    //scriptable objects or something to trigger story beat events

    public class OnTimeIntervalElapsedEventArgs : EventArgs
    {
        public float gameTime;
        public float gameTimeHours;
        public float gameTimeMinutes;
    }

    [Tooltip("1 Minute IRL is 1 Hour in game with a standard 24 hour system (24 minutes per in game day)")]
    [SerializeField] private float secondsPerInGameHour = 60; // 1 Minute IRL is 1 Hour in game with a standard 24 hour system by default
    [Tooltip("The interval between in game time progress updates for in game minutes (1 irl second is 1 in game minute)")]
    [SerializeField] private float inGameMinuteDisplayInterval = 15; // The interval between in game time progress updates for in game minutes(1 irl second is 1 in game minute by default)

    private float gameTime;

    public bool FOR_TESTING_gameDayStarted = false; //Temporary Public
    public float FOR_TESTING_gameTimeMultiplier = 1f;

    private void Update()
    {
        if (FOR_TESTING_gameDayStarted)
        {
            Time.timeScale = FOR_TESTING_gameTimeMultiplier;
            gameTime += Time.deltaTime;
            float gameTimeHours = Mathf.Floor(gameTime / secondsPerInGameHour);
            Debug.Log("Game Time: " + Mathf.Floor(gameTime) + " % 15 = " + Mathf.Floor(gameTime % inGameMinuteDisplayInterval));
            if(Mathf.Floor(gameTime % inGameMinuteDisplayInterval) == 0)
            {
                Debug.Log("Total Game Time (In Seconds): " + Mathf.Floor(gameTime));
                Debug.Log("Display Game Time: " + gameTimeHours + ":" + Mathf.Floor(gameTime % secondsPerInGameHour));
                OnTimeIntervalElapsedEventArgs args = new()
                {
                    gameTime = gameTime,
                    gameTimeHours = gameTimeHours,
                    gameTimeMinutes = Mathf.Floor(gameTime % secondsPerInGameHour)
                };
                OnTimeIntervalElapsed?.Invoke(this, args);
            }
        }
    }

    private void OnNewDay()
    {
        //save game
    }

    private void LoadCurrentDay()
    {
        //load game probably should have specific save scripts
    }

    public float GetGameTime()
    {
        return gameTime;
    }

    public float GetGameTimeHours()
    {
        return Mathf.Floor(gameTime / secondsPerInGameHour);
    }

    public float GetGameTimeMinutes()
    {
        return Mathf.Floor(gameTime % secondsPerInGameHour);
    }
}
