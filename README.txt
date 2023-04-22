# RestaurantAPI
 Simple API to handle restaurants related requests
 
Opis aplikacji
--------------------------------------------------
Aplikacja realizowana w trakcie kursu "ASP.NET Core - Tworzenie aplikacji webowych" (YouTube, kanały: Pasja informatyki, Fullstack Developer).

Jej zadaniem jest odbieranie i przetwarzanie prostych zapytań typu Get, Post, Put, Delete dotyczących restauracji. Zasoby (encje w bazie danych), na których operuje aplikacja to restauracje, ich adresy oraz oferowane przez nie dania. Wszystkie zawarte są w osobnych klasach, mapowanych do bazy danych z użyciem EntityFramework. Po pobraniu należy utworzyć własną bazę danych, podać jej connection string do klasy RestaurantDbContext i wykonać update bazy danych na podstawie załączonych migracji. Dołączony do aplikacji Seeder automatycznie doda do niej dwie restauracje, na których można przetestować requesty.

Dodatkowo API zawiera prosty mechanizm logów z użyciem NLog, użyto w niej AutoMappera do tworzenia obiektów pośrednich dto jak również zawiera proste walidacje poprawności. W aplikacji są również dwa dodatkowe middleware: jedno mierzy czas wykonywania requestów i wypisuje informacje o nich do konsoli, drugie jest ogólnego przeznaczenia i wychwytuje błędy (choć będę tutaj szczery, niestety nie działa doskonale).

Do całości został własnoręcznie dodany Swagger dokumentujący całe API.
