using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    public int maxHealth = 3;
    public float speed = 2f;
    public int contactDamage = 20;
    public float deathDuration = 0.8f;
    public enum EnemyState { Entering, Chasing, Dead }
    public EnemyState State { get; private set; }
    public bool IsDead => State == EnemyState.Dead;
    private int health;
    private Rigidbody2D body;
    private Transform player;
    private WaveManager waves;
    private Vector2 entryTarget;
    private Animator animator;
    private SpriteRenderer sprite;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        health = maxHealth;
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public void Initialize(Transform target, WaveManager manager, Vector2 entrance)
    {
        player = target;
        waves = manager;
        entryTarget = entrance;
        State = EnemyState.Entering;
    }

    private void Update()
    {
        if (!IsDead) animator.SetFloat("Speed", body.velocity.magnitude);
    }

    private void FixedUpdate()
    {
        if (IsDead || player == null || GameManager.Instance.IsFinished) { body.velocity = Vector2.zero; return; }
        // Primeiro atravessa a abertura; depois segue o jogador diretamente.
        if (State == EnemyState.Entering && Vector2.Distance(body.position, entryTarget) < 0.15f)
            State = EnemyState.Chasing;
        Vector2 target = State == EnemyState.Entering ? entryTarget : (Vector2)player.position;
        Vector2 direction = (target - body.position).normalized;
        body.velocity = direction * speed;
        if (Mathf.Abs(direction.x) > 0.05f) sprite.flipX = direction.x < 0;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        PlayerHealth target = collision.gameObject.GetComponent<PlayerHealth>();
        if (!IsDead && target != null && target.TakeDamage(contactDamage)) animator.SetTrigger("Attack");
    }

    public void TakeDamage(int damage)
    {
        if (IsDead || GameManager.Instance.IsFinished) return;
        health -= damage;
        if (health > 0) return;
        State = EnemyState.Dead;
        body.velocity = Vector2.zero;
        body.simulated = false;
        GetComponent<Collider2D>().enabled = false;
        animator.SetBool("Dead", true);
        GameManager.Instance.AddScore(100);
        StartCoroutine(FinishDeath());
    }

    private IEnumerator FinishDeath()
    {
        // A onda só termina depois que a última animação de morte acabar.
        yield return new WaitForSeconds(deathDuration);
        waves.EnemyDefeated();
        Destroy(gameObject);
    }
}
