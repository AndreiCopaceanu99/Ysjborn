using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
    [SerializeField] float arrowSpeed;
    public AudioClip Smash;
    Rigidbody2D rb;
    //private AudioSource source;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        rb.velocity = transform.right * arrowSpeed;
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {


        Destroy(gameObject);
        if ((collision.gameObject.tag == "archer") || (collision.gameObject.tag == "melee") || (collision.gameObject.tag == "Arrow"))
        {
            Explode();
            FindObjectOfType<AudioManager>().Play("ArrowSmash");
            
        }
        if (collision.gameObject.tag == "Castle")
        {
            FindObjectOfType<AudioManager>().Play("ArrowBounceWall");
        }
        if(collision.gameObject.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("ArrowImpactFlesh");
        }
    }
    void Explode()
    {
        var exp = GetComponent<ParticleSystem>();
        exp.Play();
        Destroy(gameObject, exp.duration);
    }
}
