using UnityEngine;

public class PlayerLighting : MonoBehaviour {

    public GameObject playerLight;
	
    // Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.F2))
        {
            playerLight.SetActive(!playerLight.activeSelf);
        }
	}
}