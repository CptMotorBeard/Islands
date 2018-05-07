using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightLighting : MonoBehaviour
{

    #region Singleton
    public static DayNightLighting instance;

    void Awake()
    {
        if (instance)
        {
            Debug.LogWarning("Another instance of daynight cycle");
            return;
        }

        instance = this;
    }
    #endregion

    public Gradient dayNightColour;
    public float midDay = 1200;

    public Transform lightSource;
    public Transform clock;

    public float timeOfDay;
    public float timeMultiplier = 1;

    float t;

    // Update is called once per frame
    void Update()
    {
        timeOfDay += timeMultiplier * Time.deltaTime;
        if (timeOfDay > 2400)
            timeOfDay = 0;

        float angle = (timeOfDay / 6.66f) - 180f;

        /*      
                Quaternion target = Quaternion.Euler(angle, 0f, 0f);
                lightSource.rotation = target;
        */
        Quaternion target = Quaternion.Euler(0f, 0f, -angle);
        clock.rotation = target;

        t = timeOfDay / midDay;
        if (t > 1)
            t = 1 - ((timeOfDay - midDay) / (2400 - midDay));

        RenderSettings.ambientLight = dayNightColour.Evaluate(t);

    }
}
