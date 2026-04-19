# MinimalCRUD

Небольшой REST API на ASP.NET Core с классическими контроллерами, слоем сервисов и Entity Framework Core. Пример полного CRUD для сущности пользователя с SQLite в качестве хранилища.

## Стек

- .NET 10 (`net10.0`)
- ASP.NET Core Web API (минимальный хост в `Program.cs`, маршруты через контроллеры)
- Entity Framework Core 10 + провайдер SQLite
- Nullable reference types, implicit usings

## Структура проекта

| Папка / область | Назначение |
|-----------------|------------|
| `Controllers` | HTTP-слой: маршруты, коды ответов, привязка DTO |
| `Services` | Интерфейс `IUserService` и реализация `UserService` — работа с данными и маппинг в DTO ответа |
| `DTO` | Модели входа/выхода API (`UserDTO`, `UpdateUserDTO`, `UserResponseDTO` и др.) |
| `Models` | Доменная модель `User` |
| `Data` | `AppDbContext`, регистрация `DbSet<User>` |

Точка входа — `Program.cs`: регистрация DI (`AddScoped<IUserService, UserService>`), `AddControllers`, `AddDbContext` с строкой `Data Source=app.db`, `MapControllers`.

## Требования

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

## Запуск

Из корня репозитория:

```bash
dotnet restore
dotnet run --project MinimalCRUD.csproj
```

По умолчанию (профиль из `Properties/launchSettings.json`) приложение слушает, например, `http://localhost:5123` и/или `https://localhost:7041` — смотрите актуальные URL в консоли после старта.

## База данных и миграции

Строка подключения задана в коде: SQLite-файл `app.db` в рабочей директории приложения.

В репозитории нет готовых миграций. Перед первым обращением к API создайте схему, например:

```bash
dotnet ef migrations add InitialCreate --project MinimalCRUD.csproj
dotnet ef database update --project MinimalCRUD.csproj
```

Пакет `Microsoft.EntityFrameworkCore.Tools` уже подключён в `.csproj` для работы CLI `dotnet ef`.

## API

Базовый префикс маршрута: `api/User` (контроллер `UserController`).

| Метод | Путь | Описание |
|--------|------|----------|
| `GET` | `api/User` | Список пользователей |
| `GET` | `api/User/{id}` | Пользователь по `Guid` |
| `POST` | `api/User` | Создание (тело: `UserDTO` — `Name`, `Email`) |
| `PUT` | `api/User/{id}` | Частичное обновление (тело: `UpdateUserDTO`; пустые строки полей не перезаписывают значения) |
| `DELETE` | `api/User/{id}` | Удаление; при успехе — `204 No Content` |

Ответы с данными пользователя используют `UserResponseDTO` (`Id`, `Name`, `Email`).

## Поведение слоя сервиса

- Идентификатор при создании задаётся на сервере (`Guid.NewGuid()`).
- Обновление: поля `Name` и `Email` меняются только если в DTO передана непустая строка (`string.IsNullOrWhiteSpace`).
- Удаление и обновление несуществующего `id` обрабатываются на уровне контроллера (`404`, `400` где применимо).
