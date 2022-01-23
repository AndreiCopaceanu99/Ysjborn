 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMovement : MonoBehaviour
{

    float XDir;
    float YDir;
    [SerializeField] float speed;
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] float arrowFrequency;
    [SerializeField] GameObject[] Drops;
    int dropsChance;
    float timeSinceLastArrow;
    Transform EnemyShooting;
    Rigidbody2D rb;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        EnemyShooting = gameObject.transform.Find("EnemyShooting").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeSinceLastArrow += Time.deltaTime;
        if (transform.position.x > 6.3f)
        {
            rb.velocity = transform.right * -speed;
        }
        else
        {
            Vector2 stopPosition = new Vector2(6.3f, transform.position.y);
            transform.position = stopPosition;
            if (timeSinceLastArrow > arrowFrequency)
            {
                Instantiate(arrowPrefab, EnemyShooting.position, EnemyShooting.rotation);
                timeSinceLastArrow = 0;
                FindObjectOfType<AudioManager>().Play("ArrowLaunch");
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (transform.position.x <= 7f)
        {
            if (collision.gameObject.tag == "Arrow")
            {
                Destroy(gameObject);
                Manager.points += 10;
                if (Manager.lives < 3)
                {
                    if ((Manager.points) % 100 == 0)
                    {
                        Manager.lives++;
                    }
                }
                if (Manager.mana < 100)
                {
                    Manager.mana += 5;
                }
                dropsChance = Random.Range(0, 5);
                if (dropsChance == 1)
                {
                    Instantiate(Drops[Random.Range(0, 2)], transform.position, transform.rotation);
                }
            }
        }
    }
}