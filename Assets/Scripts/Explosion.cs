using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    public float ExplosionTime;
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Renderer>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        ExplosionTime += Time.deltaTime;
        if (ExplosionTime > 0.5f)
        {
            Destroy(gameObject);
        }
	}
}
