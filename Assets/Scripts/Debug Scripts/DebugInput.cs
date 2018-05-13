using UnityEngine;
using UnityEngine.UI;

public class DebugInput : MonoBehaviour {

    public InputField inputField;
    public GameObject input;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            input.SetActive(!input.activeSelf);
            DebugManager.instance.debug = !DebugManager.instance.debug;
            if (input.activeSelf)
                inputField.ActivateInputField();

            inputField.text = "";
        }
            
    }

    public void ParseInput()
    {
        if (!Input.GetKey(KeyCode.Return))
            return;

        string input = inputField.text;
        inputField.text = "";
        inputField.ActivateInputField();

        input = input.ToLower();
        string[] args = input.Split(' ');

        switch (args[0])
        {
            case "additem":
                try
                {
                    int id = int.Parse(args[1]);
                    int quantity = int.Parse(args[2]);
                    Inventory.instance.Add(DebugItems.instance.m_ItemList[id], quantity);
                }
                catch
                {
                    MessageManagement.instance.SetErrorMessage("Invalid Command");
                }
                return;
            case "time":
                try
                {
                    float time = float.Parse(args[1]);
                    DayNightLighting.instance.timeOfDay = time;
                }
                catch
                {
                    MessageManagement.instance.SetErrorMessage("Invalid Command");
                }
                return;
            case "timescale":
                try
                {
                    float scale = float.Parse(args[1]);
                    scale = Mathf.Clamp(scale, 0, 200);
                    DayNightLighting.instance.timeMultiplier = scale;
                }
                catch
                {
                    MessageManagement.instance.SetErrorMessage("Invalid Command");
                }
                return;
            default:
                MessageManagement.instance.SetErrorMessage("Invalid Command");
                return;
        }
    }
}
