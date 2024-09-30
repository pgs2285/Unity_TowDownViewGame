using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 500f;

    [Header("Ground Check Settings")]
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] Vector3 groundCheckOffset;
    [SerializeField] LayerMask groundLayer;

    
    
    bool isGrounded;
    bool hasControl = true;
    
    public bool InAction { get; private set; } 

    Vector3 desiredMoveDir;
    Vector3 moveDir;
    Vector3 velocity;

    public bool IsOnLedge { get; set; }
    //public LedgeData LedgeData { get; set; }

    float ySpeed;
    Quaternion targetRotation;

    CameraController cameraController;
    Animator animator;
    CharacterController characterController;
    // EnvironmentScanner environmentScanner;
    private void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        //environmentScanner = GetComponent<EnvironmentScanner>();
    }


    private void Update()
    {

       DetectInteract();

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float moveAmount = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v));
        
        Vector3 moveInput = new Vector3(h, 0, v).normalized;

        desiredMoveDir = cameraController.PlanarRotation * moveInput;
        moveDir = desiredMoveDir;   
        if (!hasControl) return;

        velocity = Vector3.zero;

        GroundCheck();
        //animator.SetBool("IsGrounded", isGrounded);
        if(isGrounded)
        {
            velocity = desiredMoveDir * moveSpeed;
            ySpeed = -0.5f;
            //IsOnLedge = environmentScanner.LedgeCheck(desiredMoveDir, out LedgeData ledgeData);
            // if(IsOnLedge)
            // {
            //     LedgeData = ledgeData;
            //     //LedgeMovement();
            // }
            animator.SetFloat("Speed", velocity.magnitude / moveSpeed, 0.2f, Time.deltaTime);
        }
        else
        {
            ySpeed += Physics.gravity.y * Time.deltaTime;

            velocity = transform.forward * moveSpeed / 2;
        }

        velocity.y = ySpeed;
        characterController.Move(velocity* Time.deltaTime);

        if (moveAmount > 0 && moveDir.magnitude > 0.2f) // ledge�� ������ 90�� �̻��� �ƴϸ� �������� �ʰ� �ߴµ�, zero�� �Ǹ� �÷��̾ ȸ���ϴ� �����ϱ� ���� magnitude��
        {
            targetRotation = Quaternion.LookRotation(moveDir);
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);



    }
    [Header("Interact")]
    public float interactDistance = 2f;
    public LayerMask interactLayer;
    public GameObject interactUI;
    public void DetectInteract()
    {
        // Debug.DrawRay(transform.position,transform.forward * interactDistance, Color.white);
        // if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit,interactDistance, interactLayer))
        // {
            //Debug.Log(hit.transform.name);
            // IInteractable i = hit.transform?.GetComponent<IInteractable>();
            // if (i == null) i = hit.transform?.GetComponentInParent<IInteractable>();// ���� collider�� ����������Ʈ�� �־ ������ ã�ƾ��ҽ�
        //
        //     i.LookAt(hit.transform);
        //     if (Input.GetKeyDown(KeyCode.F))
        //     {
        //         i.Interact();
        //     }
        // }
        // else
        // {
        //     interactUI.SetActive(false);
        // }
        
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius, groundLayer);
    }
    
    
    public void SetControl(bool hasControl)
    {
        Cursor.visible = !hasControl;
        if(hasControl)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        //Cursor.lockState = CursorLockMode.Locked;
        Camera.main.GetComponent<CameraController>().enabled = hasControl;
        this.hasControl = hasControl;
        characterController.enabled = hasControl;

        if(!hasControl)
        {
            animator.SetFloat("moveAmount", 0f);
            targetRotation = transform.rotation;
        }
    }

    public bool HasControl
    {
        get => hasControl;
        set => hasControl = value;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius);
    }

    [SerializeField] private GameObject hitEffect;
    private bool isHit = false;
    public void TakeDamage(int damage, Vector3 hitPoint)
    {
        CharacterState.Instance.HP -= damage;
        animator.SetTrigger("Hit");
        Instantiate(hitEffect, hitPoint, Quaternion.identity);
    }

    public void SetHitOrigin()
    {
        isHit = false;
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Obstacle") && !isHit)
        {
            isHit = true;
            TakeDamage(5, hit.point);
        }
    }
    public float RotationSpeed => rotationSpeed;

}
