using UnityEngine;
using TMPro;

public class MessageManagement : MonoBehaviour {

    #region Singleton
    public static MessageManagement instance;

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

    public GameObject messageBox;
    TextMeshProUGUI messageText;

    public Color errorColor, messageColor;

    int displayDuration = 2;
    float currentDisplay = 0f;
	
	void Start () {
        messageText= messageBox.GetComponent<TextMeshProUGUI>();
        messageBox.SetActive(false);
	}

	void Update () {
		if (messageBox.activeSelf)
        {
            currentDisplay += Time.deltaTime;

            if (currentDisplay > displayDuration)
            {
                currentDisplay = 0f;
                messageBox.SetActive(false);
            }
        }
	}

    public void SetErrorMessage(string message)
    {
        messageText.color = errorColor;
        currentDisplay = 0f;
        messageText.text = message;
        messageBox.SetActive(true);
    }

    public void SetMessage(string message)
    {
        messageText.color = messageColor;
        currentDisplay = 0f;
        messageText.text = message;
        messageBox.SetActive(true);
    }
}
