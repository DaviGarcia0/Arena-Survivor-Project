using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;
    public float lifetime = 3f;
    private bool hit;

    public void Launch(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().velocity = direction * speed;
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hit || GameManager.Instance.IsFinished) return;
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy != null) enemy.TakeDamage(damage);
        else if (other.gameObject.layer != LayerMask.NameToLayer("Walls")) return;
        hit = true;
        Destroy(gameObject);
    }
}
