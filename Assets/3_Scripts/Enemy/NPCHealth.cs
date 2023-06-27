using UnityEngine;
using System.Collections;

public class NPCHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private bool isDead = false;

    private Animator m_animator;

    private void Start()
    {
        currentHealth = maxHealth;
        //m_animator = GetComponent<Animator>();

        // Get all Rigidbody components in children
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        // Loop over the rigidbodies and disable them
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = true;
        }

    }

    public bool TakeDamage(int damageAmount)
    {

        Debug.Log("vurdun");
        if (isDead)
        {
            return false;
        }

        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
            return true;
        }
        else
        {
            //m_animator.SetTrigger("Hurt");
            return false;
        }
    }

    private void Die()
    {
        isDead = true;

        // Get the EnemyShooting script and disable it
        Enemy enemyShooting = GetComponent<Enemy>();
        if (enemyShooting != null)
        {
            enemyShooting.enabled = false;
        }

        // Get the Animator component and disable it
        Animator animator = GetComponent<Animator>();
        animator.enabled = false;

        // Get all Rigidbody components in children and enable them
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = false;
        }

        //m_animator.SetTrigger("Death");
        // Add any other necessary logic for NPC death
        // For recovery, you can use a coroutine or a timer to trigger the recovery animation after a certain duration.
        //StartCoroutine(RecoverAfterDelay());
    }

}