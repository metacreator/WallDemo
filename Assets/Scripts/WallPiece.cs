using UnityEngine;

public class WallPiece : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;

    private bool fellOff;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag($"Jackhammer")) return;
        if (fellOff) return;
        FallOff();
    }

    private void FallOff()
    {
        rigidbody.constraints = RigidbodyConstraints.None;
        rigidbody.AddForce(Vector3.right,ForceMode.Impulse);
        fellOff = true;
    }
}
