using UnityEngine;
using UnityEngine.UI;
public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    [SerializeField] private AudioClip changeSound;
    [SerializeField] private AudioClip interactSound;
    private RectTransform rect;
    private int currentposition;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangePosition(-1);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangePosition(1);
        }
        else if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space))
        {
            Interact();
        }
    }

    private void ChangePosition(int _change)
    {
        currentposition += _change;
        if(_change != 0)
            SoundManager.instance.PlaySound(changeSound);

        if (currentposition >= options.Length)
        {
               currentposition = 0;
        }
        else if (currentposition < 0)
        {
            currentposition = options.Length - 1;
        }

        //assign the y position of the rect to the y position of the current option
        rect.position = new Vector3(rect.position.x,
            options[currentposition].position.y, 0);
    }

    private void Interact()
    {
        SoundManager.instance.PlaySound(interactSound);

        // Access the button component of the current option and simulate a click

        options[currentposition].GetComponent<Button>().onClick.Invoke();

    }
}
