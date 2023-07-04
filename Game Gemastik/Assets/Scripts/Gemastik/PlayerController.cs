using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    private float moveSpeed;
    private float walkSpeed = 2f;
    private float runSpeed = 6f;

    [SerializeField] private float speed_jump = 2f;
    [SerializeField] private float gravitasi = -9.81f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.8f;
    [SerializeField] private LayerMask groundMask;
    public bool isGrounded;
    Vector3 velocity;

    NavMeshAgent agent;
    [Header("Movement")]
    [SerializeField] ParticleSystem clickEffect;
    [SerializeField] LayerMask clickableLayers;

    float lockRotationSpeed = 8f; 

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        

    }

    // Update is called once per frame
    void Update()
    {
        
        Pergerakan();
        Lari(); 
        Gravity();
        Lompat();
        ClickToMove();
    }

    void ClickToMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, clickableLayers))
            {
                agent.enabled = true;
                controller.enabled = false;
                agent.destination = hit.point;
                FaceTarget();
                if (clickEffect != null)
                {
                    Instantiate(clickEffect, hit.point += new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);
                }
            }
        }

        if (agent.velocity != Vector3.zero)
        {
            animator.SetFloat("Speed", 0.3f);
            animator.SetBool("Running", true);
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (agent.destination - transform.position).normalized;
        Quaternion lockRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lockRotation, Time.deltaTime * lockRotationSpeed);
    }

    void Pergerakan()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(horizontal, 0f,vertical).normalized;
        Vector3 velocity = moveSpeed * Time.deltaTime * dir;


        if (dir.magnitude >= 0.1f)
        {
            
            controller.enabled = true;
            agent.enabled = false;
            transform.rotation= Quaternion.LookRotation(dir);

            controller.Move(velocity);
        }
        animator.SetFloat("Speed", velocity.magnitude);
        
    }

    void Lari()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = runSpeed;
            animator.SetBool("Running", true);
        }
        else
        {
            moveSpeed = walkSpeed;
            animator.SetBool("Running", false);
        }
    }

    private void Gravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

    }

    private void Lompat()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            controller.enabled = true;
            agent.enabled = false;
            animator.SetBool("Grounded", isGrounded);
            velocity.y = Mathf.Sqrt(speed_jump * -2f * gravitasi);
        }
        velocity.y += gravitasi * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        animator.SetBool("Grounded", isGrounded);
    }

   /* private void OnTriggerStay(Collider other)
    {
        if (other.name == "Bonus Beruang")
        {
            controller.enabled = true;
        }

    }*/
}
