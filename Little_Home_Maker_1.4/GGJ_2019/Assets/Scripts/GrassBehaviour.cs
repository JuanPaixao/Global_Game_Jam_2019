using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassBehaviour : MonoBehaviour
{
    private Animator _animator;
    public bool isGood;
    private Player _player;
    private GameManager _gameManager;
    int count = 0;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            _player = other.gameObject.GetComponent<Player>();
            Debug.Log(_player);

            if (_player.isWatering && this.count == 0)
            {
                this.isGood = true;
                this._animator.SetBool("isGood", true);
                Debug.Log(_player.isWatering);
                _gameManager.IncreaseGrassCounter();
                this.count = 1;
            }
        }
    }
}
