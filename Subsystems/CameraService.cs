namespace WebConferenceStreamFacade.Subsystems;

public sealed class CameraService
{
    public bool IsDriverAvailable()
    {
        Console.WriteLine("[Camera] Проверка драйвера камеры…");
        Console.WriteLine("[Camera] Драйвер найден (эмуляция).");
        return true;
    }

    public void EnableCamera()
    {
        Console.WriteLine("[Camera] Камера включена.");
    }

    public void DisableCamera()
    {
        Console.WriteLine("[Camera] Камера выключена.");
    }
}
