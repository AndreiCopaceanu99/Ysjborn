using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMoving : MonoBehaviour
{
    public Sprite[] MeleeSprite;
    [SerializeField] float speed;
    [SerializeField] GameObject[] Drops;
    int dropsChance;
    Rigidbody2D rb;
    public int HP;
    float StopTime;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.right * -speed;
        Die();
        StopTime += Time.deltaTime;
        if ((HP == 1)&&(StopTime<=1))
        {
            speed = 0;
            GetComponent<SpriteRenderer>().sprite = MeleeSprite[1];
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = MeleeSprite[0];
            speed = 2;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (transform.position.x <= 7f)
        {
            if (collision.gameObject.tag == "Arrow")
            {
                HP--;
                if ((Manager.lives < 3) && (HP == 0))
                {
                    if (((Manager.points + 10) % 100 == 0) || ((Manager.points + 20) % 100 == 0))
                    {
                        Manager.lives++;
                    }
                }
                if ((HP == 0) && (Manager.mana < 100))
                {
                    Manager.mana += 5;
                }
                StopTime = 0;
            }
            if (collision.gameObject.tag == "Player")
            {
                Destroy(gameObject);
            }
            if (collision.gameObject.tag == "Castle")
            {
                Destroy(gameObject);
                Manager.WallHP -= 5;
            }
        }
    }
    public void Die()
    {
        if (HP == 0)
        {
            Destroy(gameObject);
            Manager.points += 20;
            dropsChance = Random.Range(0, 5);
            if (dropsChance == 1)
            {
                Instantiate(Drops[Random.Range(0, 2)], transform.position, transform.rotation);
            }
        }
    }
}