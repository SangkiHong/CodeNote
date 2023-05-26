using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SK
{
    public class TextureExample : MonoBehaviour
    {
        [SerializeField] private bool autoChangeTexture;
        [SerializeField] private bool autoChangeTexOffset;
        [SerializeField] private bool flowTexture;

        [SerializeField] MeshRenderer targetRenderer;
        [SerializeField] Texture[] textures;

        private Texture2D _textureAtlas;
        private Vector2[] _offsetArray;
        private int _textureIndex;
        private float _timer;

        private void Awake()
        {
            _textureAtlas = Resources.Load<Texture2D>("TextureAtlas/FigureSpr");
        }

        private void Start()
        {
            _offsetArray = new Vector2[10];
            _offsetArray[0] = new Vector2(0, 0);
            _offsetArray[1] = new Vector2(0.25f, 0);
            _offsetArray[2] = new Vector2(0.5f, 0);
            _offsetArray[3] = new Vector2(0.75f, 0);
            _offsetArray[4] = new Vector2(0, 0.25f);
            _offsetArray[5] = new Vector2(0.25f, 0.25f);
            _offsetArray[6] = new Vector2(0.5f, 0.25f);
            _offsetArray[7] = new Vector2(0.75f, 0.25f);
            _offsetArray[8] = new Vector2(0, 0.5f);
            _offsetArray[9] = new Vector2(0.25f, 0.5f);

            targetRenderer.material.mainTexture = _textureAtlas;
            targetRenderer.material.SetTextureOffset("_BaseMap", _offsetArray[0]);
            targetRenderer.material.SetTextureScale("_BaseMap", new Vector2(0.25f, 0.25f));
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                targetRenderer.material.mainTexture = textures[0];
            }
            if (Input.GetMouseButtonDown(1))
            {
                targetRenderer.material.mainTexture = textures[1];
            }

            if (autoChangeTexture || autoChangeTexOffset)
            {
                _timer += Time.deltaTime;

                if (_timer >= 0.3f)
                {
                    _timer = 0;
                    if (autoChangeTexture)
                    {
                        targetRenderer.material.mainTexture = textures[_textureIndex++];

                        if (_textureIndex >= textures.Length)
                            _textureIndex = 0;
                    }

                    if (autoChangeTexOffset)
                    {
                        targetRenderer.material.SetTextureOffset("_BaseMap", _offsetArray[_textureIndex++]);

                        if (_textureIndex >= _offsetArray.Length)
                            _textureIndex = 0;
                    }
                }
            }
        }
    }
}
