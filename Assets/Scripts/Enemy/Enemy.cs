using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    private StateMachine m_Machine;
    private Animator m_Animator;
    private NavMeshAgent agent;
    public Way way;
    private GameObject player;
    public float cHealthMax = 150f;
    private float cHealth;
    private Vector3 lastPos;
    public GameObject Player { get => player; }
    public Vector3 LastPos { get => lastPos; set => lastPos = value; }
    public NavMeshAgent Agent {  get =>  agent; }
    [Header("Sight values")]
    public float sightDis = 20f;
    public float fieldOfView = 85f;
    public float eyeHight;
    [Range(0.1f, 10f)]
    public float fireRate;
    [SerializeField]
    private string currentState;
    private bool attackFlag;
    private float attackRange = 2f;
    private float moveSpeed = 1.2f;
    private MusicManager musicManager;
    public int scoreValue = 10;
    public AudioSource playerMusic;
    public AudioClip deathMusic;
    private void OnCollisionEnter(Collision collision)
    {

        Transform hitTransform = collision.transform;
        if (hitTransform.CompareTag("Player"))
        {
            Debug.Log("Attack Player");
            attackFlag = true;
            hitTransform.GetComponent<PlayerHealth>().TakeDamage(UnityEngine.Random.Range(3, 10));
            m_Machine.ChangeState(new AttackState());
            Vector3 direction = (hitTransform.position - this.transform.position).normalized;
            this.transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    void Start()
    {
        m_Machine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        musicManager = FindObjectOfType<MusicManager>();
        m_Machine.Init();
        player = GameObject.FindGameObjectWithTag("Player");
        m_Animator = GetComponentInChildren<Animator>();
        cHealth = cHealthMax;
    }

    void Update()
    {
        CanSeePlayer();
        currentState = m_Machine.currentState.ToString();
        Debug.Log($"{currentState}");
        if (m_Animator != null)
        {
            
            if (attackFlag)
            {
                m_Animator.SetTrigger("TrAttack");
                attackFlag = false;
            }
        }
        if (currentState == "AttackState")
        {
            musicManager?.NotifyAttackStarted(this); 
        }
        else
        {
            musicManager?.NotifyAttackStopped(this); 
        }
    }

    public bool CanSeePlayer()
    {
        if(player != null)
        {
            if(Vector3.Distance(transform.position, player.transform.position) < sightDis)
            {
                Vector3 targetDir = player.transform.position - transform.position - (Vector3.up * eyeHight);
                float angleP = Vector3.Angle(targetDir, transform.forward);
                if (angleP >= -fieldOfView && angleP <= fieldOfView) { 
                    Ray rayEnemy = new Ray(transform.position + (Vector3.up * eyeHight) , targetDir);
                    RaycastHit hit = new RaycastHit();
                    if (Physics.Raycast(rayEnemy, out hit, sightDis)) { 
                        if(hit.transform.gameObject == player)
                        { 
                            Debug.DrawRay(rayEnemy.origin, rayEnemy.direction * sightDis);
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }
    public void TakeDamage(float damage)
    {
        cHealth -= damage;
        if (cHealth <= 0)
        {
            StartCoroutine(HandleDeath());
        }
    }

    private IEnumerator HandleDeath()
    {
        ScoreManager uiManager = FindObjectOfType<ScoreManager>();
        if (uiManager != null)
        {
            uiManager.AddScore(scoreValue);
        }
        m_Animator.SetTrigger("TrDeath");
        playerMusic.clip = deathMusic;
        playerMusic.Play();

        agent.enabled = false;
        musicManager?.NotifyAttackStopped(this);

        float animationDuration = m_Animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationDuration);

        Destroy(gameObject);
    }

}
