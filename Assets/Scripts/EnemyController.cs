using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D RB; // vill använda rigidbody för att ta hansyn till positioner, kollision blah blah
    public float moveSpeed; // hur snabbt?
    private Transform target; // mål
    public float dmg;

    public float hitWaitTime = 1f;
    private float hitCounter;

    public float health = 5f;

    public float knockbacktime = .5f;
        private float knockBackCounter;
    public int expToGive = 1;
    // Start is called before the first frame update
    void Start()
    {
        //target = FindObjectOfType<PlayerController>().transform;
        target = PlayerHealthController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (knockBackCounter > 0)
        { 
            knockBackCounter -= Time.deltaTime;

            if(moveSpeed > 0)
            {
                moveSpeed = -moveSpeed * 2f;
            }

            if (knockBackCounter < 0)
            {
                moveSpeed = Mathf.Abs(moveSpeed * .5f);
            }
        }

        RB.velocity = (target.position - transform.position).normalized * moveSpeed;

        if(hitCounter > 0f) 
        {
            hitCounter -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && hitCounter <= 0f)
        {
            PlayerHealthController.instance.TakeDmg(dmg);

            hitCounter = hitWaitTime;
        }
    }

    public void TakeDmg(float dmgToTake)
    {
        health -= dmgToTake;

        if(health <= 0) 
        {
            Destroy(gameObject);
            ExperienceLevelController.Instance.spawnExp(transform.position, expToGive);
        }

        DamageNumberController.instance.spawnDmg(dmgToTake, transform.position);
    }

    public void TakeDmg(float dmgToTake, bool shouldKnockBack)
    {
        TakeDmg(dmgToTake);

        if(shouldKnockBack == true)
        {
            knockBackCounter = knockbacktime;
        }
    }
}
