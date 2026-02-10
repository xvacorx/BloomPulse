using UnityEngine;

public class GrowthManager : MonoBehaviour
{
    [Header("Stats Actuales")]
    public int flowerLevel = 1;
    public int growthPoints = 0;
    public int naturePoints = 0;

    [Header("Configuración de Balance")]
    public int basePointsForNextLevel = 5;
    public float levelMultiplier = 1.25f;
    public int natureMultiplier = 1; // Mejora que mencionaste

    public int GetPointsRequired() 
    {
        // Fórmula: Base * (1.25 ^ (level-1)) redondeado a int
        return Mathf.RoundToInt(basePointsForNextLevel * Mathf.Pow(levelMultiplier, flowerLevel - 1));
    }

    public void AddGrowth(int amount)
    {
        growthPoints += amount;
        Debug.Log($"<color=green>Crecimiento +{amount}. Total: {growthPoints}/{GetPointsRequired()}</color>");
        
        if (growthPoints >= GetPointsRequired())
        {
            LevelUp();
        }
    }

    public void AddNature()
    {
        // Fórmula: Lvl de flor + Multiplicador
        int gained = flowerLevel + natureMultiplier;
        naturePoints += gained;
        Debug.Log($"<color=yellow>Naturaleza +{gained}. Total: {naturePoints}</color>");
    }

    private void LevelUp()
    {
        growthPoints = 0;
        flowerLevel++;
        Debug.Log($"<color=cyan>¡SUBIDA DE NIVEL! Ahora eres Nivel {flowerLevel}</color>");
        Debug.Log($"Siguiente nivel requiere: {GetPointsRequired()} puntos.");
    }
}