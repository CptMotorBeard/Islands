using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipBehavior : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition + new Vector3(5, 10, 0);
    }
}
