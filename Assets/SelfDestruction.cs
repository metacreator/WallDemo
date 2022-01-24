using UnityEngine;

public class SelfDestruction : MonoBehaviour
{
    public float timeLeft;

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0.0f) Destroy(gameObject);
    }
}