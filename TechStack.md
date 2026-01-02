# Технический стек
Проект использует следующие технологии:

- **KeyCloak** - Аутентификация и авторизация
- **Redis** - Кэширование
- **Kafka** - Очереди сообщений
- **ELK (Elasticsearch, Logstash, Kibana)** - Логирование и мониторинг
  - Elasticsearch
  - Logstash
  - Kibana
- **iText** - Генерация PDF
- **EntityFramework** - ORM
- **Dapper** - Лёгкий ORM
- **Blazor** - UI
- **DevExpress/Telerik** - UI Компоненты
- **BenchmarkDotNet** - Бенчмаркинг
- **OpenTelemetry** - Трассировка
- **Grafana** - Мониторинг и визуализация метрик

---

## Развёртывание стека в Docker

Для развёртывания каждого компонента стека в Docker можно использовать следующие команды и конфигурации:

### KeyCloak
```
docker run -d --name keycloak -p 8080:8080 -e KC_BOOTSTRAP_ADMIN_USERNAME=admin -e KC_BOOTSTRAP_ADMIN_PASSWORD=admin quay.io/keycloak/keycloak:latest start-dev
```
```
eyJhbGciOiJIUzUxMiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJhNjczZTA4Ni05YmE0LTRlYTMtOTEzZi0yYThmZWViYTM2MmEifQ.eyJleHAiOjE3NjY4NjQ5NzMsImlhdCI6MTc2Njc3ODU3MywianRpIjoiYjczODI5NzQtNGQ4My00ZWNiLTlkYTUtNzE2YmIxNjY1OTg4IiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo4MDgwL3JlYWxtcy9tYXN0ZXIiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjgwODAvcmVhbG1zL21hc3RlciIsInR5cCI6IkluaXRpYWxBY2Nlc3NUb2tlbiJ9.gxa1TcebaDk-6H8Q3TLM_bVTL3I217oJ_0wxHW4p8hm8J-Ootxf0gKSBJT6JqMsZaiSfoY8XLoXqNctMz9Mbhw
```

### Redis
```
docker run -d \
  --name redis \
  -p 6379:6379 \
  redis:latest
```

### Kafka
```
docker run -d \
  --name zookeeper \
  -p 2181:2181 \
  confluentinc/cp-zookeeper:latest

docker run -d \
  --name kafka \
  -p 9092:9092 \
  -e KAFKA_ZOOKEEPER_CONNECT=localhost:2181 \
  -e KAFKA_ADVERTISED_LISTENERS=PLAINTEXT://localhost:9092 \
  confluentinc/cp-kafka:latest
```

### ELK (Elasticsearch, Logstash, Kibana)
#### Elasticsearch
```
docker run -d \
  --name elasticsearch \
  -p 9200:9200 \
  -e "discovery.type=single-node" \
  docker.elastic.co/elasticsearch/elasticsearch:latest
```

#### Logstash
```
docker run -d \
  --name logstash \
  -p 5044:5044 \
  -v $(pwd)/logstash.conf:/usr/share/logstash/pipeline/logstash.conf \
  docker.elastic.co/logstash/logstash:latest
```

#### Kibana
```
docker run -d \
  --name kibana \
  -p 5601:5601 \
  --link elasticsearch:elasticsearch \
  docker.elastic.co/kibana/kibana:latest
```

### Grafana
```
docker run -d \
  --name grafana \
  -p 3000:3000 \
  grafana/grafana:latest
```

---

## Примечания
- Убедитесь, что у вас установлен Docker и он запущен.
- Для настройки и конфигурации каждого компонента рекомендуется ознакомиться с официальной документацией.
- Файлы конфигурации, такие как `logstash.conf`, должны быть подготовлены заранее и размещены в соответствующих директориях.