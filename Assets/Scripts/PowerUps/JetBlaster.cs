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

    public override void OnPowerStop()
    {
    }

    public override void OnPowerUse()
    {
        rigid.AddForce(rigid.transform.up * thrustForce, ForceMode2D.Impulse);

        if (readyToFire)
        {
            // Fire off bullet
            readyToFire = false;
            GameObject bullet = Instantiate(bulletPrefab, fireSpawnPoint.position, bulletPrefab.transform.rotation);
            // Push in the bullet stats
            bullet.GetComponent<BlasterBullets>().setDirection((fireSpawnPoint.position - powerUpHander.transform.position).normalized);
            // Animation is firing set to true
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
