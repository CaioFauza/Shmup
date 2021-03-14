using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPatrolState : State
{
    public Transform[] waypoints;  
    SteerableBehaviour steerable;
    IShooter shooter;
    public float shootDelay = 0.5f;
    private float _lastShootTimeStamp = 0.0f;
    
    public override void Awake()
    {
        base.Awake();
        steerable = GetComponent<SteerableBehaviour>();
        shooter = steerable as IShooter;
    }

    public void Start()
    {
        waypoints[0].position = transform.position;
        waypoints[1].position = GameObject.FindWithTag("Player").transform.position;
    }

    public override void Update()
    {
        
        if(Vector3.Distance(transform.position, waypoints[1].position) > .1f) {
            Vector3 direction = waypoints[1].position - transform.position;
            direction.Normalize();
            steerable.Thrust(direction.x, direction.y);
        } else {
            waypoints[1].position = GameObject.FindWithTag("Player").transform.position;
        }

        if(Time.time - _lastShootTimeStamp < shootDelay) return;
        _lastShootTimeStamp = Time.time;
        try
        {
            shooter.Shoot();
        } 
        catch
        {
            return;
        }
    }
}
