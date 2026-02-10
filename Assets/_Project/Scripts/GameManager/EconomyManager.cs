using UnityEngine;
using System.Collections;

public class EconomyManager : MonoBehaviour
{
    public GrowthManager growthManager;

    [Header("Inventario de Mejoras")]
    public int bees = 0;
    public int flowers = 0;
    public int uvLights = 0;

    [Header("Estado de Amenazas")]
    public bool waspActive = false; // Afecta abejas
    public bool plagueActive = false; // Afecta flores

    void Start() => StartCoroutine(EconomyTick());

    IEnumerator EconomyTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // Un tick por segundo

            // Producción de Naturaleza (Abejas y Flores)
            float beeProd = bees * 1f; // 1 punto base
            if (waspActive) beeProd *= 0.5f;

            float flowerProd = flowers * 1f;
            if (plagueActive) flowerProd *= 0.5f;

            int totalNature = Mathf.RoundToInt(beeProd + flowerProd);
            if (totalNature > 0)
            {
                growthManager.naturePoints += totalNature;
                Debug.Log($"<color=yellow>[Pasivo] Naturaleza +{totalNature} (Abejas: {beeProd}, Flores: {flowerProd})</color>");
            }

            // Producción de Crecimiento (Luces UV)
            int uvProd = uvLights * 1; // 1 punto de crecimiento pasivo
            if (uvProd > 0)
            {
                growthManager.AddGrowth(uvProd);
            }
        }
    }
}