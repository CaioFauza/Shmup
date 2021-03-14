using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SteerableBehaviour, IShooter, IDamageable
{
    GameManager gm;
    Animator animator;
    public GameObject player, bullet, explosion;
    public Transform gun;
    public float shootDelay = 0.5f;
    private float _lastShootTimeStamp = 0.0f;
    public AudioClip shootSFX, explosionSFX;

    private void Start()
    {
        
       animator = GetComponent<Animator>();
       gm = GameManager.GetInstance();
    }
    
    public void Shoot()
    {
        if(Time.time - _lastShootTimeStamp < shootDelay) return;
        AudioManager.PlaySFX(shootSFX);
        _lastShootTimeStamp = Time.time;
        Instantiate(bullet, gun.position, Quaternion.identity, transform);
    }

    public void TakeDamage()
    {
        if(gm.gameState != GameManager.GameState.GAME) return;
        gm.lifes--;
    }

    public void Die() {
        Destroy(gameObject);
    }

    void FixedUpdate()
    {   
        if(gm.gameState != GameManager.GameState.GAME) return;
        
        float yInput = Input.GetAxis("Vertical");
        float xInput = Input.GetAxis("Horizontal");

        if(xInput < 0) xInput = 0;
        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        if(viewportPosition.y < 0 || viewportPosition.y > 1) yInput *= -1;
        
        Thrust(xInput, yInput);
       
        if (yInput != 0 || xInput != 0)
        {
           animator.SetFloat("Velocity", 1.0f);
        }
        else
        {
           animator.SetFloat("Velocity", 0.0f);
        }

        if(Input.GetAxisRaw("Jump") != 0){ Shoot(); }
    }

    void Update()
    {
        if(gm.gameState == GameManager.GameState.START)
        {
            transform.position = new Vector3(-5.87f, -3.25f, 0.0f);
            gm.ChangeState(GameManager.GameState.GAME);
            
        }
        if(Input.GetKeyDown(KeyCode.Escape) && gm.gameState == GameManager.GameState.GAME)
        {
            gm.ChangeState(GameManager.GameState.PAUSE);
        }
        if((gm.points >= 10000 && gm.gameState == GameManager.GameState.GAME) || gm.lifes <= 0)
        {
            transform.position = new Vector3(-5.87f, -3.25f, 0.0f);
            gm.ChangeState(GameManager.GameState.END);
        }
    }    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {   
            Explode();
            Destroy(collision.gameObject);
            TakeDamage();
        }
    }

   void Explode()
   {
       AudioManager.PlaySFX(explosionSFX);
       Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 1);
   }    
}
