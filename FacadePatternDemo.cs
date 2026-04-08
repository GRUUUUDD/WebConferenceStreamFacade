using WebConferenceStreamFacade.Facades;
using WebConferenceStreamFacade.Subsystems;

namespace WebConferenceStreamFacade;

/// <summary>Сравнение клиента без фасада и с фасадом.</summary>
internal static class FacadePatternDemo
{
    private const string User = "organizer";
    private const string Password = "secret";
    private const string RoomSession = "room-42";
    private const string Message = "Всем привет! Ссылка: https://example.com";

    public static void Run()
    {
        RunWithoutFacade();
        Console.WriteLine();
        RunWithFacade();
        Console.WriteLine();
    }

    private static void Section(string title)
    {
        Console.WriteLine($"========== {title} ==========");
        Console.WriteLine();
    }

    private static void RunScenario(string versionTag, Action action)
    {
        try
        {
            action();
            Console.WriteLine();
            Console.WriteLine($"[{versionTag}] Сценарий завершён без ошибок.");
        }
        catch (Exception ex)
        {
            Console.WriteLine();
            Console.WriteLine($"[{versionTag}] Ошибка: {ex.Message}");
        }
    }

    private static void RunWithoutFacade()
    {
        Section("Версия 1: без фасада (много зависимостей в одном месте)");

        RunScenario("V1", () =>
        {
            var auth = new AuthService();
            var camera = new CameraService();
            var microphone = new MicrophoneService();
            var recording = new RecordingService();
            var chat = new ChatService();

            if (!auth.Login(User, Password))
                throw new InvalidOperationException("Аутентификация не пройдена.");

            if (!auth.CanCreateRoom(User))
                throw new InvalidOperationException("Нет прав на комнату.");

            if (!camera.IsDriverAvailable())
                throw new InvalidOperationException("Драйвер камеры недоступен.");
            camera.EnableCamera();

            if (!microphone.CheckMicrophone())
                throw new InvalidOperationException("Микрофон не готов.");
            microphone.SetVolume(75);

            if (!recording.HasEnoughDiskSpace())
                throw new InvalidOperationException("Мало места на диске.");
            recording.StartRecording(RoomSession);

            var moderated = chat.ModerateLinks(Message);
            chat.SendMessage(User, moderated);
        });
    }

    private static void RunWithFacade()
    {
        Section("Версия 2: фасад (один вызов вместо цепочки)");

        RunScenario("V2", () =>
        {
            var facade = new StreamFacade();
            facade.StartWebinar("user", "pass", withCamera: true, withRecording: true, chatMessage: "Hello!");
        });
    }
}
