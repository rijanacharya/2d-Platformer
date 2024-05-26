using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFrameDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;

    private bool invulnerable;
    public float currentHealth { get; private set; }

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();    
    }

    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth -  _damage, 0, startingHealth);
        if (currentHealth >0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerablity() );
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");
                // disable all attached components
                foreach (Behaviour component in components)
                    component.enabled = false;
                dead = true;
            }
           
            
        }
    }


    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator Invunerablity()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(8,9, true); //  8 is player layer, 9 is enemy 

        // invunerablity duratiom
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFrameDuration/ (numberOfFlashes *2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
        invulnerable = false;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
    
}
