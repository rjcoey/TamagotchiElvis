using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GigSelect : MonoBehaviour
{
    [SerializeField] private GameFlagsSO gameFlags;

    [SerializeField] private List<GigDataSO> gigOptions = new();
    [SerializeField] private List<GigButton> gigButtons = new();

    [SerializeField] private float fadeDuration = 0.5f;

    private CanvasFader canvasFader;

    void OnEnable()
    {
        GigEventBus.OnStartGigSelection += RunGigSelection;
    }

    void OnDisable()
    {
        GigEventBus.OnStartGigSelection -= RunGigSelection;
    }

    void Awake()
    {
        canvasFader = GetComponent<CanvasFader>();
    }

    private void RunGigSelection()
    {
        StartCoroutine(Co_GigSelection());
    }

    private IEnumerator Co_GigSelection()
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

    public void RunSelectGig(int index)
    {
        StartCoroutine(Co_SelectGig(index));
    }

    private IEnumerator Co_SelectGig(int index)
    {
        yield return canvasFader.Co_FadeOut(fadeDuration);
        GigEventBus.RaiseGigSelected(gigOptions[index]);
    }
}
