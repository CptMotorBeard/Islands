using UnityEngine;

public class DebugManager : MonoBehaviour {
    #region Singleton
    public static DebugManager instance;
    
    void Awake()
    {
        if (instance)
        {
            Debug.LogWarning("Another instance of debug manager");
            return;
        }
        instance = this;
    }

    #endregion

    public bool debug = false;
}
