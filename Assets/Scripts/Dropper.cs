using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;

public class Dropper : MonoBehaviour
{
    [Header("Itens & Rarity")]
    [SerializeField] private List<Itens> commonItens = new List<Itens>();
    [SerializeField] private List<Itens> rareItens = new List<Itens>();
    [SerializeField] private List<Itens> epicItens = new List<Itens>();

    private const int commonRarityMax = 60;
    [HideInInspector] public float commonRarity = 60f;
    private const int rareRarityMax = 90;
    [HideInInspector] public float rareRarity = 30f;
    private const int epicRarityMax = 100;
    [HideInInspector] public float epicRarity = 10f;

    private List<Itens>[] listOfItens = new List<Itens>[3];

    [Header("Itens On GameScreen")]
    [SerializeField] private List<ItemScript> item = new List<ItemScript>();

    [SerializeField] private GameObject[] itensOnAchviements;

    private AchviementsManager achviementsManager;

    private const float MIN_DROP_STRENGTH = 8f;
    private const float MAX_DROP_STRENGTH = 9f;

    private Animator boxAnim;
    private AudioSource animAudio;
    [SerializeField] private AudioSource dropAudio;

    private void Start()
    {
        listOfItens[0] = commonItens;
        listOfItens[1] = rareItens;
        listOfItens[2] = epicItens;

        boxAnim = GameObject.Find("Box").GetComponent<Animator>();

        animAudio = GetComponent<AudioSource>();

        achviementsManager = GameObject.Find("Achviements/AchviementsManager").GetComponent<AchviementsManager>();

        foreach(GameObject gameObj in itensOnAchviements)
        {
            gameObj.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) {
            OnCorrectAnswer(5);
        }
    }

