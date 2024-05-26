using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]   private float speed;
    private bool hit;
    private float direction;
    private float lifetime;


    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

    }


    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 5) Deactivate();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Health>().TakeDamage(1);
        }
    }

    public void SetDirection(float _direction)
        
    {
        

        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX= transform.localScale.x;
        if (Mathf.Sign(localScaleX) != direction)
            localScaleX  = -localScaleX;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);


    }

    private void Deactivate()
    {
        lifetime = 0;
        gameObject.SetActive(false);
    }
}
