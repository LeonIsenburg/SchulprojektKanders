# SchulprojektKanders

## Projektstruktur

| Ordner            | Inhalt                                                        |
| ----------------- | ------------------------------------------------------------- |
| `Backend/`        | ASP.NET Core Web API (.NET 10, EF Core, SQLite)               |
| `Frontend/`       | Vue 3 + Vite                                                  |
| `AppHost/`        | .NET Aspire: startet Backend, Frontend und Datenbank zusammen |
| `ServiceDefaults/`| Gemeinsame Aspire-Einstellungen (Logging, Tracing, Health-Checks) |

## Voraussetzungen

- .NET SDK 10
- Node.js 22.18+ oder 24.12+
- Docker Desktop (nur für den Docker-Export)
- Aspire-CLI: `dotnet tool install -g Aspire.Cli`
- Einmalig: `dotnet dev-certs https --trust`

## Starten mit Aspire (Entwicklung)

```sh
aspire run
```

oder in VS Code die Startkonfiguration **„Aspire: AppHost (Backend + Frontend + DB)“** wählen (F5).
Dafür wird die Extension `microsoft-aspire.aspire-vscode` benötigt.

Das Aspire-Dashboard öffnet sich im Browser. Dort siehst du alle Ressourcen, ihre URLs, Logs und Traces:

- `appdb` – SQLite-Datenbank (`Backend/app.db`)
- `backend` – Web API
- `frontend` – Vite-Dev-Server; Aufrufe von `/api/...` werden an das Backend weitergeleitet

Das Backend lässt sich weiterhin auch alleine starten (`dotnet run --project Backend`).

## Starten mit Docker

```sh
aspire deploy
```

Baut die Images und startet alles per Docker Compose:

- Frontend: http://localhost:8080 (leitet `/api/...` an das Backend weiter)
- Backend: http://localhost:8081
- Die Datenbank liegt im Docker-Volume `appdb-data` und bleibt bei Neustarts erhalten.

Nur die Compose-Dateien erzeugen, ohne zu starten: `aspire publish` (Ausgabe in `aspire-output/`).
