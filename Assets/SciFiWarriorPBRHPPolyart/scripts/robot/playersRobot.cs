using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playersRobot : MonoBehaviour
{
    private enum ControlMode
    {
        /// <summary>
        /// Up moves the character forward, left and right turn the character gradually and down moves the character backwards
        /// </summary>
        Tank,
        /// <summary>
        /// Character freely moves in the chosen direction from the perspective of the camera
        /// </summary>
        Direct
    }
    [SerializeField] private float m_moveSpeed = 2;
    [SerializeField] private float m_moveRun = 5;

    [SerializeField] private float m_turnSpeed = 200;
    [SerializeField] private float m_jumpForce = 4;
    private float horizontalInput;
    private float verticalInput;
    [SerializeField] private Animator m_animator = null;
    [SerializeField] private Rigidbody m_rigidBody = null;

    [SerializeField] private ControlMode m_controlMode = ControlMode.Direct;


    private bool m_wasGrounded;
    private Vector3 m_currentDirection = Vector3.zero;

    private float m_jumpTimeStamp = 0;
    private float m_minJumpInterval = 0.25f;
    private bool m_jumpInput = false;

    private bool m_isGrounded;

    private List<Collider> m_collisions = new List<Collider>();


    private void Awake()
    {
        if (!m_animator) { gameObject.GetComponent<Animator>(); }
        if (!m_rigidBody) { gameObject.GetComponent<Animator>(); }
    }
    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal"); // Changed forwardInput to Input
        verticalInput = Input.GetAxis("Vertical"); // Changed forwardInput to Input


        if (Input.GetKey("up"))
        {
            // ? move forward 
            up();

        }
        if (Input.GetKey("down"))
        {
            // ? move backword
            down();
        }
        if (Input.GetKey("left"))
        {
            // ? move left 
            left();
        }
        if (Input.GetKey("right"))
        {
            // ? move right 
            right();
        }
        if (Input.GetKey("w"))
        {
            // ? move right 
            shoot();
        }
        if (Input.GetKey("e") && !Input.GetKey("up") && !Input.GetKey("down"))
        {
            // ? move right 
            shoot_auto();
        }
        if (Input.GetKey("q"))
        {
            // ? move right 
            shooIdle();
        }
        if (Input.GetKey("r") && (Input.GetKey("up")))
        {
            // ? move run
            run();
        }

        if (Input.GetKey("space"))
        {
            // ? move right 
            jump();
        }
        if (!Input.anyKey)
        {
            // ? idle mode
            idle();
        }
    }


    //////////////////////////////////
    //////////////////////////////////
    //////////////////////////////////


    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!m_collisions.Contains(collision.collider))
                {
                    m_collisions.Add(collision.collider);
                }
                m_isGrounded = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true; break;
            }
        }

        if (validSurfaceNormal)
        {
            m_isGrounded = true;
            if (!m_collisions.Contains(collision.collider))
            {
                m_collisions.Add(collision.collider);
            }
        }
        else
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }
            if (m_collisions.Count == 0) { m_isGrounded = false; }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (m_collisions.Contains(collision.collider))
        {
            m_collisions.Remove(collision.collider);
        }
        if (m_collisions.Count == 0) { m_isGrounded = false; }
    }

    //////////////////////////////////
    //////////////////////////////////
    //////////////////////////////////

    // todo move character //
    void idle()
    {
        m_animator.SetBool("isIdle", true);
        m_animator.SetBool("isWalking", false);
        m_animator.SetBool("isDown", false);
        m_animator.SetBool("isRun", false);
        m_animator.SetBool("isLeft", false);
        m_animator.SetBool("isRight", false);
        m_animator.SetBool("isJump", false);
        m_animator.SetBool("isShoot", false);
        m_animator.SetBool("isShoot3", false);
        m_animator.SetBool("isIdleShoot", false);
        m_animator.SetBool("isDie", false);


    }
    void up()
    {
        m_animator.SetBool("isIdle", false);
        m_animator.SetBool("isWalking", true);
        m_animator.SetBool("isDown", false);
        m_animator.SetBool("isRun", false);
        m_animator.SetBool("isLeft", false);
        m_animator.SetBool("isRight", false);
        m_animator.SetBool("isJump", false);
        m_animator.SetBool("isShoot", false);
        m_animator.SetBool("isShoot3", false);
        m_animator.SetBool("isIdleShoot", false);
        m_animator.SetBool("isDie", false);


        var velocity = Vector3.forward * verticalInput * m_moveSpeed;
        transform.Translate(velocity * Time.deltaTime);


    }
    void down()
    {
        m_animator.SetBool("isIdle", false);
        m_animator.SetBool("isWalking", false);
        m_animator.SetBool("isDown", true);
        m_animator.SetBool("isRun", false);
        m_animator.SetBool("isLeft", false);
        m_animator.SetBool("isRight", false);
        m_animator.SetBool("isJump", false);
        m_animator.SetBool("isShoot", false);
        m_animator.SetBool("isShoot3", false);
        m_animator.SetBool("isIdleShoot", false);
        m_animator.SetBool("isDie", false);


        // todo Calculate the backward movement
        var velocity = Vector3.back * m_moveSpeed;
        transform.Translate(velocity * Time.deltaTime);
    }
    void left()
    {
        m_animator.SetBool("isIdle", false);
        m_animator.SetBool("isWalking", false);
        m_animator.SetBool("isDown", false);
        m_animator.SetBool("isRun", false);
        m_animator.SetBool("isLeft", true);
        m_animator.SetBool("isRight", false);
        m_animator.SetBool("isJump", false);
        m_animator.SetBool("isShoot", false);
        m_animator.SetBool("isShoot3", false);
        m_animator.SetBool("isIdleShoot", false);
        m_animator.SetBool("isDie", false);


        transform.Rotate(0, -m_turnSpeed * Time.deltaTime, 0);
    }
    void right()
    {
        m_animator.SetBool("isIdle", false);
        m_animator.SetBool("isWalking", false);
        m_animator.SetBool("isDown", false);
        m_animator.SetBool("isRun", false);
        m_animator.SetBool("isLeft", false);
        m_animator.SetBool("isRight", true);
        m_animator.SetBool("isJump", false);
        m_animator.SetBool("isShoot", false);
        m_animator.SetBool("isShoot3", false);
        m_animator.SetBool("isIdleShoot", false);
        m_animator.SetBool("isDie", false);



        transform.Rotate(0, m_turnSpeed * Time.deltaTime, 0);

    }
    void run()
    {
        m_animator.SetBool("isIdle", false);
        m_animator.SetBool("isWalking", false);
        m_animator.SetBool("isDown", false);
        m_animator.SetBool("isRun", true);
        m_animator.SetBool("isLeft", false);
        m_animator.SetBool("isRight", false);
        m_animator.SetBool("isJump", false);
        m_animator.SetBool("isShoot", false);
        m_animator.SetBool("isShoot3", false);
        m_animator.SetBool("isIdleShoot", false);
        m_animator.SetBool("isDie", false);


        var velocityrun = Vector3.forward * verticalInput * m_moveRun;
        transform.Translate(velocityrun * Time.deltaTime);

    }

    void shoot()
    {
        m_animator.SetBool("isIdle", false);
        m_animator.SetBool("isWalking", false);
        m_animator.SetBool("isDown", false);
        m_animator.SetBool("isRun", false);
        m_animator.SetBool("isLeft", false);
        m_animator.SetBool("isRight", false);
        m_animator.SetBool("isJump", false);
        m_animator.SetBool("isShoot", true);
        m_animator.SetBool("isShoot3", false);
        m_animator.SetBool("isIdleShoot", false);
        m_animator.SetBool("isDie", false);

    }
    void shoot_auto()
    {
        m_animator.SetBool("isIdle", false);
        m_animator.SetBool("isWalking", false);
        m_animator.SetBool("isDown", false);
        m_animator.SetBool("isRun", false);
        m_animator.SetBool("isLeft", false);
        m_animator.SetBool("isRight", false);
        m_animator.SetBool("isJump", false);
        m_animator.SetBool("isShoot", false);
        m_animator.SetBool("isShoot3", true);
        m_animator.SetBool("isIdleShoot", false);
        m_animator.SetBool("isDie", false);

    }
    void shooIdle()
    {
        m_animator.SetBool("isIdle", false);
        m_animator.SetBool("isWalking", false);
        m_animator.SetBool("isDown", false);
        m_animator.SetBool("isRun", false);
        m_animator.SetBool("isLeft", false);
        m_animator.SetBool("isRight", false);
        m_animator.SetBool("isJump", false);
        m_animator.SetBool("isShoot", false);
        m_animator.SetBool("isShoot3", false);
        m_animator.SetBool("isIdleShoot", true);
        m_animator.SetBool("isDie", false);

    }
    void reload()
    {
        m_animator.SetBool("", true);
        m_animator.SetBool("", false);
        m_animator.SetBool("", false);
        m_animator.SetBool("", false);
        m_animator.SetBool("", false);
        m_animator.SetBool("", false);
        m_animator.SetBool("isJump", false);
        m_animator.SetBool("isShoot", false);
        m_animator.SetBool("isShoot3", false);
        m_animator.SetBool("isIdleShoot", false);
        m_animator.SetBool("isDie", false);

    }
    void die()
    {
        m_animator.SetBool("isIdle", false);
        m_animator.SetBool("isWalking", false);
        m_animator.SetBool("isDown", false);
        m_animator.SetBool("isRun", false);
        m_animator.SetBool("isLeft", false);
        m_animator.SetBool("isRight", false);
        m_animator.SetBool("isJump", false);
        m_animator.SetBool("isShoot", false);
        m_animator.SetBool("isShoot3", false);
        m_animator.SetBool("isIdleShoot", false);
        m_animator.SetBool("isDie", true);

    }

    void jump()
    {
        m_animator.SetBool("isJump", true);
        m_animator.SetBool("isIdle", false);
        m_animator.SetBool("isWalking", false);
        m_animator.SetBool("isDown", false);
        m_animator.SetBool("isRun", false);
        m_animator.SetBool("isLeft", false);
        m_animator.SetBool("isRight", false);
        m_animator.SetBool("isShoot", false);
        m_animator.SetBool("isShoot3", false);
        m_animator.SetBool("isIdleShoot", false);
        m_animator.SetBool("isDie", false);

    }
}
