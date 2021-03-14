using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolantBehaviour : SteerableBehaviour, IDamageable
{
    GameManager gm;
    float angle = 0;
    private float life = 1.0f;
    public Image healthBar;
    public GameObject hitPoints;

    void Start()
    {
        gm = GameManager.GetInstance();
    }

    private void FixedUpdate()
    {   
        if(gm.gameState == GameManager.GameState.START) gm.ChangeState(GameManager.GameState.GAME);
        if(gm.gameState != GameManager.GameState.GAME) return;
        angle += 0.1f;
        if (angle > 2.0f * Mathf.PI) angle = 0.0f;
        Thrust(-0.5f, Mathf.Cos(angle));
        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        if(viewportPosition.y < 0 || viewportPosition.y > 1) Die();
    }

    public void TakeDamage()
    {
       gm.points += 500;
       Destroy(Instantiate(hitPoints, transform.position, Quaternion.identity), 0.5f);
       life -= 0.25f;
       healthBar.fillAmount = life;
       if(life <=0) Die();
    }
    
    public void Die()
    {
        Destroy(gameObject);
    }
}
