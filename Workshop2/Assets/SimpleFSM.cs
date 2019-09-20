using UnityEngine;
using System.Collections;

public class SimpleFSM : FSM 
{
    public enum FSMState
    {
        None,
        Idle,
        Chase,
        Dead,
        Flee
    }

    //Current state that the NPC is reaching
    public FSMState curState;

    //Speed of the tank
    private float curSpeed;

    //Tank Rotation Speed
    private float curRotSpeed;

    //Whether the NPC is destroyed or not
    private bool bDead;
    private int health;

    private void Awake()
    {
        curState = FSMState.Idle;   
    }

    //Initialize the Finite state machine for the NPC tank
    protected override void Initialize () 
    {
        curSpeed = 10;
        curRotSpeed = 2.0f;
        bDead = false;
        elapsedTime = 0.0f;
        shootRate = 3.0f;
        health = 200;


        //Get the target enemy(Player)
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        playerTransform = objPlayer.transform;

        if(!playerTransform)
            print("Player doesn't exist.. Please add one with Tag named 'Player'");
	}

    //Update each frame
    protected override void FSMUpdate()
    {
        switch (curState)
        {
            case FSMState.Idle: UpdateIdleState(); break;
            case FSMState.Chase: UpdateChaseState(); break;
            case FSMState.Dead: UpdateDeadState(); break;
            case FSMState.Flee: UpdateFleeState(); break;    
        }

        Debug.Log(curState);

        //Update the time
        elapsedTime += Time.deltaTime;
        //Go to dead state is no health left
        if (health <= 0)
            curState = FSMState.Dead;
    }


    /// <summary>
    /// Chase state
    /// </summary>
    protected void UpdateChaseState()
    {
        //Set the target position as the player position
        destPos = playerTransform.position;

        transform.LookAt(playerTransform);
        //Go Forward
        transform.Translate(Vector3.forward * Time.deltaTime * curSpeed);
    }

    protected void UpdateIdleState()
    {
        destPos = playerTransform.position;

        //Check the distance with the player tank
        float dist = Vector3.Distance(transform.position, playerTransform.position);
        if (dist <= 100f)
        {
            curState = FSMState.Chase;
        }
    }

    /// <summary>
    /// Attack state
    /// </summary

    /// <summary>
    /// Dead state
    /// </summary>
    protected void UpdateDeadState()
    {
        //Show the dead animation with some physics effects
        if (!bDead)
        {
            bDead = true;
            Explode();
        }
    }

    protected void UpdateFleeState()
    {
        print("Switch to Flee state - distance:" + Vector3.Distance(transform.position, playerTransform.position));

        //Set the target position as the player position
        destPos = playerTransform.position;
        transform.LookAt(playerTransform);

        float dist = Vector3.Distance(transform.position, playerTransform.position);
        if (dist <= 100.0f)
        {
            curState = FSMState.Flee;
        } else if (dist > 100f) {
            curState = FSMState.Idle;
        }

        //Go Forward
        transform.Translate(-Vector3.forward * Time.deltaTime * curSpeed);
    }

    /// <summary>
    /// Check whether the next random position is the same as current tank position
    /// </summary>
    /// <param name="pos">position to check</param>
    protected bool IsInCurrentRange(Vector3 pos)
    {
        float xPos = Mathf.Abs(pos.x - transform.position.x);
        float zPos = Mathf.Abs(pos.z - transform.position.z);

        if (xPos <= 50 && zPos <= 50)
            return true;

        return false;
    }

    protected void Explode()
    {
        float rndX = Random.Range(10.0f, 30.0f);
        float rndZ = Random.Range(10.0f, 30.0f);
        for (int i = 0; i < 3; i++)
        {
            GetComponent<Rigidbody>().AddExplosionForce(10000.0f, transform.position - new Vector3(rndX, 10.0f, rndZ), 40.0f, 10.0f);
            GetComponent<Rigidbody>().velocity = transform.TransformDirection(new Vector3(rndX, 20.0f, rndZ));
        }

        Destroy(gameObject, 1.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRiggered");
        //Reduce health
        if (other.gameObject.tag == "light")
        {
            curState = FSMState.Flee;
        }
    }

}
