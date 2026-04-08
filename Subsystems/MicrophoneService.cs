namespace WebConferenceStreamFacade.Subsystems;

public sealed class MicrophoneService
{
    public bool CheckMicrophone()
    {
        Console.WriteLine("[Mic] Проверка микрофона…");
        Console.WriteLine("[Mic] Устройство доступно.");
        return true;
    }

    public void SetVolume(int percent)
    {
        var v = Math.Clamp(percent, 0, 100);
        Console.WriteLine($"[Mic] Громкость установлена: {v}%.");
    }

    public void SetMuted(bool muted)
    {
        Console.WriteLine(muted ? "[Mic] Звук выключен (mute)." : "[Mic] Звук включён.");
    }
}
