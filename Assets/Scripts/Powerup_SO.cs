using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Powerup", menuName = "Powerup")]
public class Powerup_SO : ScriptableObject
{
    public enum powerType { health, immunity, damagingPower, AOE_pulse, movementSpeed}

    public powerType buffType;
    public string buffName;
    public float amount;
    public float duration;
    public Sprite image;
    // TODO Maybe add a color so screen blinks when a buff is picked up

    // Find a way to only display certain variables when a certain enum is selected on the editor

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
