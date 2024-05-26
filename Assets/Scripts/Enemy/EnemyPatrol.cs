using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header ("Patrol Points")]  
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movinngleft;

    [Header ("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("Animation")]
    [SerializeField] private Animator anim;
    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void Update()
    {
        if (movinngleft)
        {
            if (enemy.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else
                DirectionChange();
            
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
                DirectionChange();
          
        }
    }

    private void DirectionChange()
    {
        anim.SetBool("moving", false);
        idleTimer += Time.deltaTime;
        if (idleTimer> idleDuration)
        {
            movinngleft = !movinngleft;
        }

    }
    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        anim.SetBool("moving", true);
        //make enemy face direction
        enemy.localScale = new Vector3( Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);

        //move enemy in that direction
        enemy.position = new Vector3(enemy.position.x + _direction * speed * Time.deltaTime,
            enemy.position.y, enemy.position.z);
    }

    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }

}
