using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    [SerializeField]
    private AnimalScriptableObject[] allAnimals;
    [SerializeField]
    private bool[] AnimalInLevel;
    private Transform animalContainer;
    private Transform animalSlotTemplate;

    private void Awake()
    {
        animalContainer = transform.Find("animalContainer");
        animalSlotTemplate = animalContainer.Find("animalSlotTemplate");
        Debug.Log("animals "+AnimalInLevel[0]+ AnimalInLevel[1]+ AnimalInLevel[2]);
        RefreshAnimals();
    }

    public void RefreshAnimals()
    {
        int x = 0;
        int y = 0;
        float animalSlotCellSize =80f;
        for (int i=0; i<AnimalInLevel.Length; i++)
        {
            if (AnimalInLevel[i])
            {
                RectTransform animalSlotRectTransform = Instantiate(animalSlotTemplate, animalContainer).GetComponent<RectTransform>();
                animalSlotRectTransform.gameObject.SetActive(true);
                animalSlotRectTransform.anchoredPosition = new Vector2(x * animalSlotCellSize, -y * animalSlotCellSize);
                y++;
            }
            Debug.Log(y);
        }
    }
}
