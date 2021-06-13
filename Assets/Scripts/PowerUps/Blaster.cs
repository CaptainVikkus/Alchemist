using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : PowerUp
{
    [SerializeField]
    protected GameObject bulletPrefab;
    [SerializeField]
    protected float fireRate;
    protected float fireTime;
    protected bool readyToFire;
    [SerializeField]
    protected Transform fireSpawnPoint;
    [SerializeField]
    protected AudioClip blasterSound;
    public override void OnPowerStop()
    {
        powerUpHander.Player.PlayFiringAnim(false);
        powerUpHander.Player.playerAudio.Stop();
    }

    public override void OnPowerUse()
    {
        if (readyToFire)
        {
            powerUpHander.Player.PlayFiringAnim(true);
            // Fire off bullet
            readyToFire = false;
            GameObject bullet = Instantiate(bulletPrefab, fireSpawnPoint.position, bulletPrefab.transform.rotation);
            // Push in the bullet stats
            bullet.GetComponent<BlasterBullets>().setDirection((fireSpawnPoint.position - powerUpHander.transform.position).normalized);
            // Animation is firing set to true

            // Sound effect
            powerUpHander.Player.playerAudio.loop = false;
            powerUpHander.Player.playerAudio.clip = blasterSound;
            powerUpHander.Player.playerAudio.Play();
        }
    }

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        powerUpType = PowerUps.Blaster;
        possibleCombos.Add(PowerUps.Jetpack, PowerUps.JetBlaster);
        possibleCombos.Add(PowerUps.Phaser, PowerUps.PhaseBlaster);
    }

    // Update is called once per frame
    void Update()
    {
        if (!readyToFire)
        {
            fireTime += Time.deltaTime;
            if (fireTime >= fireRate)
            {
                readyToFire = true;
                fireTime = 0;
            }
        }
    }


}
