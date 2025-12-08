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
        GameEventBus.OnStartGame += RunInitGigSelection;
        GigEventBus.OnGigComplete += RunInitGigSelection;
    }

    void OnDisable()
    {
        GameEventBus.OnStartGame -= RunInitGigSelection;
        GigEventBus.OnGigComplete -= RunInitGigSelection;
    }

    void Awake()
    {
        canvasFader = GetComponent<CanvasFader>();
    }

    void Start()
    {
        if (gameFlags.HasPlayedTutorial)
        {
            RunInitGigSelection();
        }
        else
        {
            TutorialEventBus.RaiseStartTutorial();
        }
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
