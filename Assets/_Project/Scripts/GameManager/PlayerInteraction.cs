using UnityEngine;
using System.Collections;

public class PlayerInteraction : MonoBehaviour
{
    public GrowthManager growthManager;
    public float waterCooldown = 3.0f;
    public LayerMask interactableLayer;

    private bool canWater = true;
    private Camera mainCam;

    void Awake() => mainCam = Camera.main;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canWater)
        {
            ProcessClick();
        }
    }

    void ProcessClick()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, interactableLayer))
        {
            StartCoroutine(WaterRoutine());
        }
    }

    IEnumerator WaterRoutine()
    {
        canWater = false;
        Debug.Log("<color=blue>Regando planta...</color>");

        growthManager.AddGrowth(1);
        growthManager.AddNature();

        yield return new WaitForSeconds(waterCooldown);
        canWater = true;
        Debug.Log("Regadera lista.");
    }
}