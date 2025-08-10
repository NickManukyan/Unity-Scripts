using UnityEngine;


public class BulletScript : BaseBulletScript
{
    // READ. Currently we will be using ONLY one pool for bullets. Later on if issues arrise we might need to separate enemy and player bullets

    private float bulletLifeSpan = 1f;
    private int MaxCollitions = 1;
    private int collitions=0;
    private int _totalDamage;

    public override ProjectileType ProjectileType => ProjectileType.PlayerBasicBullet;

    //u can add KNOCKBACK here

    private void OnEnable()
    {
        Invoke(nameof(Deactivate), bulletLifeSpan); // Automatically return after lifetime
    }

    public void SetDamage(int Damage)
    {
        _totalDamage = Damage;
    }

    private void OnTriggerEnter(Collider collision)
    {
        //if (collision.CompareTag("Bullet")) return; // redundand 

        if (collision.CompareTag("Enemy"))
        {
            EnemyBehavior enemy = collision.gameObject.GetComponent<EnemyBehavior>();
            if (enemy != null)
            {
                collitions++;
                if (collitions > MaxCollitions) Deactivate();
                enemy.TakeDmg(Mathf.RoundToInt(_totalDamage));
              
                if (collitions >= MaxCollitions)
                {
                    if (collitions > MaxCollitions) Debug.Log("Bullet got registered twice, collitions: " + collitions);
                    Deactivate();
                }
                
            }
        }
    }
    private void Deactivate()
    {
        collitions = 0;
        CancelInvoke();
        BulletPool.BulletPoolInstance.ReturnBullet(gameObject);
    }
}
