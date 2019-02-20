using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed, ladderSpeed;
    [SerializeField] private float _jumpForce, _groundDetectorValue, _ladderDetectorValue;
    private Rigidbody2D _rb;
    private RaycastHit2D _hit;
    public bool isGrounded, isMoving, isClimbing, isHanging, isFalling, canMove, isJumping, isWatering, helpedBear;
    public int withWater;
    private float _movHor, _movVer;
    private SpriteRenderer _sprite;
    [SerializeField] private LayerMask _layer, _layerLadder;
    public Transform groundDetector;
    public string direction;
    private Animator _animator;
    public GameManager gameManager;
    public int rock;
    public GameObject[] animals;
    public float distance;
    public GameObject text;
    public bool withLittlerBear;
    void Awake()
    {
        rock = 0;
        _sprite = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        _movHor = Input.GetAxisRaw("Horizontal");
        _movVer = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButton(0))
        {
            if (withWater > 0)
            {
                _animator.SetBool("isWatering", true);
                isWatering = true;
            }
        }

        if (_movHor != 0 && _movVer != 0)
        {
            if (isClimbing)
            {
                _movHor = 0;
            }
        }
        if (isGrounded)
        {
            isJumping = false;
        }


        GroundDetecting();

        if (_movHor < 0)
        {
            _sprite.flipX = false;
            direction = "left";
        }
        else if (_movHor > 0)
        {
            _sprite.flipX = true;
            direction = "right";
        }
        if (!isClimbing)
        {
            isMoving = _movHor != 0 ? true : false;
        }
        else
        {
            isMoving = false;
        }

        _animator.SetBool("isMoving", isMoving);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        isFalling = isGrounded ? false : true;
        _animator.SetBool("isFalling", isFalling);
        _animator.SetBool("isClimbing", isClimbing);
        _animator.SetBool("isHanging", isHanging);

        if (isClimbing && _movVer != 0)
        {
            _animator.SetBool("goingUp", true);
        }
        else
        {
            _animator.SetBool("goingUp", false);
        }


        foreach (var animal in animals)
        {
            distance = Vector2.Distance(this._rb.transform.position, animal.transform.position);
            if (distance < 4f)
            {
                text.SetActive(true);
            }
            else
            {
                text.SetActive(false);
            }
        }
    }
    private void FixedUpdate()
    {
        if (isMoving && !isClimbing && !isHanging)
        {
            _rb.velocity = direction == "left" ? _rb.velocity = new Vector2(-speed, this._rb.velocity.y) :
                                                 _rb.velocity = new Vector2(speed, this._rb.velocity.y);
        }
        _hit = Physics2D.Raycast(this.transform.position, Vector2.up, _ladderDetectorValue, _layerLadder);


        if (_hit.collider != null)
        {
            if (_movVer > 0 && !isClimbing)
            {
                if (!isClimbing)
                {
                    isClimbing = true;
                    isHanging = true;
                    isMoving = false;
                    _rb.velocity = new Vector2(0, 0);
                }
            }
            else if (_hit.collider == null || _movHor != 0)
            {
                _rb.gravityScale = 1;
                isClimbing = false;
                isHanging = false;
            }
        }


        if (isClimbing)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _movVer * ladderSpeed);
            _rb.gravityScale = 0;
        }
    }
    public void Jump()
    {
        _rb.AddForce(Vector2.up * Mathf.Sqrt(_jumpForce * -1 * Physics.gravity.y));
        isJumping = true;
        if (isWatering)
        {
            withWater--;
            _animator.SetBool("isWatering", false);
            isWatering = false;
        }

    }
    private void Movement()
    {
        _rb.velocity = new Vector2(speed, this._rb.velocity.y);
    }
    private void GroundDetecting()
    {
        isGrounded = Physics2D.Raycast(groundDetector.transform.position, Vector2.down, _groundDetectorValue, _layer);
        if (isGrounded)
        {
            isClimbing = false;
            isHanging = false;
        }
    }
    public void Watering()
    {
        withWater--;
        _animator.SetBool("isWatering", false);
        if (withWater == 0)
        {
            _animator.SetBool("isWatering", false);
            isWatering = false;
            gameManager.Deactive(0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            withWater = 4;
            gameManager.Active(0);
        }
        if (other.gameObject.CompareTag("Rock"))
        {
            gameManager.Active(2);
            rock++;
            other.gameObject.SetActive(false);
            gameManager.IncreaseRockCounter(rock);
        }
        if (other.gameObject.CompareTag("Bear"))
        {
            gameManager.Active(3);
            withLittlerBear = true;
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("BearRockTrigger"))
        {
            if (rock >= 25)
            {
                gameManager.BuildRockWall();
                this._rb.transform.position = new Vector2(31.57f, -8.55f);
                Debug.Log("rock");
                gameManager.Deactive(2);
                gameManager.IncreaseRockCounter(rock);
            }
        }
        if (other.gameObject.CompareTag("HugeBear"))
        {
            if (withLittlerBear)
            {
                gameManager.BearMissonComplete();
            }
            helpedBear = true;
        }
        if (other.gameObject.CompareTag("Whale"))
        {
            if (helpedBear)
            {
                gameManager.WhaleMissionComplete();
            }
        }
    }
}
