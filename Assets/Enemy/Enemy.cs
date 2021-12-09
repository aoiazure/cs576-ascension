using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    // STATS
    public float health = 50;

    public Transform goal;
    NavMeshAgent agent;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
    }

    private void FixedUpdate() {
        agent.destination = goal.position;
    }

    public void TakeDamage(float amount) {
        health -= amount;
        if (health <= 0f) {
            Die();
        }
    }

    void Die() {
        Destroy(gameObject);
    }

}
