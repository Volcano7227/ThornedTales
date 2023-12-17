using UnityEngine;

public class TTD : MonoBehaviour
{
    [SerializeField] float initialTimeToDeactivate = 2;
    float timeToDeactivate;

    /// <summary>
    /// Reset deactivation time on the GameObject's enabling
    /// </summary>
    private void OnEnable()
    {
        timeToDeactivate = initialTimeToDeactivate;
    }

    /// <summary>
    /// Deactivate the GameObject after the set deactivation time
    /// </summary>
    void Update()
    {
        timeToDeactivate -= Time.deltaTime;
        if (timeToDeactivate <= 0)
            gameObject.SetActive(false);
    }
}