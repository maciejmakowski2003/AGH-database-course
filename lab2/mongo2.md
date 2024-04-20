# Dokumentowe bazy danych – MongoDB

ćwiczenie 2


---

**Imiona i nazwiska autorów:**

--- 


## Yelp Dataset

- [www.yelp.com](http://www.yelp.com) - serwis społecznościowy – informacje o miejscach/lokalach
- restauracje, kluby, hotele itd. `businesses`,
- użytkownicy odwiedzają te miejsca - "meldują się"  `check-in`
- użytkownicy piszą recenzje `reviews` o miejscach/lokalach i wystawiają oceny oceny,
- przykładowy zbiór danych zawiera dane z 5 miast: Phoenix, Las Vegas, Madison, Waterloo i Edinburgh.

# Zadanie 1 - operacje wyszukiwania danych

Dla zbioru Yelp wykonaj następujące zapytania

W niektórych przypadkach może być potrzebne wykorzystanie mechanizmu Aggregation Pipeline

[https://www.mongodb.com/docs/manual/core/aggregation-pipeline/](https://www.mongodb.com/docs/manual/core/aggregation-pipeline/)


1. Zwróć dane wszystkich restauracji (kolekcja `business`, pole `categories` musi zawierać wartość "Restaurants"), które są otwarte w poniedziałki (pole hours) i mają ocenę co najmniej 4 gwiazdki (pole `stars`).  Zapytanie powinno zwracać: nazwę firmy, adres, kategorię, godziny otwarcia i gwiazdki. Posortuj wynik wg nazwy firmy.

2. Ile każda firma otrzymała ocen/wskazówek (kolekcja `tip` ) w 2012. Wynik powinien zawierać nazwę firmy oraz liczbę ocen/wskazówek Wynik posortuj według liczby ocen (`tip`).

3. Recenzje mogą być oceniane przez innych użytkowników jako `cool`, `funny` lub `useful` (kolekcja `review`, pole `votes`, jedna recenzja może mieć kilka głosów w każdej kategorii).  Napisz zapytanie, które zwraca dla każdej z tych kategorii, ile sumarycznie recenzji zostało oznaczonych przez te kategorie (np. recenzja ma kategorię `funny` jeśli co najmniej jedna osoba zagłosowała w ten sposób na daną recenzję)

4. Zwróć dane wszystkich użytkowników (kolekcja `user`), którzy nie mają ani jednego pozytywnego głosu (pole `votes`) z kategorii (`funny` lub `useful`), wynik posortuj alfabetycznie według nazwy użytkownika.

5. Wyznacz, jaką średnia ocenę uzyskała każda firma na podstawie wszystkich recenzji (kolekcja `review`, pole `stars`). Ogranicz do firm, które uzyskały średnią powyżej 3 gwiazdek.

	a) Wynik powinien zawierać id firmy oraz średnią ocenę. Posortuj wynik wg id firmy.

	b) Wynik powinien zawierać nazwę firmy oraz średnią ocenę. Posortuj wynik wg nazwy firmy.

## Zadanie 1  - rozwiązanie

> Wyniki: 
> 
> przykłady, kod, zrzuty ekranów, komentarz ...

```js
--  ...
```

# Zadanie 2 - modelowanie danych


Zaproponuj strukturę bazy danych dla wybranego/przykładowego zagadnienia/problemu

Należy wybrać jedno zagadnienie/problem (A lub B)

Przykład A
- Wykładowcy, przedmioty, studenci, oceny
	- Wykładowcy prowadzą zajęcia z poszczególnych przedmiotów
	- Studenci uczęszczają na zajęcia
	- Wykładowcy wystawiają oceny studentom
	- Studenci oceniają zajęcia

Przykład B
- Firmy, wycieczki, osoby
	- Firmy organizują wycieczki
	- Osoby rezerwują miejsca/wykupują bilety
	- Osoby oceniają wycieczki

