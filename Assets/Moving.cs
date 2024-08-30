using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D _rbody;
    private Animator _animator;
    private float _horizontalMovement;
    private bool _onTheGround;
    private float _verticalSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    public float HorizontalMovement
    {
        private set
        {
            if (value != _horizontalMovement)
            {
                _horizontalMovement = value;

                _animator.SetFloat("xSpeed", Mathf.Abs(_horizontalMovement));


                if (_horizontalMovement > 0)
                    FacingRight = _horizontalMovement > 0;
            }
        }
        get => _horizontalMovement;
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        VerticalSpeed = _rbody.velocity.y;

        _horizontalMovement = (Input.GetAxis("Horizontal"));
        Jump();
    }

    private void Awake()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _rbody.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, _rbody.velocity.y);

        float horizontalInput = Input.GetAxis("Horizontal");

        if ((horizontalInput > 0 && !FacingRight) || (horizontalInput < 0 && FacingRight))
        {
            FacingRight = !FacingRight;
        }
    }

    public bool OnTheGround
    {
        private set
        {
            if (_onTheGround != value)
            {
                _onTheGround = value;
                _animator.SetBool("grounded", _onTheGround);
            }
        }
        get => _onTheGround;
    }

    private void GroundCheck()
    {
        RaycastHit2D[] hits = new RaycastHit2D[5];
        int numhits = _rbody.Cast(Vector2.down, hits, 0.5f);
        _onTheGround = numhits > 0;
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && _onTheGround)
        {
            _animator.SetTrigger("jumpTrigger");
            _rbody.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }


    private bool _facingRight = true;

    public bool FacingRight
    {
        private set
        {
            if (_facingRight != value)
            {
                _facingRight = value;
                Flip();
            }
        }
        get => _facingRight;
    }

    public float VerticalSpeed
    {
        private set
        {
            if (_verticalSpeed != value)
            {
                _verticalSpeed = value;
                _animator.SetFloat("ySpeed", _verticalSpeed);
            }
        }
        get => _verticalSpeed;
    }
}