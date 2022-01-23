using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potions : MonoBehaviour
{
    // Use this for initialization
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            Destroy(gameObject);
            if (Manager.mana < 100)
            {
                Manager.mana += 20;
            }
            else
            {
                Manager.points += 20;
            }
        }
    }
}
