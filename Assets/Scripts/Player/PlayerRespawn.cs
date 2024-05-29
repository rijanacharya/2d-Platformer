using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;

    private Transform respawnPoint;

    private Health playerHealth;

    private UIManager uiManager;
    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
    }

    public void CheckRespawn()
    {
        //check if checkpoint is set

        if(respawnPoint == null)
        {
            //if not show game over screen
            uiManager.GameOver();
            return; 
        }

        transform.position = respawnPoint.position;
        playerHealth.Respawn();
        Camera.main.GetComponent<CameraController>().MoveToNewRoom(respawnPoint.parent);

    }

    //activate the checkpoint

    private void OnTriggerEnter2D(Collider2D collison)
    {
        if(collison.transform.tag == "Checkpoint")
        {
            respawnPoint = collison.transform;
            SoundManager.instance.PlaySound(checkpointSound);
            collison.GetComponent<Collider2D>().enabled = false;
            collison.GetComponent<Animator>().SetTrigger("appear");
        }
    }
}
