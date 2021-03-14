using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : SteerableBehaviour, IShooter, IDamageable
{
    GameManager gm;
    public GameObject bullet;
    public float shootDelay = 6.5f;
    private float _lastShootTimeStamp = 0.0f;
    private float life = 1.0f;
    public Image healthBar;
    public GameObject hitPoints;

    void Start()
    {
        gm = GameManager.GetInstance();
    }
    
    public void Shoot()
    {   
        if(gm.gameState == GameManager.GameState.START) gm.ChangeState(GameManager.GameState.GAME);
        if(gm.gameState != GameManager.GameState.GAME) return;
        if(Time.time - _lastShootTimeStamp < shootDelay) return;
        _lastShootTimeStamp = Time.time;
        Instantiate(bullet, transform.position - new Vector3(1.0f, 0.0f, 0.0f), Quaternion.identity);
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
