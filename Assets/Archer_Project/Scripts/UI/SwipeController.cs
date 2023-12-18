using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeController : MonoBehaviour
{
    [SerializeField] int maxPage;
    
    int currentPage;
    
    Vector3 targetPos;
    
    [SerializeField] Vector3 pageStep;
    
    [SerializeField] RectTransform levelPagesRect;

    [SerializeField] ScrollRect scrollRect;

    [SerializeField] Button nextButton;

    [SerializeField] Button prevButton;

    private void OnEnable()
    {
        nextButton.onClick.AddListener(Next);
        prevButton.onClick.AddListener(Previous);
    }

    private void OnDisable()
    {
        nextButton.onClick.RemoveListener(Next);
        prevButton.onClick.RemoveListener(Previous);
    }

    private void Awake()
    {       
        currentPage = 1;
    }    

    public void Next()
    {
        scrollRect.normalizedPosition = Vector2.right;

        if (currentPage < maxPage)
        {
            currentPage++;
            targetPos += pageStep;
            MovePage();
        }
    }

    public void Previous()
    {
        scrollRect.normalizedPosition = Vector2.zero;

        if (currentPage > maxPage)
        {
            currentPage--;
            targetPos -= pageStep;
            MovePage();
        }

    }

    void MovePage()
    {
        
    }
}
