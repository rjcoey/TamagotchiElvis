using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GigSelect : MonoBehaviour
{
    [SerializeField] private List<GigDataSO> gigOptions = new();
    [SerializeField] private List<GigButton> gigButtons = new();

    [SerializeField] private float fadeDuration = 0.5f;

    private CanvasFader canvasFader;

    void OnEnable()
    {
        GigEventBus.OnGigComplete += RunInitGigSelection;
    }

    void OnDisable()
    {
        GigEventBus.OnGigComplete -= RunInitGigSelection;
    }

    void Awake()
    {
        canvasFader = GetComponent<CanvasFader>();
    }

    IEnumerator Start()
    {
        yield return InitGigSelection();
    }

    private void RunInitGigSelection()
    {
        StartCoroutine(InitGigSelection());
    }

    private IEnumerator InitGigSelection()
    {
        InitialiseButtons();
        yield return canvasFader.Co_FadeIn(fadeDuration);
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
        yield return canvasFader.Co_FadeOut(fadeDuration);
        GigEventBus.RaiseGigSelected(gigOptions[index]);
    }
}
