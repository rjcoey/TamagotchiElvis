using System.Collections;
using TMPro;
using UnityEngine;

public static class Typewriter
{
    public static IEnumerator TypewriterEffect(TextMeshProUGUI textElement, string textToType, float typewriterSpeed = 0.02f)
    {
        textElement.text = textToType;
        textElement.maxVisibleCharacters = 0;
        foreach (char c in textToType)
        {
            textElement.maxVisibleCharacters += 1;
            yield return new WaitForSeconds(typewriterSpeed);
        }
        // Ensure the full text is displayed at the end.
        textElement.text = textToType;
    }
}
