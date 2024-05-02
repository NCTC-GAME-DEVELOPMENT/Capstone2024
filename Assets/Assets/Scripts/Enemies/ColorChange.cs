using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public GameObject[] objectsToChange;
    public Material[] originalMaterials;

    public void ChangeToRed()
    {
        for (int i = 0; i < objectsToChange.Length; i++)
        {
            Renderer renderer = objectsToChange[i].GetComponent<Renderer>();

            if (renderer != null)
            {
                originalMaterials[i] = renderer.material;

                Material redMaterial = new Material(Shader.Find("Standard"));
                redMaterial.color = Color.red;

                renderer.material = redMaterial;
            }
        }
        StartCoroutine(Delay(0.15f));
    }
    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ChangeToOriginal();
    }

    public void ChangeToOriginal()
    {
        for (int i = 0; i < objectsToChange.Length; i++)
        {
            Renderer renderer = objectsToChange[i].GetComponent<Renderer>();

            if (renderer != null && originalMaterials[i] != null)
            {
                renderer.material = originalMaterials[i];
            }
        }
    }
}