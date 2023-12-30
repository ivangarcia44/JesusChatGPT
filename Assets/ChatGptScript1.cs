using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using OpenAI;
using UnityEngine.Events;

public class ChatGptScript1 : MonoBehaviour
{
    [System.Serializable]
    public class OnResponseEvent : UnityEvent<string> {}

    private OpenAIApi openAI;

    private List<ChatMessage> messages = new List<ChatMessage>();

    public OnResponseEvent OnResponse;

    // [SerializeField] public Button fRecordButton;
    // private string fMicText;
    // private string fTempFileName = "output.wav";

    // [SerializeField] private readonly int fDuration = 5;

    // private float fTime;
    // private bool fIsRecording;
    // private AudioClip fClip;
    // private string fMicOption;

    void Awake() {
        string openAiKey = System.Environment.GetEnvironmentVariable("OPENAI_API_KEY");
        string openAiOrg = System.Environment.GetEnvironmentVariable("OPENAI_ORG");
        openAI = new OpenAIApi(openAiKey, openAiOrg);
    }

    public async void AskChatGPT(string newText) {
        ChatMessage systemRole = new ChatMessage();
        systemRole.Content = "Chatbot acts like Jesus of Nazareth.";
        systemRole.Role = "system";
        messages.Add(systemRole);
        ChatMessage newMessage = new ChatMessage();
        newMessage.Content = newText;
        newMessage.Role = "user";
        messages.Add(newMessage);

        CreateChatCompletionRequest request = new CreateChatCompletionRequest();
        request.Messages = messages;        
        request.Model = "ft:gpt-3.5-turbo-0613:personal::8Eg09Cm3";
        // request.Model = "gpt-3.5-turbo";

        var response = await openAI.CreateChatCompletion(request);

        if (response.Choices != null && response.Choices.Count > 0) {
            var chatResponse = response.Choices[0].Message;
            messages.Add(chatResponse);

            OnResponse.Invoke(chatResponse.Content);

            Debug.Log(chatResponse.Content);
        }
    }

    // Start is called before the first frame update
    // void Start()
    // {
    //     foreach (var device in Microphone.devices) {
    //         fMicOption = device;
    //         Debug.Log(device);
    //         break;
    //     }
    //     fRecordButton.onClick.AddListener(StartRecording);
    // }

    // private void StartRecording() {
    //     fIsRecording = true;
    //     fRecordButton.enabled = false;
    //     Debug.Log("Start recording");

    //     fClip = Microphone.Start(fMicOption, false, fDuration, 44100);
    // }

    // private async void EndRecording() {
    //     Debug.Log("End recording");
    //     fMicText = "Transcripting...";

    //     fIsRecording = false;
    //     Microphone.End(null);

    //     byte[] data = SavWav.Save(fTempFileName, fClip);

    //     var req = new CreateAudioTranscriptionsRequest {
    //         FileData = new FileData() {Data = data, Name = "audio.wav"},
    //         // File = Application.persistentDataPath + "/" + fileName,
    //         Model = "whisper-1",
    //         Language = "en"
    //     };
    //     var res = await openAI.CreateAudioTranscription(req);
    //     fMicText = res.Text;
    //     Debug.Log(fMicText);
    // }

    // Update is called once per frame
    // void Update()
    // {
    //     if (fIsRecording) {
    //         fTime += Time.deltaTime;

    //         if (fTime >= fDuration) {
    //             fIsRecording = false;
    //             EndRecording();
    //         }
    //     }
    // }
}
