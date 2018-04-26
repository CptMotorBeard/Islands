using UnityEngine;

[CreateAssetMenu(fileName = "New Tool", menuName = "Inventory/Tool")]
public class Tool : Item {

    public ToolType toolType;
    public int grade;

}

public enum ToolType { NONE, HAMMER, PICKAXE, WOODAXE }