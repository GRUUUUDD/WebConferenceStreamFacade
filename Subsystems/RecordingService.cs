namespace WebConferenceStreamFacade.Subsystems;

public sealed class RecordingService
{
    public bool HasEnoughDiskSpace()
    {
        Console.WriteLine("[Record] Проверка свободного места на диске…");
        Console.WriteLine("[Record] Достаточно места для записи.");
        return true;
    }

    public void StartRecording(string sessionLabel)
    {
        Console.WriteLine($"[Record] Запись экрана и звука начата (сессия: {sessionLabel}).");
    }

    public void StopRecording()
    {
        Console.WriteLine("[Record] Запись остановлена, файл сохранён (эмуляция).");
    }
}
