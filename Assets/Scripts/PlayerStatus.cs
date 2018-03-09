using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour {

    #region Singleton
    public static PlayerStatus instance;

    void Awake()
    {        
        if (instance)
        {
            Debug.LogWarning("More than one instance of Player Status");
            return;
        }

        instance = this;
    }

    #endregion

    public delegate void OnStatChanged();
    public OnStatChanged onStatChanged;

    public Image HealthBar;
    public Image EnergyBar;

    public bool isRunning = false;
    public float speed;

    bool isFatigued = false;
    bool isExhausted = false;
    bool isEnergyExhausted = false;

    float runEnergyCost = 20;
    float energyRegen = 8;

    // Variable Stats
    public float Health { get; private set; }
    public float Energy { get; private set; }
    public float Hunger { get; private set; }
    public float Temperature { get; private set; }

    // Main stats
    public int Mind { get; private set; }
    public int Body { get; private set; }

    // Equipment stats
    public Stat warmth;
    public Stat damage;
    public Stat armor;

    void Start()
    {
        Health = 100f;
        Energy = 100f;
        Hunger = 100f;
        Temperature = 100f;

        Mind = 10;
        Body = 10;
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

	void Update () {
        // Decrease stamina if running
        if (isRunning)
        {
            LoseEnergy(runEnergyCost * Time.deltaTime);
        }
        else
        {
            if (isFatigued)
                LoseEnergy(-(energyRegen / 4) * Time.deltaTime);
            else
                LoseEnergy(-energyRegen * Time.deltaTime);
        }

		// decrease hunger
        // inc/dec temperature based on equipment / location
	}

    public void Run()
    {
        if (!isExhausted)
            isRunning = true;
        else
        {
            if (!isEnergyExhausted)
                isRunning = true;
        }
    }

    public float GetSpeed()
    {
        if (isRunning)
            return speed * 1.5f;
        else
            return speed;
    }
	
	public void LoseEnergy(float amount)
    {
        Energy -= amount;
        Energy = Mathf.Clamp(Energy, 0, 100);
        float per = Energy / 100f;

        EnergyBar.rectTransform.localScale = new Vector3(per, 1f, 1f);

        if (Energy <= 0)
            isEnergyExhausted = true;
        if (Energy <= 20 && !isExhausted)
        {
            MessageManagement.instance.SetMessage("You became exhausted");
            isExhausted = true;
            isFatigued = true;
        }
        if (Energy >= 55)
        {
            isExhausted = false;
            isEnergyExhausted = false;
        }
        if (Energy >= 100)
            isFatigued = false;

        if (onStatChanged != null)
            onStatChanged.Invoke();
    }

    public void LoseHealth(float amount)
    {
        Health -= amount;
        Health = Mathf.Clamp(Health, 0, 100);
        float per = Health / 100f;

        HealthBar.rectTransform.localScale = new Vector3(per, 1f, 1f);

        if (onStatChanged != null)
            onStatChanged.Invoke();
    }

    public void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            warmth.AddModifier(newItem.warmthMod);
            damage.AddModifier(newItem.damageMod);
            armor.AddModifier(newItem.armorMod);
        }
        if (oldItem != null)
        {
            warmth.RemoveModifier(oldItem.warmthMod);
            damage.RemoveModifier(oldItem.damageMod);
            armor.RemoveModifier(oldItem.armorMod);
        }

        if (onStatChanged != null)
            onStatChanged.Invoke();
    }

    public int GetStat(int index)
    {
        switch(index)
        {
            case 0:
                return (int)Health;
            case 1:
                return (int)Energy;
            case 2:
                return (int)Hunger;
            case 3:
                return (int)Temperature;
            case 4:
                return Mind;
            case 5:
                return Body;
            case 6:
                return warmth.GetValue();
            case 7:
                return damage.GetValue();
            case 8:
                return armor.GetValue();
            default:
                return -1;
        }
    }
}
