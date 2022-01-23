using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    // Use this for initialization
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            Destroy(gameObject);
            if (Manager.lives < 3)
            {
                Manager.lives += 1;
            }
            else
            {
                Manager.points += 20;
            }
        }
    }
}
