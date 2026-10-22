using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public float contactCooldown = 1f;
    public int CurrentHealth { get; private set; }
    private float nextDamage;

    private void Awake() { CurrentHealth = maxHealth; }

    public bool TakeDamage(int damage)
    {
        if (GameManager.Instance.IsFinished || Time.time < nextDamage || CurrentHealth <= 0) return false;
        nextDamage = Time.time + contactCooldown;
        CurrentHealth = Mathf.Max(0, CurrentHealth - damage);
        GameManager.Instance.ui.UpdateHealth(CurrentHealth, maxHealth);
        if (CurrentHealth == 0) GameManager.Instance.Finish(false);
        return true;
    }
}
