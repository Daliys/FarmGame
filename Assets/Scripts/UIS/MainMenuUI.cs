using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Sprite[] loadingImg;
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private Image loadingImage;
    public void OnButtonPlayClicked()
    {
        StartCoroutine(LoadSceneAsync());
    }


    IEnumerator LoadSceneAsync()
    {
        loadingPanel.SetActive(true);
        loadingImage.sprite = loadingImg[Random.Range(0, loadingImg.Length)];
        
        SceneManager.LoadSceneAsync(1);
        yield return null;
    }
}
