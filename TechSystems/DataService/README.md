# DataService Backend API

## Архитектура
- `DataService.Domain` — доменные модели и сущности.
- `DataService.Application` — DTO, сервисы, интерфейсы репозиториев, валидаторы.
- `DataService.Infrastructure` — EF Core контекст, репозитории, кеширование.
- `DataService.Api` — HTTP API, аутентификация, телеметрия, middleware.
- `DataService.Tests` — unit-тесты бизнес-логики.
- `DataService.IntegrationTests` — интеграционные тесты API.

## Запуск локально
1. Установите .NET 10 SDK.
2. Настройте переменные окружения (или используйте `appsettings.json`):
   - `DATABASE_CONNECTION_STRING`
   - `REDIS_CONNECTION_STRING`
   - `KEYCLOAK_AUTHORITY`
   - `KEYCLOAK_AUDIENCE`
3. Запуск API:
   - `dotnet run --project DataService/DataService.Api`
4. Swagger доступен в режиме Development по адресу `/swagger`.

## Docker Compose
1. Перейдите в папку `DataService`.
2. Запустите: `docker compose up --build`.
3. Сервисы:
   - API: `http://localhost:8080`
   - Keycloak: `http://localhost:8080` (admin/admin)
   - Prometheus: `http://localhost:9090`
   - Grafana: `http://localhost:3000` (admin/admin)
   - Kibana: `http://localhost:5601`

## Аутентификация
- JWT-токены выдаёт Keycloak (realm: `dataservice`).
- Используйте `Authorization: Bearer <token>`.
- Политики:
  - `AdminOnly` — роль `admin`
  - `OfficerOnly` — роли `officer` или `admin`

## Телеметрия
- Трассировки и логи отправляются в OpenTelemetry Collector (`OTEL_EXPORTER_OTLP_ENDPOINT`).
- Метрики доступны на `/metrics` и собираются Prometheus.
- Логи поступают в Logstash, далее в Elasticsearch.

## Тесты
- Unit: `dotnet test DataService/DataService.Tests`
- Integration: `dotnet test DataService/DataService.IntegrationTests`
