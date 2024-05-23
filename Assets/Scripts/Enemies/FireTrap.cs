using System.Collections;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header("FireTrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activationTime;
    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered;
    private bool active;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!triggered)
            {
                StartCoroutine(ActivateFireTrap());
            }
            if (active)
            {
                collision.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }

    private IEnumerator ActivateFireTrap()
    {

        // turn the sprite red to notify the player and trigger trap
        triggered = true;
        spriteRend.color = Color.red;

        // wait for  the delay, activate trap, turn on animation, return color back to normal
        yield return new WaitForSeconds(activationDelay);
        spriteRend.color = Color.white;
        active = true;
        anim.SetBool("activated", true);

        // wait until x seconds , deactivate trap, and reset all variable and animator
        yield return new WaitForSeconds(activationTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);

    }
}
