using UnityEngine;
public class Cartridge : MonoBehaviour
{
    public string weaponName;
    public int shellCount = 30;
    public int maxShell = 30;
    public float loadTime = 1.5f;
    private int currentCount;
    public int chargerCount = 5;
    private float currentLoadTime;

    public bool isLoading()
    {
        if (chargerCount == 0) return true;
        if (currentCount < shellCount)
        {
            return false;
        }
        currentLoadTime += Time.deltaTime;
        if (currentLoadTime >= loadTime)
        {
            currentLoadTime = 0;
            chargerCount--;
            currentCount = 0;
            return false;
        }
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
}

