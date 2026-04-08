namespace WebConferenceStreamFacade.Subsystems;

public sealed class ChatService
{
    public string ModerateLinks(string message)
    {
        Console.WriteLine("[Chat] Модерация ссылок в сообщении…");
        if (message.Contains("http://", StringComparison.OrdinalIgnoreCase)
            || message.Contains("https://", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("[Chat] Обнаружена ссылка — помечена для предпросмотра.");
        }

        return message;
    }

    public void SendMessage(string userId, string text)
    {
        Console.WriteLine($"[Chat] Сообщение от «{userId}»: {text}");
    }

    public void ClearHistory(string roomId)
    {
        Console.WriteLine($"[Chat] История чата комнаты «{roomId}» очищена (эмуляция).");
    }
}
