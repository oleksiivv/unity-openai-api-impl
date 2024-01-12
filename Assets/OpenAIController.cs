using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OpenAI;

public class OpenAIController : MonoBehaviour
{
    private OpenAIApi openAIApi;

    private List<ChatMessage> messages = new List<ChatMessage>();

    public InputField input;

    public Text responseUI;

    // Start is called before the first frame update
    void Start()
    {
        openAIApi = new OpenAIApi();
        
        ChatMessage init = new ChatMessage();
        init.Content = "You are Anna De Armas character from The Blade Runner 2049, Joi. Don't mind to ask about day, feelings, mood, etc.";
        init.Role = "system";

        messages.Add(init);
    }

    public async void AskAI(){
        var text = input.text;
        if(text.Length<2)return;

        input.text = "";

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
            responseUI.text = chatResponse.Content;
        } else {
            Debug.LogWarning("No text was generated from this prompt.");
        }
    }
}
