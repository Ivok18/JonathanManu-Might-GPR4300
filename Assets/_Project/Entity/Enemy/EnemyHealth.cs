using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Might.Entity.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.transform.CompareTag("PlayerSword"))
            {
                Debug.Log("tg");
            }
        }
    }
}
