using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearBehaviour : MonoBehaviour
{
    public GameManager gameManager;
    public void DestroyBear()
    {
        Destroy(this.gameObject);
    }
    public void DestroyWall()
    {
        gameManager.DestroyWall();
    }
}
