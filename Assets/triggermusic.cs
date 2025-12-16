using UnityEngine;

public class TriggerActivator : MonoBehaviour
{
    public MonoBehaviour targetComponent;

    void Start()
    {
        if (targetComponent != null)
        {
            targetComponent.enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && targetComponent != null)
        {
            targetComponent.enabled = true;
            Debug.Log("Player entered, component activated.");
        }
    }
    private void OnTriggerStay(Collider other)
    {
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && targetComponent != null)
        {
            targetComponent.enabled = false;
            Debug.Log("Player left, component deactivated.");
        }
    }
}
