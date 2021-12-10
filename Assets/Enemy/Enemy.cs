using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    public GameManager gm;
    public Zone zone = null;
    // STATS
    public float health = 50;

    public Transform goal;
    NavMeshAgent agent;
    public AudioClip death;
    public AudioClip hit;

    void Start() {
        gm.enemy_list.Add(this); // add reference to enemy to enemy_list for later
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
        
        gm.EnemyDie();
        AudioSource.PlayClipAtPoint(death, transform.position);
        gm.enemy_list.Remove(this);
        if (zone != null)
            this.zone.EnemyInZoneDie();
        Destroy(gameObject);
    }
}
