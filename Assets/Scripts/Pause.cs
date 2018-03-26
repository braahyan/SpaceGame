using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool Paused { get; set; }

    // Update is called once per frame
    private void Update()
    {
        var pause = Input.GetButtonDown("Pause");
        if (pause && !Paused)
        {
            Time.timeScale = 0.0f;
            Paused = true;
        }
        else if (pause && Paused)
        {
            Time.timeScale = 1.0f;
            Paused = false;
        }
    }
}
