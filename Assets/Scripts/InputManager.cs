using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private Animator m_Animator;
    public PlayerInput.OnFootActions onFootActions;
    private PlayerLook playerLook;
    private PlayerMovement PlayerMovement;
    private bool isAttacking = false;
    public AudioSource music;
    public AudioClip trakeOrHitMusic;
    void Awake()
    {
        playerInput = new PlayerInput();
        onFootActions = playerInput.OnFoot;
        PlayerMovement = GetComponent<PlayerMovement>();
        playerLook = GetComponent<PlayerLook>();
        onFootActions.Jump.performed += ctx => PlayerMovement.jump();
        m_Animator = GetComponentInChildren<Animator>();
        m_Animator.SetTrigger("TrIdle");
    }

    void Update()
    {
        Vector2 movementInput = onFootActions.Movement.ReadValue<Vector2>();

        PlayerMovement.ProcessMove(movementInput);

        if (!isAttacking)
        {
            if (movementInput.magnitude > 0)
            {
                m_Animator.SetTrigger("TrGo");
            }
            else
            {
                m_Animator.SetTrigger("TrIdle");
            }
        }   
    }

    public void TriggerAttack()
    {
        if (!isAttacking) 
        {
            StartCoroutine(HandleAttackAnimation());
        }
    }

    private IEnumerator HandleAttackAnimation()
    {
        isAttacking = true;
        m_Animator.SetTrigger("TrAttack");
        music.clip = trakeOrHitMusic;
        if (music != null && !music.isPlaying)
        {
            music.Play();
        }
        yield return new WaitForSeconds(m_Animator.GetCurrentAnimatorStateInfo(0).length);

        isAttacking = false;
    }

    public void LateUpdate()
    {
        playerLook.look(onFootActions.Look.ReadValue<Vector2>());
        
    }

    private void OnEnable()
    {
        onFootActions.Enable();
    }

    private void OnDisable()
    {
        onFootActions.Disable();
        
    }
}
