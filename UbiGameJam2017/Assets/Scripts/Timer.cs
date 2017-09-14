using System;
using UnityEngine;
using System.Collections;

public class Timer {
    public float currentTime;

    public float totalTime;

    public float GetTimerPercentage() {
        return this.currentTime / this.totalTime;
    }

    public float GetTime() {
        return (float)Math.Round(this.currentTime, 2);
    }

    public void Start(float time) {
        this.totalTime = time;
        this.currentTime = time;
    }

    public void Tick(float time) {
        this.currentTime -= time;
        if (this.currentTime < 0) {
            this.currentTime = 0;
        }
    }

    public bool IsFinished() {
        return this.currentTime <= 0;
    }
}
