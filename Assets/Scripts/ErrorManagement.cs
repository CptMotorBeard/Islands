using UnityEngine;
using TMPro;

public class ErrorManagement : MonoBehaviour {

    #region Singleton
    public static ErrorManagement instance;

    void Awake()
    {
        if (instance)
        {
            Debug.LogWarning("More than one instance of error management");
            return;
        }

        instance = this;
    }
    #endregion

    public GameObject errorBox;
    TextMeshProUGUI errorMessage;

    int displayDuration = 2;
    float currentDisplay = 0f;
	
	void Start () {
        errorMessage = errorBox.GetComponent<TextMeshProUGUI>();
        errorBox.SetActive(false);
	}

	void Update () {
		if (errorBox.activeSelf)
        {
            currentDisplay += Time.deltaTime;

            if (currentDisplay > displayDuration)
            {
                currentDisplay = 0f;
                errorBox.SetActive(false);
            }
        }
	}

    public void SetErrorMessage(string message)
    {
        currentDisplay = 0f;
        errorMessage.text = message;
        errorBox.SetActive(true);
    }
}
