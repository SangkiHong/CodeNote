using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameRate : MonoBehaviour
{
    public Text FPS;
    float fps;
    int frames;
    private float accum;
    private float timeleft; // Left time for current interval
    public float updateInterval = 3.1f;

    void Start()
    {
        if (!FPS)
        {
            Debug.Log("UtilityFramesPerSecond needs a GUIText component!");
            enabled = false;
            return;
        }
        timeleft = updateInterval;
    }

    void Update()
    {
        timeleft -= Time.deltaTime;
        accum += 1f / Time.deltaTime;
        ++frames;

        // Interval ended - update GUI text and start new interval
        if (timeleft <= 0.0)
        {
            // display two fractional digits (f2 format)
            float fps = accum / frames;
            string format = System.String.Format("{0:F2} FPS", fps);
            FPS.text = format;

            timeleft = updateInterval;
            accum = 0.0F;
            frames = 0;
        }
    }
}
