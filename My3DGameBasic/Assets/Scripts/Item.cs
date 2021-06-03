using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
       // if (other.transform.tag == "Player")
        if (other.transform.CompareTag("Player")){
            Destroy(this.gameObject);
            GameManager.itemCount += 1;
            
        }
    }
}
