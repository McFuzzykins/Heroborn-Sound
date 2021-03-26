using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{

    public float moveSpeed = 10f;
    public float rotateSpeed = 75f;
    public float jumpVelocity = 5f;
    public float distanceToGround = 0.1f;
    public LayerMask groundLayer;

    public GameObject bullet;
    public float bulletspeed = 100f;

    private float vInput;
    private float hInput;

    private Rigidbody _rb;

    private CapsuleCollider _col;

    private GameBehavior _gameManager;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();

        _gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }

    void Update()
    {

        vInput = Input.GetAxis("Vertical") * moveSpeed;


        hInput = Input.GetAxis("Horizontal") * rotateSpeed;

        /*
        this.transform.Translate(Vector3.forward * vInput * Time.deltaTime);
        this.transform.Rotate(Vector3.up * hInput * Time.deltaTime);
        */
    }
    void FixedUpdate()
    {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
        }
        bool IsGrounded()
        {
            Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z);
            bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);
            return grounded;
        }
        {
            Vector3 rotation = Vector3.up * hInput;
            Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
            _rb.MovePosition(this.transform.position + this.transform.forward * vInput * Time.fixedDeltaTime);
            _rb.MoveRotation(_rb.rotation * angleRot);
        }
        
            if (Input.GetMouseButtonDown(0)) {
            //Original Code is bugged. I replaced it directly below
            //GameObject newBullet = Instantiate(bullet, transform.position + new Vector3(1, 0, 0), transform.rotation) as GameObject;
            GameObject newBullet = Instantiate(bullet, transform.position + this.transform.right, transform.rotation) as GameObject;
            Rigidbody bulletRB =
            newBullet.GetComponent<Rigidbody>();
            bulletRB.velocity = this.transform.forward * bulletspeed;
             }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Enemy")
        {
            _gameManager.HP -= 1;
            if (_gameManager.GlassCannon)
            {
                _gameManager.HP -= 10;
            }
        }
        if (collision.gameObject.name == "Jump Boost Pickup")
        {
            _gameManager.Timer = 7f;
            jumpVelocity = 15f;
        }
        if (collision.gameObject.name == "Health_Pickup")
        {
            _gameManager.HP = 10;
        }
    }
}

