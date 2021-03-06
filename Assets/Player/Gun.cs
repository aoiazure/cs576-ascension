using UnityEngine;

public class Gun : MonoBehaviour {

    // Gun Stats
    public float damage = 10f;
    public float range = 100f;
    public float firerate = 20f;

    // References
    public Camera fps_cam;
    public GameObject muzzle_transform;
    public GameObject muzzle_flash_effect;
    public GameObject bullet_impact_effect;
    public GameObject gun;
    public AudioSource gunShot;

    // fire rate control
    private float next_time_to_fire = 0.0f;
    public bool is_firing = false;

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Fire1") && Time.time >= next_time_to_fire) {
            // Muzzle Flash Particles
            GameObject mzf = Instantiate(muzzle_flash_effect, muzzle_transform.transform.position, Quaternion.LookRotation(fps_cam.transform.forward, Vector3.up));
            mzf.transform.SetParent(this.transform);
            Destroy(mzf, 0.15f);
            // Shoot
            if (!is_firing && Time.time >= next_time_to_fire) {
                next_time_to_fire = Time.time + 1f / firerate;
                Shoot();
            }
        }
    }

    void Shoot() {
        is_firing = true;
        gun.GetComponent<Animator>().Play("firepistol");
        gunShot.Play();
        RaycastHit hit;
        if (Physics.Raycast(fps_cam.transform.position, fps_cam.transform.forward, out hit, range)) {
            // Debug.Log(hit.transform.name);
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null) {
                enemy.TakeDamage(damage);
            }
            GameObject bip = Instantiate(bullet_impact_effect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(bip, 1f);
        }
        gun.GetComponent<Animator>().Play("idle");
        is_firing = false;
    }
}
