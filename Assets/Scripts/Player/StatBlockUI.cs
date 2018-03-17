using TMPro;
using UnityEngine;

public class StatBlockUI : MonoBehaviour {

    public TextMeshProUGUI statText;


    public void UpdateStat(int newValue)
    {
        statText.text = newValue.ToString();
    }
}
