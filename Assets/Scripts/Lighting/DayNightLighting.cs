using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightLighting : MonoBehaviour {

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

    public Transform lightSource;

    public float timeOfDay;
    public float timeMultiplier = 1;
	
	// Update is called once per frame
	void Update () {
        timeOfDay += timeMultiplier * Time.deltaTime;
        if (timeOfDay > 2400)
            timeOfDay = 0;

        float angle = (timeOfDay / 6.66f) - 180f;

        Quaternion target = Quaternion.Euler(angle, 0f, 0f);

        lightSource.rotation = target;
	}
}
