using UnityEngine;

public class Shop : MonoBehaviour
{
    public TowerPrint redTower;
    public TowerPrint blueTower;

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
