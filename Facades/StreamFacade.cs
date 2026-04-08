using WebConferenceStreamFacade.Subsystems;

namespace WebConferenceStreamFacade.Facades;

/// <summary>
/// Фасад: одна точка входа для клиента. Здесь сосредоточена последовательность вызовов
/// пяти подсистем — клиенту не нужно знать порядок, имена методов и условия (камера/запись).
/// При изменении сценария (например, «сначала проверить микрофон перед записью»)
/// правки вносятся только сюда, а не в каждом месте вызова.
/// </summary>
public sealed class StreamFacade
{
    private readonly AuthService _auth;
    private readonly CameraService _camera;
    private readonly MicrophoneService _microphone;
    private readonly RecordingService _recording;
    private readonly ChatService _chat;

    /// <summary>Создаёт подсистемы внутри — клиент не зависит от их конструкторов.</summary>
    public StreamFacade()
        : this(new(), new(), new(), new(), new())
    {
    }

    /// <summary>Альтернатива: внедрение подсистем (удобно для тестов).</summary>
    public StreamFacade(
        AuthService auth,
        CameraService camera,
        MicrophoneService microphone,
        RecordingService recording,
        ChatService chat)
    {
        _auth = auth;
        _camera = camera;
        _microphone = microphone;
        _recording = recording;
        _chat = chat;
    }

    /// <summary>
    /// Единый сценарий вебинара: фасад скрывает «простыню» из пяти сервисов и десятка вызовов.
    /// </summary>
    public void StartWebinar(string user, string password, bool withCamera, bool withRecording, string chatMessage)
    {
        const string sessionId = "room-42";

        if (!_auth.Login(user, password))
            throw new InvalidOperationException("Не удалось войти в систему.");

        if (!_auth.CanCreateRoom(user))
            throw new InvalidOperationException("Нет прав на создание комнаты.");

        if (withCamera)
        {
            if (!_camera.IsDriverAvailable())
                throw new InvalidOperationException("Камера недоступна.");
            _camera.EnableCamera();
        }
        else
        {
            _camera.DisableCamera();
        }

        if (!_microphone.CheckMicrophone())
            throw new InvalidOperationException("Микрофон не готов.");

        _microphone.SetVolume(75);

        // Фасад: одно место, куда добавить шаг «проверка микрофона перед записью» —
        // без дублирования у всех клиентов.
        if (withRecording)
        {
            if (!_recording.HasEnoughDiskSpace())
                throw new InvalidOperationException("Недостаточно места для записи.");
            _recording.StartRecording(sessionId);
        }

        var safeText = _chat.ModerateLinks(chatMessage);
        _chat.SendMessage(user, safeText);
    }
}