a) Warto zaproponować/rozważyć różne warianty struktury bazy danych i dokumentów w poszczególnych kolekcjach oraz przeprowadzić dyskusję każdego wariantu (wskazać wady i zalety każdego z wariantów)

b) Kolekcje należy wypełnić przykładowymi danymi

c) W kontekście zaprezentowania wad/zalet należy zaprezentować kilka przykładów/zapytań/zadań/operacji oraz dla których dedykowany jest dany wariantów

W sprawozdaniu należy zamieścić przykładowe dokumenty w formacie JSON ( pkt a) i b)), oraz kod zapytań/operacji (pkt c)), wraz z odpowiednim komentarzem opisującym strukturę dokumentów oraz polecenia ilustrujące wykonanie przykładowych operacji na danych

Do sprawozdania należy kompletny zrzut wykonanych/przygotowanych baz danych (taki zrzut można wykonać np. za pomocą poleceń `mongoexport`, `mongdump` …) oraz plik z kodem operacji zapytań (załącznik powinien mieć format zip).


## Zadanie 2  - rozwiązanie
Wybraliśmy zagadnienie A

### Dekompozycja

Kolekcje:
1. lecturers
- wykładowcy prowadzą ćwiczenia/wykłady
- kady dokument reprezentuje jednego wykładowcę
Schemat:
```json
{
  "_id": ObjectId,       
  "firstname": String, 
  "lastname": String,
  "email": String,
  "departmentId": ObjectId,
  "birdhDate": Date,    
  "subjectsIds": List<ObjectId>,
}
```

2. students
- studenci mogą uczęszczać na zajęcia/wykłady
- kazdy dokumenty reprezentuje jednego studenta

```json
{
  "_id": ObjectId,       
  "firstname": String,     
  "lastname": String,      
  "email": String,  
  "birthDate": String,
  "courseIds": List<ObjectId>
}
```

3. courses
- na studia mogą zapisywać się studenci
- kazdy dokument reprezentuje pojedynczy kierunek studiów
```json
{
  "_id": ObjectId,          
  "name": String,              
  "description": String,
  "departmentId": ObjectId
}
```

4. departments
- wydzialy studiów

```json
{
  "_id": ObjectId,          
  "name": String,              
  "description": String,
}
```

5. semesters
```json
{
  "_id": ObjectId,          
  "semesterNumber": Int32,
  "startDate": Date,
  "endDate": Date,
  "courseId": ObjectId
}
```

6. subjects
```json
{
  "_id": ObjectId,
  "name": String,
  "description": String,
  "ECTSPoints": String
}
```

7. classSeries
- kazdy dokument reprezentuje przedmiot na danym semestrze
```json
{
  "_id": ObjectId,
  "semesterId": ObjectId,
  "subjectId": ObjectId,
}
```
8. classes
```json
{
  "_id": ObjectId,          
  "startDate": Date, // date includes both date and time of the class start/end
  "endDate": Date,
  "semesterId": ObjectId,
  "subjectId": ObjectId,
  "lecturerId": ObjectId
}
```

9. classSerieRatings
- kazdy dokument reprezentuje pojedynczą ocenę zajęć na danym semestrze, którą wystawia student
```json
{
  "_id": ObjectId,
  "rating": Int32,
  "classSerieId": ObjectId,
  "studentId": ObjectId
}
```

10. studentGrades
- kazdy dokument reprezentuje pojedynczą ocenę z przedmiotu, którą otrzymuje student

```json
{
  "_id": ObjectId,
  "grade": Int32,
  "studentId": ObjectId,
  "classSerieId": ObjectId
}
```

> Wyniki: 
> 
> przykłady, kod, zrzuty ekranów, komentarz ...

```js
--  ...
```

---

Punktacja:

|         |     |
| ------- | --- |
| zadanie | pkt |
| 1       | 0,6 |
| 2       | 1,4 |
| razem   | 2   |



