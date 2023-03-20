using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AchviementsManager : MonoBehaviour
{
    private TextMeshProUGUI itensLeftToUnlockText;
    private int itensLeftToUnlockIntMax = 0;
    private int itensLeftToUnlockInt = 0;

    private Dropper dropper;

    private TextMeshProUGUI commonRarityText;
    private TextMeshProUGUI rareRarityText;
    private TextMeshProUGUI epicRarityText;

    private void Awake(){
        Transform achviementsTransform = transform.parent;

        itensLeftToUnlockText = achviementsTransform.Find("ItensLeft").GetComponent<TextMeshProUGUI>();
        
        Transform GroupOfItensTransform = achviementsTransform.Find("GroupItem");

        foreach(Transform transform in GroupOfItensTransform.GetComponentsInChildren<Transform>()){
            if(transform.name == "Item"){
                itensLeftToUnlockIntMax++;
            }
        }

        itensLeftToUnlockText.text = "0/" + itensLeftToUnlockIntMax.ToString();

        dropper = GameObject.Find("Dropper").GetComponent<Dropper>();

        Transform rarityListTransform = achviementsTransform.Find("RarityChances/RarityList");

        commonRarityText = rarityListTransform.Find("Common/TextTMP").GetComponent<TextMeshProUGUI>();
        rareRarityText = rarityListTransform.Find("Rare/TextTMP").GetComponent<TextMeshProUGUI>();
        epicRarityText = rarityListTransform.Find("Epic/TextTMP").GetComponent<TextMeshProUGUI>();
    }
    private void Start(){
        UpdateRarityList();
    }
    public void UpdateItensLeftToUnlock(){
        itensLeftToUnlockInt++;
        itensLeftToUnlockText.text = itensLeftToUnlockInt.ToString()+
        "/" + itensLeftToUnlockIntMax.ToString();
    }
    private void UpdateRarityList(){
        commonRarityText.text = "Common " + dropper.commonRarity.ToString() + "%";
        rareRarityText.text = "Rare " + dropper.rareRarity.ToString() + "%";
        epicRarityText.text = "Epic " + dropper.epicRarity.ToString() + "%";
    }
}
