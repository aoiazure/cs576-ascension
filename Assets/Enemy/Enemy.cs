using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    public GameManager gm;

    // STATS
    public float health = 50;

    public Transform goal;
    NavMeshAgent agent;
    public AudioClip death;

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
<<<<<<< Updated upstream
        gm.EnemyDie();
=======
        AudioSource.PlayClipAtPoint(death, transform.position);
>>>>>>> Stashed changes
        gm.enemy_list.Remove(this);
        Destroy(gameObject);
    }

}
