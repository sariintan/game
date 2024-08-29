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

    // Start is called before the first frame update
    void Start()
    {

    }

    private float _HorizontalMovement;

    private void setHorizontalMovement(float value)
    {
        if (value != _horizontalMovement)
        {
            _horizontalMovement = value;
        }

    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
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

    private bool OnTheGround()
    {
        RaycastHit2D hit =
            Physics2D.Raycast(transform.position, Vector2.down, 2f, groundLayer);
        return hit.collider != null;
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
            _rbody.AddForce(
                 Vector2.up * jumpHeight,
                 ForceMode2D.Impulse
                );
        }
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
    private void Flip(float speed)
    {
        if (speed > 0)
            transform.rotation = new Quaternion(
                  transform.rotation.x,
                  0,
                  transform.rotation.z,
                  transform.rotation.w

                );
        else if (speed < 0)
            transform.rotation = new Quaternion(
                  transform.rotation.x,
                  180,
                  transform.rotation.z,
                  transform.rotation.w

                );
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
}