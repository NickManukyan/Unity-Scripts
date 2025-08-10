using System;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] SpriteRenderer _playerSpriteRenderer;
    [SerializeField] GameObject _bullet;
    [SerializeField] float _bulletSpeed = 10f;
    [Range(0f, 1f)]
    [SerializeField] float _fireRate =0.1f;
    private float _nextFire = 0.0F;
    [SerializeField] Transform _shootPoint;
    public bool canShoot = true;


    private void Start()
    {
        //_playerSpriteRenderer= GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (canShoot)
        {
            if (Input.GetMouseButton(0) && Time.time > _nextFire)
            {
                _nextFire = Time.time + _fireRate;
                Shoot();
            }
        }

    }

    void FixedUpdate()
    {
  
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition)-transform.position;
        difference.Normalize();
        float rotZ = Mathf.Atan2(difference.y, difference.x)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f,0f,rotZ);

        if(rotZ < -90 || rotZ > 90)
        {
            if (player.transform.eulerAngles.y == 0)
            {
                transform.localRotation = Quaternion.Euler(180f,0f,-rotZ);
            }
            else if (player.transform.eulerAngles.y == 180)
            {
                transform.localRotation = Quaternion.Euler(180,180,-rotZ);
            }
        }


    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(_bullet, _shootPoint.position,_shootPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * _bulletSpeed  );
    }
}
