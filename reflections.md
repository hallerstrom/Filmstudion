# ”REST”

I API:et har jag skapat flera resurser och modeller som
representerar de objekt som API:et hanterar, såsom FILM, FILMSTUDIO, USER.
Varje resurs är anpassad för att hantera separat logik och data.

# ”Implementation”

De interna modellerna i API:et såsom ”Film” och ”FilmStudio”
hanterar mer komplexa data och logik i API:et. Som externa modeller använder
jag ”DTOs” och på detta sätt kan jag välja att dölja vissa data och se till att
det endast är nödvändiga data som skickas. Ett bra område där ”DTOs” används är
att en användares lösenord aldrig skickas tillbaka som ett svar från API:et.

# ”Säkerhet”

För att säkerställa vilka som kommer åt API:et använder jag
JWT för autentisering. När en användare (admin eller filmstudio) registrerar
sig via API:et så skapas en JWT-token. Denna token innehåller även användarens
roll som sedan används för att bestämma användarens behörigheter. Om till
exempel en ”filmstudio” försöker lägga till en film via API:et kommer de mötas
av en ”401” eftersom de inte har rätt roll.

I klienten loggar en ”Filmstudio” in med sitt användarnamn
och lösenord. När det sedan skickas till API:et så skapas en JWT-token som
returneras till klienten. Token sparas i ”localStorage” och används sedan vid
varje anrop mot API:et. När användaren sedan loggar ut så tas token bort och
”localStorage” töms helt.

# Sammanfattning

Detta har varit en otroligt tuff uppgift. När jag har
trott att jag har fått saker att fungera så blir det fel på ett annat ställe.
Återigen känner jag att det är planeringen det faller på som många gånger innan
och jag vet inte hur många gånger jag har börjat om och tänkt annorlunda. Jag
känner att jag har väldigt mycket mer att lära inom ”REST”-tänket, samt jobba
på att göra en gedigen planering och faktiskt hålla mig
