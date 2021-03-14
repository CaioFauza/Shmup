using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaticEnemyController : SteerableBehaviour, IDamageable
{
    GameManager gm;
    private float life = 1.0f;
    public Image healthBar;
    public GameObject hitPoints;

    void Start()
    {
        gm = GameManager.GetInstance();
    }

    public void TakeDamage()
    {
        gm.points += 500;
        Destroy(Instantiate(hitPoints, transform.position, Quaternion.identity), 0.5f);
        life -= 0.25f;
        healthBar.fillAmount = life;
        if(life <= 0) Die();
    }

    public void Die()
    {   
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Die();
    }
}
