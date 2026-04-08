namespace WebConferenceStreamFacade.Subsystems;

/// <summary>Подсистема аутентификации и прав (без фасада клиент держит ссылку на неё явно).</summary>
public sealed class AuthService
{
    public bool Login(string user, string password)
    {
        Console.WriteLine($"[Auth] Проверка учётных данных для «{user}»…");
        var ok = !string.IsNullOrWhiteSpace(user) && password.Length >= 4;
        Console.WriteLine(ok ? "[Auth] Логин успешен." : "[Auth] Ошибка: неверный логин или пароль.");
        return ok;
    }

    public bool CanCreateRoom(string userId)
    {
        Console.WriteLine($"[Auth] Проверка права на создание комнаты для «{userId}»…");
        Console.WriteLine("[Auth] Роль: организатор — разрешено.");
        return true;
    }

    public void EndSession(string userId)
    {
        Console.WriteLine($"[Auth] Сессия пользователя «{userId}» завершена.");
    }
}
