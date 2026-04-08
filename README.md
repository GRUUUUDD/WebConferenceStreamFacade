# WebConferenceStreamFacade

Демонстрация паттерна **Фасад** на тему веб-конференции / стрима (условный «Zoom»).

**Суть:** пять подсистем (`Auth`, `Camera`, `Microphone`, `Recording`, `Chat`) эмулируют работу через `Console.WriteLine`. В `Program.Main` сначала показан клиент **без фасада** (сам создаёт все сервисы и вызывает шаги по порядку), затем — **с фасадом** `StreamFacade`, где сценарий сводится к одному методу `StartWebinar(...)`.

## Запуск

Из каталога проекта:

```bash
dotnet run
```

Или с указанием файла проекта:

```bash
dotnet run --project WebConferenceStreamFacade.csproj
```

Требуется [.NET SDK](https://dotnet.microsoft.com/download) 9.0 (или совместимая версия, указанная в `WebConferenceStreamFacade.csproj`).
