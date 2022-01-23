using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterMoving : MonoBehaviour {
    float YDir;
    
    [SerializeField] float speed;
    [SerializeField] GameObject arrowPrefab;
    public static float arrowFrequency;
    public Sprite[] BowAnimation;
    Transform spawnPoint;
    Vector2 startingPos;
    float timeSinceLastArrow;
    Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        spawnPoint = gameObject.transform.Find("SpawnPoint").transform;
        transform.position = new Vector2(-8f, 0f);
        arrowFrequency = 1;

    }
	
	// Update is called once per frame
	void Update () {
        YDir = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(0f, YDir*speed);
        timeSinceLastArrow += Time.deltaTime;
        if ((timeSinceLastArrow > arrowFrequency) && (Input.GetKey(KeyCode.Space)))
        {
            Arrow();
            FindObjectOfType<AudioManager>().Play("ArrowLaunch");
        }
        if(timeSinceLastArrow>arrowFrequency/2)
        {
            GetComponent<SpriteRenderer>().sprite = BowAnimation[0];
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = BowAnimation[1];
        }
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Arrow") || (collision.gameObject.tag == "melee"))
        {
            Manager.lives--;
        }
    }
    public void Arrow()
    {
        Instantiate(arrowPrefab, spawnPoint.position, spawnPoint.rotation);
        timeSinceLastArrow = 0f;
    }
}
