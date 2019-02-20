using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public GameObject[] objects;
    public Text counter;

    public void Active(int i)
    {
        objects[i].SetActive(true);
    }
    public void Deactive(int i)
    {
        objects[i].SetActive(false);
    }
    public void IncreaseRockCounter(int i)
    {
        counter.text = i.ToString();
    }
}
