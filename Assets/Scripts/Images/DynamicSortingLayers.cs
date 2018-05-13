using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSortingLayers : MonoBehaviour {

    public bool moveableObject = false;
    public int offset = 0;

	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().sortingOrder = (Mathf.RoundToInt(transform.position.y * 100f) * -1) + offset;
    }
	
	// Update is called once per frame
	void Update () {
		if (moveableObject)
            GetComponent<SpriteRenderer>().sortingOrder = (Mathf.RoundToInt(transform.position.y * 100f) * -1) + offset;
    }
}
