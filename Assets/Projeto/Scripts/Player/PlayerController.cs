using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Projectile projectilePrefab;
    public float fireCooldown = 0.3f;
    private Rigidbody2D body;
    private Camera worldCamera;
    private Vector2 movement;
    private float nextShot;
    private Animator animator;
    private float attackUntil;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        worldCamera = Camera.main;
        animator = GetComponent<Animator>();
        animator.SetFloat("Direction", 7);
    }

    private void Update()
    {
        if (GameManager.Instance == null || GameManager.Instance.IsFinished) { movement = Vector2.zero; return; }
        Move(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
        animator.SetFloat("Speed", body.velocity.magnitude);
        if (Input.GetMouseButton(0) && Time.time >= nextShot &&
            !(EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()))
        {
            ShootAt(worldCamera.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    public void Move(Vector2 direction)
    {
        movement = Vector2.ClampMagnitude(direction, 1f);
        if (movement.sqrMagnitude > 0.01f && Time.time >= attackUntil) Face(movement);
    }

    public bool ShootAt(Vector2 worldPosition)
    {
        if (GameManager.Instance.IsFinished || Time.time < nextShot) return false;
        Vector2 direction = worldPosition - (Vector2)transform.position;
        if (direction.sqrMagnitude < 0.001f) return false;
        direction.Normalize();
        Projectile shot = Instantiate(projectilePrefab, (Vector2)transform.position + direction * 0.4f,
            Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));
        shot.Launch(direction);
        Face(direction);
        animator.SetTrigger("Attack");
        attackUntil = Time.time + 0.29f;
        nextShot = Time.time + fireCooldown;
        return true;
    }

    private void Face(Vector2 direction)
    {
        // As oito linhas das folhas começam em sudeste e seguem em sentido anti-horário.
        int row = (Mathf.RoundToInt(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg / 45f) + 9) % 8;
        animator.SetFloat("Direction", row);
    }

    private void FixedUpdate()
    {
        body.velocity = GameManager.Instance != null && !GameManager.Instance.IsFinished ? movement * speed : Vector2.zero;
    }
}
