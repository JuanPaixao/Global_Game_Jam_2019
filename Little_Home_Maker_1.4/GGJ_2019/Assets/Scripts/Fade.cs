using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public GameObject destroyTrigger;
    public void DestroyTrigger()
    {
        destroyTrigger.SetActive(false);
    }
    public void DestroyMe()
    {
        this.gameObject.SetActive(false);
    }
}
