using UnityEngine;

/*
 * Статистика игрока
 */
public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public Stat damage; //броня
    public Stat armor; //ущерб

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }*/
    }

    /*
     * Нанесение урона,
     * Отнять очки здоровья
     * int damage - урон
     */
    public void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue); //для возврата только положит. value

        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Override for enemy and player
    public virtual void Die()
    {
        //Die in some way
        Debug.Log(transform.name + "died.");
    }
}
