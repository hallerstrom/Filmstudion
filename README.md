# FilmStudio API

## Starta API:et

Följ dessa steg för att starta API:et lokalt:

1. **Klona repositoriet**  
   ```
   git clone https://github.com/ditt-repo/filmstudio-api.git
   cd filmstudio-api
   ```
2. **Installera beroenden**
   ```
   dotnet restore
   ```
3. **Starta API:et**
```
   dotnet run
```
4. Användning av API:et
   1. Registrera en användare (Admin eller Filmstudio)
   Endpoint: POST /api/users/register
   Body:
   ```
   {
      "Username": "testuser",
     "Password": "password123",
     "IsAdmin": true
   }
   ```
   2. Autentisera en användare
   Endpoint: POST /api/users/authenticate
   Body:
   ```
   {
     "Username": "testuser",
     "Password": "password123"
   }
   ```
   Svar:
   
   ```
   {
     "token": "JWT-token-här"
   }
   ```
   3. Registrera en filmstudio
   Endpoint: POST /api/filmstudio/register
   Body:
   ```
   {
     "Name": "Test Studio",
     "City": "Stockholm"
   }
   ```
   4. Lägg till en film (Endast admin)
   Endpoint: POST /api/films
   Header:
   ```
   Authorization: Bearer <JWT-token>
   ```
   Body:
   ```
   {
     "Title": "Test Film",
     "Description": "A great movie",
     "NumberOfCopies": 5
   }
   ```
   5. Hyr en film (Endast filmstudio)
   Endpoint: POST /api/rent?id=1&studioid=1
   Header:
   ```
   Authorization: Bearer <JWT-token>
   ```
**Säkerhet**
API:et använder JWT för autentisering.
För att få tillgång till skyddade endpoints, skicka din token i Authorization-headern:

**Authorization: Bearer <JWT-token>**
Admin kan lägga till filmer, medan filmstudios kan hyra filmer.
Om en användare inte har rätt behörighet returneras en 401 Unauthorized.

**Logga ut**
För att logga ut, rensa JWT-token från klienten (t.ex. localStorage i webbläsaren).
```
localStorage.removeItem("token");
```
**Teknologier**
.NET Core
Entity Framework
JWT för autentisering
