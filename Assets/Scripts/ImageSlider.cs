using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ImageSlider : MonoBehaviour
{
    [SerializeField] private Image[] _Images;
    [SerializeField] private float _delay = 3f; // seconds between images
    [SerializeField] private string _nextSceneName;

    private void Start()
    {
        StartCoroutine(ShowImages());
    }

    private IEnumerator ShowImages()
    {
        foreach (var img in _Images)
            img.gameObject.SetActive(false);

        for (int i = 0; i < _Images.Length; i++)
        {
            _Images[i].gameObject.SetActive(true);
            Debug.Log($"Showing image {i + 1}");

            // Wait before hiding it (except for the last one)
            if (i < _Images.Length - 1)
            {
                yield return new WaitForSeconds(_delay);
                _Images[i].gameObject.SetActive(false);
            }
        }
        //// The last image stays active
        yield return new WaitForSeconds(_delay + 2f);

        if (!string.IsNullOrEmpty(_nextSceneName))
        {
            SceneManager.LoadScene(_nextSceneName); //Missing scene so it's commented for now.
        }
        else
        {
            Debug.LogError("Next scene name not set in Inspector!");
        }
    }
}
