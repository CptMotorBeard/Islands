using UnityEngine;

public class MoveScale : MonoBehaviour {

    Vector3 startLocation;

    void Start()
    {
        startLocation = this.transform.position;
    }

    public void ChangeScale(float s)
    {
        this.transform.localScale = new Vector3(s, s, s);
    }

    public void ChangePosY(float y)
    {
        this.transform.position = startLocation + new Vector3(0, y);
    }
}
