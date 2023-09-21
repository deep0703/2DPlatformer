using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    public ResourceManager manager;
    float time = 0f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Space Station")
        {
            Debug.Log("Space station hit.");
            manager.giveAmmo(25);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Space Station")
        {
            time += Time.deltaTime;
            if (time >= 0.5f)
            {
                Debug.Log("Giving ammo.");
                manager.giveAmmo(3);
                time = 0f;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Space Station")
        {
            Debug.Log("Left space station.");
        }
    }
}
