using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D _rbody;
    private float _horizontalMovement;
    private bool _onTheGround;

    // Start is called before the first frame update
    void Start()
    {

    }

    private float HorizontalMovement;

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
        _horizontalMovement = Input.GetAxis("Horizontal");
        Jump();
    }

    private void Awake()
    {
        _rbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        _rbody.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, _rbody.velocity.y);
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
}