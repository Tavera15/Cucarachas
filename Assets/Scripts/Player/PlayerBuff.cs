using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuff : MonoBehaviour
{
    private PlayerShooting playerShoot;
    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;
    private bool isCurrentlyBuffed = false;
    private float buffCooldown = 0;

    public float additionalBuffDelay = 1f;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerShoot = GetComponent<PlayerShooting>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (buffCooldown <= 0)
            isCurrentlyBuffed = false;

        buffCooldown -= Time.deltaTime;
    }

    // Only trigger when player touches a powerup. Then apply powerup to player.
    private void OnTriggerEnter(Collider other)
    {
        // Only activate powerup if player is currently not buffed
        if(other.tag != "PU" || isCurrentlyBuffed || playerMovement.isPulseEnabled) { return; }

        Powerup_SO powerUp = other.GetComponent<PowerupDisplay>().powerupInfo;

        // Activate the right buff and apply it to player's stats
        switch(powerUp.buffType)
        {
            case Powerup_SO.powerType.health:
                playerHealth.IncreaseHealth(powerUp.amount);
                break;
            case Powerup_SO.powerType.AOE_pulse:
                playerMovement.ActivatePulses(powerUp.amount);
                break;
            case Powerup_SO.powerType.damagingPower:
                playerShoot.buffShootingPower(powerUp.amount, powerUp.duration);
                break;
            case Powerup_SO.powerType.immunity:
                playerHealth.ActivateImmunity(powerUp.duration);
                break;
            case Powerup_SO.powerType.movementSpeed:
                playerMovement.buffMovementSpeed(powerUp.amount, powerUp.duration);
                break;
            default:
                break;
        }

        // Prevent player from stacking buffs
        buffCooldown += (powerUp.duration + additionalBuffDelay);
        isCurrentlyBuffed = true;

        // Change the screen color to "blink"
        playerHealth.FlashScreen(powerUp.blinkColor);
    }
}
