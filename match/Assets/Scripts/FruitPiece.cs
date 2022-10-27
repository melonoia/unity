using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitPiece : MonoBehaviour
{
    public enum FruitType
    {
        BANANA,
        CHERRY,
        STAR,
        ANY,
        COUNT,
    };


    [System.Serializable]
    public struct FruitSprite
    {
        public FruitType fruit;
        public Sprite sprite;
    };

    public FruitSprite[] fruitSprites;

    private FruitType fruit;
    public FruitType Fruit
    {
        get { return fruit; }
        set { SetFruit(value); }
    }

    public int FruitColors
    {

        get { return fruitSprites.Length; }
    }

    private SpriteRenderer fSprite;

    private Dictionary<FruitType, Sprite> fruitSpriteDict;

    void Awake()
    {
        fSprite = transform.Find("fruit").GetComponent<SpriteRenderer>();

        fruitSpriteDict = new Dictionary<FruitType, Sprite>();

        for (int i = 0; i < fruitSprites.Length; i++)
        {
            if (!fruitSpriteDict.ContainsKey(fruitSprites[i].fruit))
            {
                fruitSpriteDict.Add(fruitSprites[i].fruit, fruitSprites[i].sprite);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetFruit(FruitType newFruit)
    {
        fruit = newFruit;

        if (fruitSpriteDict.ContainsKey(newFruit))
        {
            fSprite.sprite = fruitSpriteDict[newFruit];

        }
    }
}
