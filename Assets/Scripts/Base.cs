using Unity.Jobs;
using UnityEngine;
using UnityEngine.EventSystems;

public class Base : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject tower;
    [HideInInspector]
    public TowerPrint towerPrint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if(tower != null)
        {
            buildManager.SelectBase(this);
            return;
        }

        if (!buildManager.CanBuild())
        {
            return;
        }

        //Build a tower
        BuildTower(buildManager.GetTowerToBuild());
        //buildManager.BuildTowerOn (this);
    }

    void BuildTower(TowerPrint _towerPrint)
    {
        if (PlayerStats.Money < _towerPrint.cost)
        {
            Debug.Log("Not enough money to build!");
            return;
        }

        PlayerStats.Money -= _towerPrint.cost;

        GameObject _tower = (GameObject)Instantiate(_towerPrint.prefab, GetBuildPosition(), Quaternion.identity);
        tower = _tower;

        towerPrint = _towerPrint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 2);

        Debug.Log("Tower build! Money left: " + PlayerStats.Money.ToString());
    }

    public void UpgradeTower()
    {
        if (PlayerStats.Money < towerPrint.upgradeCost)
        {
            Debug.Log("Not enough money to upgrade!");
            return;
        }

        PlayerStats.Money -= towerPrint.upgradeCost;

        //Get rid of the old tower
        Destroy(tower);

        //Build a new one

        GameObject _tower = (GameObject)Instantiate(towerPrint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        tower = _tower;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 2);

        isUpgraded = true;

        Debug.Log("Tower upgrade! Money left: " + PlayerStats.Money.ToString());
    }

    public void SellTower()
    {
        PlayerStats.Money += towerPrint.GetSellAmount();

        // Spawn a effect
        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 2);

        Destroy(tower);
        towerPrint = null;
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.CanBuild())
        {
            return;
        }

        if (buildManager.HasMoney())
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
