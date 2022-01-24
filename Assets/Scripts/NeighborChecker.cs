using System;
using UnityEngine;

public class NeighborChecker : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(""))
        {
            throw new NotImplementedException();
        }
    }
}
