using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour {

    #region Singleton
    public static DialogueManager instance;
    void Awake()
    {
        if (instance)
        {
            Debug.LogWarning("Another instance of Dialogue Manager already exists");
            return;
        }
        instance = this;
    }
    #endregion

    public Animator animator;

    Queue<string> conversationSentences;

    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueSentence;
    public TextMeshProUGUI continueText;

	// Use this for initialization
	void Start () {
        conversationSentences = new Queue<string>();        
	}

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);
        conversationSentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            conversationSentences.Enqueue(sentence);
        }

        characterName.text = dialogue.characterName;
        continueText.text = "Continue >>";
        DisplayNextSentence();        
    }

    public void DisplayNextSentence()
    {
        if (conversationSentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        if (conversationSentences.Count == 1)
            continueText.text = "End";

        string sentence = conversationSentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueSentence.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueSentence.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);
    }
}
