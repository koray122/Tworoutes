using UnityEngine;

[System.Serializable]
public class DialogueChoice
{
    [TextArea] public string choiceText;     // Oyuncunun göreceği seçim yazısı
    [TextArea] public string responseText;   // NPC'nin cevabı

    public DialogueChoice[] nextChoices;     // Bu cevaptan sonra çıkacak yeni seçimler
}
