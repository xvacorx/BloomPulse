using UnityEngine;

public class Threat : MonoBehaviour
{
    public enum ThreatType { Wasp, Plague }
    public ThreatType type;

    private void OnMouseDown()
    {
        EconomyManager eco = FindObjectOfType<EconomyManager>();
        if (type == ThreatType.Wasp) eco.waspActive = false;

        Debug.Log($"<color=green>[LIMPIEZA] {type} eliminada. Producción normalizada.</color>");
        Destroy(gameObject);
    }
}