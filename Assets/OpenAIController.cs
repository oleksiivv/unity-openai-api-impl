using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;

public class OpenAIController : MonoBehaviour
{
    private OpenAIApi openAIApi;

    private List<ChatMessage> messages = new List<ChatMessage>();

    // Start is called before the first frame update
    void Start()
    {
        openAIApi = new OpenAIApi();

    }

    public async void AskAI(string text){
        ChatMessage newMessage = new ChatMessage();
        newMessage.Content = text;
        newMessage.Role = "user";

        messages.Add(newMessage);

        CreateChatCompletionRequest request = new CreateChatCompletionRequest();

        request.Messages = messages;
        request.Model = "gpt-3.5-turbo-1106";

        var response = await openAIApi.CreateChatCompletion(request);

        if (response.Choices != null && response.Choices.Count > 0){
            var chatResponse = response.Choices[0].Message;
            messages.Add(chatResponse);

            Debug.Log(chatResponse.Content);
        } else {
            Debug.LogWarning("No text was generated from this prompt.");
        }
    }
}
