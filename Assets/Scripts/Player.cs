using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb2d;

    private Vector2 oldMovementInput;

    private Vector2 pointerInput, movementInput;

    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public Vector2 PointerInput => pointerInput;

    [SerializeField]
    private InputActionReference movement, attack, pointerPosition;

    [SerializeField]
    private float maxSpeed = 2, acceleration = 50, deacceleration = 100;
    [SerializeField]
    private float currentSpeed = 0;

    private WeaponParent weaponParent;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        weaponParent = GetComponentInChildren<WeaponParent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        pointerInput = GetPointerInput();
        weaponParent.PoiterPosition = pointerInput;
        movementInput = movement.action.ReadValue<Vector2>();
        Debug.Log(pointerInput.x);

        if(movementInput.magnitude > 0 && currentSpeed >= 0)
        {
            oldMovementInput = movementInput;
            currentSpeed += acceleration * maxSpeed * Time.deltaTime;
            animator.SetBool("isMoving", true);
            if (movementInput.x > 0)
            {
                if (pointerInput.x < 0)
                {
                    spriteRenderer.flipX = true;
                }
                else
                {
                    spriteRenderer.flipX = false;
                }
            }
            else
            {
                if (pointerInput.x < 0)
                {
                    spriteRenderer.flipX = true;
                }
                else
                {
                    spriteRenderer.flipX = false;
                }
            }
        }
        else
        {
            currentSpeed -= deacceleration * maxSpeed * Time.deltaTime;
            animator.SetBool("isMoving", false);
            if (pointerInput.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        rb2d.velocity = oldMovementInput * currentSpeed;

    }

    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane; 
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

}
