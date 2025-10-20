using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Dissolve : MonoBehaviour
{
    [SerializeField] private float _dissolveTime = 0.1f;

    [SerializeField] private SpriteRenderer[] _spriteRenderers;
    [SerializeField] private Material[] _materials;
    [SerializeField] private bool _useDissolve;
    [SerializeField] private bool _useVertical;

    private int _dissolveAmount = Shader.PropertyToID("_DissovleAmount");
    private int _verticalDissolveAmount = Shader.PropertyToID("_VerticalDissolve");

    private float _transparent = 0f;
    private float _appearant = 1.1f;


    private void Start()
    {
        //_spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        _materials = new Material[_spriteRenderers.Length];
        for (int i = 0; i < _spriteRenderers.Length; i++)
        {
            _materials[i] = _spriteRenderers[i].material;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(HandleDissolve());
        }
    }

    private IEnumerator Vanish(float x, float y)
    {
        Debug.Log($"starting Vanish{x}");
        Debug.Log(gameObject.name);
        float elapsedTime = 0f;
        while (elapsedTime < _dissolveTime)
        {
            elapsedTime += Time.deltaTime;
            //float t = Mathf.Clamp01(elapsedTime / _dissolveTime);
            float lerpedDissolve = Mathf.Lerp(x, y, (elapsedTime / _dissolveTime));
            float lerpedVerticalDissolve = Mathf.Lerp(x, y, (elapsedTime / _dissolveTime));
            
            // --- instant transitions ---
            // If going from transparent - apparent (x < y)
            if (x < y && lerpedDissolve < 0.2f)
            {
                lerpedDissolve = 0.2f; // jump instantly to 0.2
            }
            // If going from apparent - transparent (x > y)
            if (x > y && lerpedDissolve < 0.2f)
            {
                lerpedDissolve = 0f; // jump instantly to 0
            }

            if (x < y && lerpedVerticalDissolve < 0.1f)
            {
                lerpedVerticalDissolve = 0.1f;
            }
            if (x > y && lerpedVerticalDissolve < 0.1f)
            {
                lerpedVerticalDissolve = 0f;
            }

            for (int i = 0; i < _materials.Length; i++)
            {
                if (_useDissolve)
                {
                    _materials[i].SetFloat(_dissolveAmount, lerpedDissolve);
                }
                if (_useVertical)
                {
                    _materials[i].SetFloat(_verticalDissolveAmount, lerpedVerticalDissolve);
                }
            }
            yield return null;
        }
        Debug.Log($"ending Vanish{y}");
    }

    protected IEnumerator HandleDissolve()
    {
        //foreach (var sr in _spriteRenderers)
        //{
        //    sr.sortingLayerName = "Decor";
        //}

        yield return StartCoroutine(Vanish(_transparent, _appearant));
        //yield return new WaitForSeconds(0.3f);
        yield return StartCoroutine(Vanish(_appearant, _transparent));

        //foreach (var sr in _spriteRenderers)
        //{
        //    sr.sortingLayerName = "Player";
        //}
    }
}
