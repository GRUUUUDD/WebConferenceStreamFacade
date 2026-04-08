using WebConferenceStreamFacade.Facades;
using WebConferenceStreamFacade.Subsystems;

namespace WebConferenceStreamFacade;

/// <summary>Точка входа: сначала «грязный» клиент без фасада, затем тот же сценарий через фасад.</summary>
internal static class Program
{
    private static void Main()
    {
        // ——— Версия 1 (без фасада): клиент держит 5 подсистем и сам выстраивает порядок вызовов ———
        Console.WriteLine("========== Версия 1: без фасада (много зависимостей в одном месте) ==========");
        Console.WriteLine();

        var auth = new AuthService();
        var camera = new CameraService();
        var microphone = new MicrophoneService();
        var recording = new RecordingService();
        var chat = new ChatService();

        const string user = "organizer";
        const string password = "secret";
        const string roomSession = "room-42";
        const string message = "Всем привет! Ссылка: https://example.com";

        try
        {
            if (!auth.Login(user, password))
                throw new InvalidOperationException("Аутентификация не пройдена.");

            if (!auth.CanCreateRoom(user))
                throw new InvalidOperationException("Нет прав на комнату.");

            if (!camera.IsDriverAvailable())
                throw new InvalidOperationException("Драйвер камеры недоступен.");
            camera.EnableCamera();

            if (!microphone.CheckMicrophone())
                throw new InvalidOperationException("Микрофон не готов.");
            microphone.SetVolume(75);

            if (!recording.HasEnoughDiskSpace())
                throw new InvalidOperationException("Мало места на диске.");
            recording.StartRecording(roomSession);

            var moderated = chat.ModerateLinks(message);
            chat.SendMessage(user, moderated);

            Console.WriteLine();
            Console.WriteLine("[V1] Сценарий завершён без ошибок.");
        }
        catch (Exception ex)
        {
            Console.WriteLine();
            Console.WriteLine($"[V1] Ошибка: {ex.Message}");
        }

        Console.WriteLine();

        // ——— Версия 2 (фасад): клиенту достаточно одного объекта и одного метода ———
        Console.WriteLine("========== Версия 2: фасад (один вызов вместо цепочки) ==========");
        Console.WriteLine();

        try
        {
            var facade = new StreamFacade();
            facade.StartWebinar("user", "pass", true, true, "Hello!");

            Console.WriteLine();
            Console.WriteLine("[V2] Сценарий завершён без ошибок.");
        }
        catch (Exception ex)
        {
            Console.WriteLine();
            Console.WriteLine($"[V2] Ошибка: {ex.Message}");
        }

        Console.WriteLine();
    }
}
