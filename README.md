# API do organizowania spotkań

Do zrobienia tego zadania użyłem CQRS pattern przy użyciu MediatR. Bazą danych jest SQL Server, a do testów użyłem xUnit.

## Działanie API

* Utworzenie spotkania: `POST /api/activities` </br>
Logika znajduje się w Application/Activities/Create

* Usunięcie spotkania: `DELETE /api/activities` </br>
Logika znajduje się w Application/Activities/Delete

* Wyświetlenie listy spotkań: `GET /api/activities` </br>
Logika znajduje się w Application/Activities/List

* Zapis uczestnika na spotkanie: `POST /api/atendees` </br>
Logika znajduje się w Application/Atendees/Create

## Setup
Najpierw trzeba dodać swój własny database do DefaultConnection w appsettings.Development.json <br />
* Włączenie API
```
cd API && dotnet run
```

* Włączenie testów jednostowych
```
cd Tests && dotnet test
```