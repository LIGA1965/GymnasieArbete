using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    public float dmgAmount;

    public float lifeTime, growSpeed = 5f;
    private Vector3 targetSize;

    public bool shouldKnockBack;

    public bool destroyParent;

    public bool dmgOverTime;
    public float timeBetweenDmg;
    private float dmgCounter;

    private List<EnemyController> enemiesInRange = new List<EnemyController>();
    // Start is called before the first frame update
    void Start()
    {
        //Destroy(gameObject, lifeTime);

        targetSize = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);

        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            targetSize = Vector3.zero;

            if (transform.localScale.x == 0f)
            {
                Destroy(gameObject);

                if (destroyParent)
                {
                    Destroy(transform.parent.gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(dmgOverTime == false)
        {
            if (collision.tag == "Enemy")
            {
                collision.GetComponent<EnemyController>().TakeDmg(dmgAmount, shouldKnockBack);
            }
        }
        else
        {
            if(collision.tag == "Enemy")
            {
                enemiesInRange.Add(collision.GetComponent< EnemyController > ());
            }
        }   
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(dmgOverTime == true)
        {
            if(collision.tag == "Enemy")
            {
                enemiesInRange.Remove(collision.GetComponent<EnemyController > ());
            }
        }
    }



}
