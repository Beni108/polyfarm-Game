using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseSprite, _offsetSprite;
    [SerializeField] private SpriteRenderer _render;

    public void Init(bool offset)
    {
        _render.color = offset ? _baseSprite:_offsetSprite;
    }
}
