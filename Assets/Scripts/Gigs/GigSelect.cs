using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GigSelect : MonoBehaviour
{
    [SerializeField] private List<GigDataSO> gigOptions = new();
    [SerializeField] private List<GigButton> gigButtons = new();

    [SerializeField] private float fadeDuration = 0.5f;

    private CanvasFader canvasFader;
    private CanvasGroup canvasGroup;

    void OnEnable()
    {
        GigEventBus.OnGigComplete += InitGigSelection;
    }

    void OnDisable()
    {
        GigEventBus.OnGigComplete -= InitGigSelection;
    }

    void Awake()
    {
        canvasFader = GetComponent<CanvasFader>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        InitGigSelection();
    }

    private void InitGigSelection()
    {
        InitialiseButtons();
        StartCoroutine(canvasFader.Fade(0, 1, fadeDuration));
        canvasGroup.interactable = true;
    }

    private void InitialiseButtons()
    {
        for (int i = 0; i < gigButtons.Count; i++)
        {
            gigButtons[i].InitialiseButton(this, gigOptions[i]);
        }
    }

    public void StartSelectGig(int index)
    {
        StartCoroutine(SelectGig(index));
    }

    private IEnumerator SelectGig(int index)
    {
        canvasGroup.interactable = false;
        yield return canvasFader.Fade(1.0f, 0.0f, fadeDuration);
        GigEventBus.RaiseGigSelected(gigOptions[index]);
    }
}
