const apiBaseUrl = "http://localhost:5199/api";
let token = localStorage.getItem("jwt"); // Hämta token från localStorage vid sidladdning

document.getElementById("loginBtn").addEventListener("click", login);
document.getElementById("logoutBtn").addEventListener("click", logout);


async function fetchWithAuth(url, options = {}) {
  const headers = { "Content-Type": "application/json" };
  const token = localStorage.getItem("jwt"); // Hämta token vid varje anrop

  if (token) {
    headers["Authorization"] = `Bearer ${token}`;
  }

  console.log("Fetching:", url);
  console.log("Headers:", headers);

  // console.log("Response Status:", response.status);
  return fetch(url, { ...options, headers });
}

// Funktion för att logga in
async function login() {
  const username = document.getElementById("username").value;
  const password = document.getElementById("password").value;

  const response = await fetch(`${apiBaseUrl}/users/authenticate`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ username, password })
  });

  if (response.ok) {
    const data = await response.json();
    token = data.token; // Kontrollera att API:et returnerar rätt fält (bör vara 'token')

    localStorage.setItem("jwt", token); // Spara token i localStorage

    alert("Inloggad som " + data.userName);
    document.getElementById("loginBtn").style.display = "none";
    document.getElementById("logoutBtn").style.display = "inline";

    loadFilms();
    // loadRentals();
  } else {
    alert("Fel användarnamn eller lösenord");
  }
}

// Funktion för att logga ut
function logout() {
  token = null;
  localStorage.removeItem("jwt"); // Ta bort token från localStorage

  document.getElementById("loginBtn").style.display = "inline";
  document.getElementById("logoutBtn").style.display = "none";
  document.getElementById("rentals-section").style.display = "none";

  alert("Utloggad");
}

// Funktion för att hämta och visa filmer
async function loadFilms() {
  const response = await fetchWithAuth(`${apiBaseUrl}/films`);

  if (response.ok) {
    const films = await response.json();
    const filmsList = document.getElementById("filmsList");
    filmsList.innerHTML = "";

    films.forEach(film => {
      const li = document.createElement("li");
      li.textContent = `${film.title || film.Title} - ${film.description || film.Description}`;

      if (token) { // Visa låneknapp endast om användaren är inloggad
        const rentBtn = document.createElement("button");
        rentBtn.textContent = "Låna";
        rentBtn.onclick = () => rentFilm(film.filmId || film.FilmId);
        li.appendChild(rentBtn);
      }

      filmsList.appendChild(li);
    });
  } else {
    console.error("Fel vid hämtning av filmer:", response.status);
  }
}

// Funktion för att hyra film
async function rentFilm(filmId) {
  const studioid = 1; 

  const response = await fetchWithAuth(`${apiBaseUrl}/films/rent?id=${filmId}&studioid=${studioid}`, {
    method: "POST"
  });

  if (response.ok) {
    alert("Film uthyrd!");
    loadRentals();
  } else {
    alert("Fel vid uthyrning");
  }
}

// // Funktion för att ladda uthyrningar
// async function loadRentals() {
//   const response = await fetchWithAuth(`${apiBaseUrl}/mystudio/rentals`);

//   if (response.ok) {
//     console.log("Rentals fetched successfully!");
//   } else {
//     console.error("Fel vid hämtning av uthyrningar:", response.status);
//     alert("Du är inte inloggad eller har inga uthyrningar.");
//   }


//   if (response.ok) {
//     const rentals = await response.json();
//     const rentalsList = document.getElementById("rentalsList");
//     rentalsList.innerHTML = "";

//     rentals.forEach(rental => {
//       const li = document.createElement("li");
//       li.textContent = `FilmCopy ID: ${rental.filmCopyId || rental.FilmCopyId}`;

//       const returnBtn = document.createElement("button");
//       returnBtn.textContent = "Lämna tillbaka";
//       returnBtn.onclick = () => returnFilm(rental.filmId || rental.FilmId);
//       li.appendChild(returnBtn);

//       rentalsList.appendChild(li);
//     });

//     document.getElementById("rentals-section").style.display = "block";
//   } else {
//     console.error("Fel vid hämtning av uthyrningar:", response.status);
//     alert("Du är inte inloggad eller har inga uthyrningar.");
//   }
// }

// Funktion för att returnera film
async function returnFilm(filmId) {
  const studioid = 1;

  const response = await fetchWithAuth(`${apiBaseUrl}/films/return?id=${filmId}&studioid=${studioid}`, {
    method: "POST"
  });

  if (response.ok) {
    alert("Film returnerad!");
    loadRentals();
  } else {
    alert("Fel vid retur");
  }
}

// Starta scriptet genom att ladda filmer och uthyrningar om användaren är inloggad
if (token) {
  document.getElementById("loginBtn").style.display = "none";
  document.getElementById("logoutBtn").style.display = "inline";
  loadFilms();
  // loadRentals();
} else {
  document.getElementById("logoutBtn").style.display = "none";
}

// Ladda filmer oavsett inloggningsstatus
loadFilms();
