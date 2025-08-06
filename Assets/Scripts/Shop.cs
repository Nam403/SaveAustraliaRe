using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] TowerPrint redTower;
    [SerializeField] TowerPrint blueTower;

    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectseRedTower()
    {
        Debug.Log("Buy red tower");
        buildManager.SelectTowerToBuild(redTower);
    }

    public void SelectBlueTower()
    {
        Debug.Log("Buy blue tower");
        buildManager.SelectTowerToBuild(blueTower);
    }
}
