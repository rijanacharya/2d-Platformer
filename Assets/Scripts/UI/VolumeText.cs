using UnityEngine;
using UnityEngine.UI;

public class VolumeText : MonoBehaviour
{
    [SerializeField] private string volumeName;
    [SerializeField] private string volumeDescription;
    private Text txt;
    private void Awake()
    {
        txt = GetComponent<Text>();
    }
    private void Update()
    {
        txt.text = volumeDescription + ": " + (int)(PlayerPrefs.GetFloat(volumeName, 1) * 100) + "%";
    }
  
}
