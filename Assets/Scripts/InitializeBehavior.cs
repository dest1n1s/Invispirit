using UnityEngine;

public class InitializeBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Screen.fullScreen = false;
        Screen.SetResolution(1440, 720, false);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}
