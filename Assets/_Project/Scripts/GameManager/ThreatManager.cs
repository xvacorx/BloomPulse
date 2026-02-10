using UnityEngine;

public class ThreatManager : MonoBehaviour
{
    public EconomyManager economy;
    public LayerMask interactableLayer;

    [Header("Configuración de Amenaza")]
    public GameObject waspPrefab; // Arrastra una esfera aquí
    public Transform spawnArea;   // Un punto cerca de la flor

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 10f) // Check cada 10 segundos como pediste
        {
            CheckThreats();
            timer = 0f;
        }
    }

    void CheckThreats()
    {
        // Cálculo Avispas: (Cant. Abejas * 0.5%) + Prob Base (ej. 5%)
        float waspChance = (economy.bees * 0.5f) / 100f;

        if (!economy.waspActive && Random.value < waspChance)
        {
            SpawnWasp();
        }
    }

    void SpawnWasp()
    {
        economy.waspActive = true;
        Vector3 randomPos = spawnArea.position + Random.insideUnitSphere * 3f;
        GameObject wasp = Instantiate(waspPrefab, randomPos, Quaternion.identity);
        wasp.name = "Wasp_Threat";
        Debug.Log("<color=red>[ALERTA] ¡Ha aparecido una Avispa! Producción de abejas al 50%.</color>");
    }
}