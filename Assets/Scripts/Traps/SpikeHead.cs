using UnityEngine;

public class SpikeHead : EnemyDamage
{
    [Header ("Spikehead Atrribute")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    private float checkTimer;
    private Vector3 destination;
    private Vector3[] directions = new Vector3[4];
    private bool attacking;

    [Header("SFX")]
    [SerializeField] private AudioClip impactSound;

    private void Update()
    {
        if (attacking)
            transform.Translate(destination * Time.deltaTime * speed);
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
                CheckForPlayer();
        }
    }

    private void OnEnable()
    {
        Stop();
    }
    private void CheckForPlayer()
    {
        CalculateDirections();

        //check if spikehead sees player in all directions 
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit =  Physics2D.Raycast(transform.position, directions[i],range, playerLayer);

            if (hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }

    }

    private void CalculateDirections()
    {
        directions[0] = transform.right * range;
        directions[1] = -transform.right * range;
        directions[2] = transform.up * range;
        directions[3] = -transform.up * range;


    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.instance.PlaySound(impactSound);
        base.OnTriggerEnter2D(collision);
        // stope spikhead after it hits something
        Stop();

    }

    private void Stop()
    {
        destination =  transform.position;
        attacking = false;
    }
}