    public void OnCorrectAnswer(int dropQuantity)
    {
        animAudio.Play();
        boxAnim.Play("DroppingAnItem");
        StartCoroutine(waitForEndAnim(dropQuantity));
    }
    private void DropRandomItem(int dropsQuantity)
    {
        dropAudio.Play();
        for (int i = 0; i < dropsQuantity; i++)
        {   
            if (item.Count > i)
            {
                ItemScript itemScript = item[i];
                
                itemScript.CancelInvoke();
                itemScript.ResetItemPosition();

                int randomIndex = Random.Range(0, 100);

                Itens itens = RandomItem(randomIndex);

                itemScript.itemSprite.sprite = itens.itemSprite;
                itemScript.itemSprite.size = new Vector2(1, 1);

                itemScript.itemRB.gravityScale = 1;
                itemScript.itemRB.velocity = Vector2.zero;

                float left_random = Random.Range(-4f, -3.5f);
                float right_random = Random.Range(3.5f, 4f);
                float[] values = new float[2];
                values[0] = left_random;
                values[1] = right_random;

                itemScript.itemRB.AddForce(Vector2.up * Random.Range(MIN_DROP_STRENGTH, MAX_DROP_STRENGTH) + Vector2.right *
                    values[Random.Range(0, 2)],
                    ForceMode2D.Impulse);

                StartCoroutine(ChangeSorting(itemScript: itemScript));

                ChangeItemIconInAchviements(randomIndex: randomIndex, itens: itens);
            }
        }
    }
    private Itens RandomItem(int randomIndex)
    {
        // The last item in inspector always need to be 100% to not give error.
        
        // common 60% first item 15%
        // common 60% second item 45-15=30%
        // common 60% third item 100-45=55%

        int randomRarity = Random.Range(0, 100);

        if (randomIndex <= commonRarityMax)
        {
            foreach (Itens item in listOfItens[0])
            {
                if (item.itemRarity >= randomRarity)
                {
                    item.rarity = "Common";
                    return item;
                }
            }
        }
        else if (randomIndex <= rareRarityMax)
        {
            foreach (Itens item in listOfItens[1])
            {
                if (item.itemRarity >= randomRarity)
                {
                    item.rarity = "Rare";
                    return item;
                }
            }
        }
        else
        {
            foreach (Itens item in listOfItens[2])
            {
                if (item.itemRarity >= randomRarity)
                {
                    item.rarity = "Epic";
                    return item;
                }
            }
        }
        return null;
    }
    private void ChangeItemIconInAchviements(int randomIndex, Itens itens)
    {
        bool alreadyHaveThis = false;
        GameObject theItemToChange = null;

        foreach (GameObject gameObj in itensOnAchviements)
        {
            if (itens.itemName == gameObj.name)
            {
                theItemToChange = gameObj;
                alreadyHaveThis = true;
                break;
            }
            if (gameObj.name == "Item")
            {
                Color transparent = gameObj.GetComponent<Image>().color;
                transparent = new Color(transparent.r, transparent.g, transparent.b, 0);
                gameObj.GetComponent<Image>().color = transparent;
            }
        }

        float currentItemRarity = CalculateCurrentItemRarity(item: itens); 

        if (!alreadyHaveThis)
        {
            achviementsManager.UpdateItensLeftToUnlock();

            foreach (GameObject gameObj in itensOnAchviements)
            {
                if (gameObj.name == "Item")
                {
                    Color transparent = RarityColor(item: itens);
                    transparent = new Color(transparent.r, transparent.g, transparent.b, 1);
                    gameObj.GetComponent<Image>().color = transparent;

                    Image itemSprite = gameObj.transform.Find("ItemSprite").GetComponent<Image>();

                    Color itemBackgroundColor = itemSprite.color;
                    itemBackgroundColor = new Color(r: itemBackgroundColor.r,
                     g: itemBackgroundColor.g,
                     b: itemBackgroundColor.b,
                     a: 1f);

                    itemSprite.color = itemBackgroundColor;
                    itemSprite.sprite = itens.itemSprite;

                    gameObj.name = itens.itemName;

                    gameObj.transform.Find("Stats").GetChild(0).GetComponent<TextMeshProUGUI>().
                    text = itens.itemName + "\n Dropped: x" +
                    itens.itemQuantity + "\n" +
                    itens.description + "\n Item chances: " +
                    currentItemRarity + "%";

                    break;
                }
            }
        }
        else
        {
            itens.itemQuantity =
                        itens.itemQuantity + 1;
            theItemToChange.transform.Find("Stats").GetChild(0).GetComponent<TextMeshProUGUI>().
                text = itens.itemName + "\n Dropped: x" +
                    itens.itemQuantity + "\n" + itens.
                    description + "\n Item chances: " +
                    currentItemRarity + "%";
        }
    }
    private Color RarityColor(Itens item){
        Color color = Color.black;
        switch(item.rarity){
            case "Common":
                if ( ColorUtility.TryParseHtmlString("#09FF0064", out color)){ 
                    return color;
                }
                break;
            case "Rare":
                if ( ColorUtility.TryParseHtmlString("#00E9FF", color: out color)){
                    return color;
                }
                break;
            case "Epic":
                if ( ColorUtility.TryParseHtmlString("#9C67E5", color: out color)){
                    return color;
                }
                break;
        }
        return color;
    }
    private float CalculateCurrentItemRarity(Itens item){
        switch (item.rarity)
        {
            case "Common":
                float commonRarity = commonRarityMax;
                for(int i = 0; i<commonItens.Count; i++){
                    if(commonItens[i] == item && i != 0){
                        return commonRarity * (item.itemRarity - commonItens[i-1].itemRarity)/100;
                    }
                    else if(commonItens[i] == item){
                        return commonRarity * item.itemRarity/100;
                    }
                }
                return 999f;
            case "Rare":
                float rareRarity = rareRarityMax - commonRarityMax;
                for(int i = 0; i<rareItens.Count; i++){
                    if(rareItens[i] == item && i != 0){
                        return rareRarity * (item.itemRarity - rareItens[i-1].itemRarity)/100;
                    }
                    else if(rareItens[i] == item){
                        return rareRarity * item.itemRarity/100;
                    }
                }
                return 999f;
            case "Epic":
                float epicRarity = epicRarityMax - rareRarityMax;
                for(int i = 0; i<epicItens.Count; i++){
                    if(epicItens[i] == item && i != 0){
                        return epicRarity * (item.itemRarity - epicItens[i-1].itemRarity)/100;
                    }
                    else if(epicItens[i] == item){
                        return epicRarity * item.itemRarity/100;
                    }
                }
                return 999f;
            default:
                return 2109419204f;
        }
    }

    IEnumerator ChangeSorting(ItemScript itemScript)
    {
        yield return new WaitForSeconds(1f);
        itemScript.itemSprite.sortingOrder = 4;
    }
    private IEnumerator waitForEndAnim(int dropQuantity)
    {
        yield return new WaitForSeconds(0.1f);

        bool repeat = isPlaying(boxAnim, "DroppingAnItem");

        if (repeat)
        {
            StartCoroutine(waitForEndAnim(dropQuantity));
        }
        else
        {
            DropRandomItem(dropQuantity);
        }
    }
    bool isPlaying(Animator anim, string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName))
            return true;
        else
            return false;
    }
}
[System.Serializable]
public class Itens
{
    public string itemName;
    public Sprite itemSprite;
    public string description;
    [Range(0, 100)]
    public float itemRarity;
    [HideInInspector] public string rarity;
    [HideInInspector] public int itemQuantity = 1;
}
