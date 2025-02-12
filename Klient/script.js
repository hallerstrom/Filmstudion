let API_URL = 'http://127.0.0.1:5199/api'; 
let currentUser = null;

// Autentisering
async function login() {
    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;

    const response = await fetch(`${API_URL}/users/authenticate`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username, password })
    });

    if (response.ok) {
        const user = await response.json();
        localStorage.setItem('token', user.token);
        currentUser = user;
        loadMainContent();
    } else {
        alert('Fel användarnamn/lösenord');
    }
}

function logout() {
    localStorage.removeItem('token');
    location.reload();
}


// Registrering
async function registerStudio() {
    const studioData = {
        name: document.getElementById('regName').value,
        city: document.getElementById('regCity').value,
        username: document.getElementById('regUsername').value,
        password: document.getElementById('regPassword').value
    };

    const response = await fetch(`${API_URL}/filmstudio/register`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(studioData)
    });

    if (response.ok) {
        alert('Studio registrerad! Logga in nu.');
        showLogin();
    } else {
        alert('Registrering misslyckades');
    }
}


// Filmhantering
async function loadFilms() {
    const response = await fetch(`${API_URL}/film`);
    const films = await response.json();

    const filmsList = document.getElementById('filmsList');
    filmsList.innerHTML = films.map(film => {
        console.log("currentUser role:", currentUser?.role);
        return `
            <div class="filmCard">
                <h5>${film.title}</h5>
                <p>Lediga kopior: ${film.filmCopies?.length || 0}</p>
                    
                    <button onclick="rentFilm(${film.filmId})">Låna</button>
            </div>
        `;
    }).join('');
}

async function loadRentals() {
    const response = await fetch(`${API_URL}/mystudio/rentals`, {
        headers: { 'Authorization': `Bearer ${localStorage.getItem('token')}` }
    });
    
    const rentals = await response.json();
    const rentalsList = document.getElementById('rentalsList');
    rentalsList.innerHTML = rentals.map(film => `
        <div class="filmCard">
            <h5>${film.title}</h5>
            <button onclick="returnFilm(${film.filmId})">Lämna tillbaka</button>
        </div>
    `).join('');
}

async function rentFilm(filmId) {
    try {
        const response = await fetch(`${API_URL}/film/rent?id=${filmId}`, { // Skicka ENDAST filmId
            method: 'POST',
            headers: { 'Authorization': `Bearer ${localStorage.getItem('token')}` } // Viktigt: Inkludera token
        });

        if (response.ok) {
            alert("Filmen har lånats!"); // Enkelt meddelande
        } else {
            const errorText = await response.text();
            alert(`Rent film failed: ${response.status} - ${errorText}`); // Visa felmeddelande
        }
    } catch (error) {
        console.error("Rent Film Error:", error);
        alert("Ett fel uppstod."); // Generellt felmeddelande
    }
}
async function returnFilm(filmId) {
    const response = await fetch(`${API_URL}/films/return?id=${filmId}&studioid=${currentUser.filmStudioId}`, {
        method: 'POST',
        headers: { 'Authorization': `Bearer ${localStorage.getItem('token')}` }
    });

    if (response.ok) {
        loadFilms();
        loadRentals();
    } else {
        alert('Kunde inte lämna tillbaka filmen');
    }
}

// --------------------------
// Hjälpfunktioner
// --------------------------
function showRegister() {
    document.getElementById('loginForm').style.display = 'none';
    document.getElementById('registerForm').style.display = 'block';
}

function showLogin() {
    document.getElementById('registerForm').style.display = 'none';
    document.getElementById('loginForm').style.display = 'block';
}

function loadMainContent() {
    document.getElementById('authSection').style.display = 'none';
    document.getElementById('mainContent').style.display = 'block';
    document.getElementById('studioName').textContent = currentUser.filmStudio?.name;
    loadFilms();
    loadRentals();
}

// Initiera
window.onload = () => {
    const token = localStorage.getItem('token');
    if (token) {
        // Hämta användardata från token (behöver implementeras i API:et)
        fetch(`${API_URL}/users/me`, {
            headers: { 'Authorization': `Bearer ${token}` }
        })
        .then(response => response.json())
        .then(user => {
            currentUser = user;
            loadMainContent();
        });
    }
};