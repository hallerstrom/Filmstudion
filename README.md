# FilmStudio API

## Starta API:et

Följ dessa steg för att starta API:et lokalt:

1. **Klona repositoriet**  
   ```bash
   git clone https://github.com/hallerstrom/Filmstudion
   cd filmstudio-api
Installera beroenden

bash
Kopiera
Redigera
dotnet restore
Uppdatera databasen

bash
Kopiera
Redigera
dotnet ef database update
Starta API:et

bash
Kopiera
Redigera
dotnet run

Användning av API:et
1. Registrera en användare (Admin eller Filmstudio)
Endpoint: POST /api/users/register
Body:

json
Kopiera
Redigera
{
  "Username": "testuser",
  "Password": "password123",
  "IsAdmin": true
}
2. Autentisera en användare
Endpoint: POST /api/users/authenticate
Body:

json
Kopiera
Redigera
{
  "Username": "testuser",
  "Password": "password123"
}
Svar:

json
Kopiera
Redigera
{
  "token": "JWT-token-här"
}
3. Registrera en filmstudio
Endpoint: POST /api/filmstudio/register
Body:

json
Kopiera
Redigera
{
  "Name": "Test Studio",
  "City": "Stockholm"
}
4. Lägg till en film (Endast admin)
Endpoint: POST /api/films
Header:

http
Kopiera
Redigera
Authorization: Bearer <JWT-token>
Body:

json
Kopiera
Redigera
{
  "Title": "Test Film",
  "Description": "A great movie",
  "NumberOfCopies": 5
}

Säkerhet
API:et använder JWT för autentisering.
För att få tillgång till skyddade endpoints, skicka din token i Authorization-headern:
http
Kopiera
Redigera
Authorization: Bearer <JWT-token>
Admin kan lägga till filmer, medan filmstudios kan hyra filmer.
Om en användare inte har rätt behörighet returneras en 401 Unauthorized.

Teknologier
.NET Core
Entity Framework
JWT för autentisering