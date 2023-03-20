using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemIconScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image statsImage;
    private Image itemIconImage;

    private void Start()
    {
        statsImage = transform.Find("Stats").GetComponent<Image>();
        statsImage.gameObject.SetActive(false);

        itemIconImage = GetComponent<Image>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemIconImage.color.a > 0.8f)
        {
            statsImage.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        statsImage.gameObject.SetActive(false);
    }
}
