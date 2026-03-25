using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(UIMover))]
public class TextBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private float revealTime = 1.0f;

    private RectTransform rectTransform;
    private UIMover textBoxPanel;
    private Coroutine typingCoroutine;

    public bool IsTyping { get; private set; }


    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        textBoxPanel = GetComponent<UIMover>();
    }

    void Start()
    {
        ResetText();
    }

    public void ResetText()
    {
        textBox.text = "";
    }

    public void StartTyping(string text)
    {
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(Co_TypeText(text));
    }

    public void SkipTypewriter()
    {
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        textBox.maxVisibleCharacters = int.MaxValue;
        IsTyping = false;
    }

    public IEnumerator Co_TypeText(string text)
    {
        IsTyping = true;
        yield return Typewriter.TypewriterEffect(textBox, text);
        IsTyping = false;
    }

    public IEnumerator Reveal()
    {
        Vector2 endPosition = new(rectTransform.anchoredPosition.x, 0.0f);
        yield return textBoxPanel.LerpPanelPosition(rectTransform.anchoredPosition, endPosition, revealTime);
    }

    public void HideImmediate()
    {
        StopAllCoroutines();
        IsTyping = false;

        Vector2 endPosition = new(rectTransform.anchoredPosition.x, rectTransform.sizeDelta.y);
        rectTransform.anchoredPosition = endPosition;
    }

    public IEnumerator Hide()
    {
        Vector2 endPosition = new(rectTransform.anchoredPosition.x, rectTransform.sizeDelta.y);
        yield return textBoxPanel.LerpPanelPosition(rectTransform.anchoredPosition, endPosition, revealTime);
    }
}
