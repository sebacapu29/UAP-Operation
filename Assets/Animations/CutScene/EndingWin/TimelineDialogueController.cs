using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class TimelineDialogueController : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public float typeSpeed = 0.05f;

    private Coroutine typingCoroutine;

    void Start()
    {
        AudioManager.Instance.Play("EndingTheme");
    }
    public void ShowDialogue1()
    {
        StartTyping("Agente A7N: ...Al fin lo encuentro....");
    }

    public void ShowDialogue2()
    {
        StartTyping(
            "Voz femenina: ...Amor, te falta mucho?\n" +
            "Agente A7N: ...Estoy regresando, ya encontré el juguete de Nurika\n" +
            "Voz de niña: ...Siiii!!");
    }

    public void ShowDialogue3()
    {
        StartTyping(
            "Agente A7N: ..Teletransportación a casa..\n" +
            "Agente A7N: ..Lo que hace un padre por su hija..");
    }

    private void StartTyping(string message)
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(message));
    }

    public void LoadMenu()
    {
        // Ir a los creditos
        SceneManager.LoadScene("Credits");
        //Debug.Log("Voy a creditos");
 
    }

    private IEnumerator TypeText(string message)
    {
        dialogueText.text = "";
        foreach (char c in message)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }

        typingCoroutine = null;
    }
}
