using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetBlaster : PowerUp
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
    protected float thrustForce = 0.5f;
    protected Rigidbody2D rigid;

    [SerializeField]
    protected AudioClip blasterSound;

    public override void OnPowerStop()
    {
        powerUpHander.Player.playerAudio.Stop();
    }

    public override void OnPowerUse()
    {
        rigid.AddForce(rigid.transform.up * thrustForce * Time.deltaTime, ForceMode2D.Impulse);
        powerUpHander.Player.SetIsJumping(false);
        if (readyToFire)
        {
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

    private void Awake()
    {
        powerUpType = PowerUps.JetBlaster;
    }

    public override void LinkPlayerHandler(PowerUpHander powerUpHander)
    {
        base.LinkPlayerHandler(powerUpHander);
        rigid = powerUpHander.GetComponent<Rigidbody2D>();
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
