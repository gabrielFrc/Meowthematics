using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GenerateImages : MonoBehaviour
{
    [SerializeField] private Sprite[] images;
    private Image thisImage;

    private int currentImageIndex = 0;

    private Color imageColor;
    private float alpha;

    private void Start()
    {
        thisImage = GetComponent<Image>();

        imageColor = thisImage.color;
    }

    public IEnumerator Generate(){
        yield return new WaitForSeconds(0.1f);

        if(currentImageIndex==images.Count()-1){
            currentImageIndex = 0;
        }

        thisImage.sprite = images[currentImageIndex];
        currentImageIndex++;

        while(true){
            alpha += 0.05f;
            thisImage.color = new Color(imageColor.r,
             imageColor.g,
              imageColor.b,
               alpha);
            yield return new WaitForSeconds(0.01f);
            if(alpha>0.98f){
                break;
            }
        }

        yield return new WaitForSeconds(5.5f);

        while(true){
            alpha -= 0.05f;
            thisImage.color = new Color(imageColor.r,
             imageColor.g,
              imageColor.b,
               alpha);
            yield return new WaitForSeconds(0.01f);
            if(alpha<0.02f){
                break;
            }
        }

        yield return new WaitForSeconds(1f);
        
        if(thisImage.IsActive())
            StartCoroutine(Generate());
    }
}
