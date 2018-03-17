using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatUI : MonoBehaviour {

    public Transform statsParent;
    StatBlockUI[] stats;

    PlayerStatus player;

	// Use this for initialization
	void Start () {
        player = PlayerStatus.instance;

        player.onStatChanged += UpdateStats;
        stats = statsParent.GetComponentsInChildren<StatBlockUI>();

        UpdateStats();        
    }

    public void UpdateStats()
    {
        for (int i = 0; i < stats.Length; i++)
        {
            stats[i].UpdateStat(player.GetStat(i));
        }
    }
}
