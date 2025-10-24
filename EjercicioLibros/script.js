const API_URL = "https://jsonplaceholder.typicode.com/posts";
const generosFake = ["Aventura", "Drama", "Fantasía", "Terror", "Romance", "Ciencia Ficción"];

// Mostrar mensaje de éxito o error
function mostrarMensaje(elemento, texto, tipo) {
    elemento.textContent = texto;
    elemento.className = `mt-3 text-center ${tipo}`;
    elemento.style.display = "block";
}

// --- POST: registrar libro ---
function initForm() {
    const formulario = document.getElementById("formularioAlta");
    const mensaje = document.getElementById("mensajeAlta");

    formulario.addEventListener("submit", async (e) => {
        e.preventDefault();
        mensaje.style.display = "none";

        const nuevoLibro = {
            title: document.getElementById("nombre").value.trim(),
            body: `ISBN: ${document.getElementById("isbn").value.trim()} | Fecha: ${document.getElementById("fechaPublicacion").value.trim()} | Género: ${document.getElementById("genero").value.trim()}`,
            userId: parseInt(document.getElementById("autor").value)
        };

        try {
            const res = await fetch(API_URL, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(nuevoLibro)
            });
            if (!res.ok) throw new Error("Error al registrar el libro");

            const data = await res.json();
            mostrarMensaje(mensaje, `✅ Libro "${data.title}" registrado correctamente (ID asignado: ${data.id})`, "text-success");
            formulario.reset();
            if (document.querySelector("table")) cargarLibros();
        } catch (err) {
            mostrarMensaje(mensaje, `❌ ${err.message}`, "text-danger");
        }
    });
}

// --- GET: cargar libros ---
async function cargarLibros() {
    const body = prepararTabla();
    body.innerHTML = "<tr><td colspan='4' class='text-center text-muted'>Cargando...</td></tr>";

    try {
        const res = await fetch(API_URL);
        if (!res.ok) throw new Error("Error al obtener los libros");

        const librosConGenero = (await res.json()).slice(0, 50).map(l => ({
            ...l,
            genero: generosFake[Math.floor(Math.random() * generosFake.length)]
        }));

        window.librosActuales = librosConGenero;
        mostrarLibros(librosConGenero);
    } catch (error) {
        body.innerHTML = `<tr><td colspan='4' class='text-center text-danger'>${error.message}</td></tr>`;
    }
}

// --- Filtrar libros por género ---
function filtrarPorGenero() {
    const genero = document.getElementById("filtroGenero").value.trim().toLowerCase();
    if (!window.librosActuales) return cargarLibros();

    const filtrados = genero
        ? window.librosActuales.filter(l => l.genero.toLowerCase().includes(genero))
        : window.librosActuales;

    const body = prepararTabla();
    body.innerHTML = filtrados.length ? "" : `<tr><td colspan='4' class='text-center text-danger'>No se encontraron libros del género "${genero}"</td></tr>`;

    mostrarLibros(filtrados);
}

// --- Mostrar libros en tabla ---
function mostrarLibros(lista) {
    const body = prepararTabla();
    body.innerHTML = "";
    lista.forEach(l => {
        const fila = document.createElement("tr");
        fila.innerHTML = `<td>${l.id}</td><td>${l.title}</td><td>${l.userId}</td><td>${l.genero}</td>`;
        body.appendChild(fila);
    });
}

// --- Crear tbody si no existe ---
function prepararTabla() {
    let body = document.querySelector("table tbody");
    if (!body) {
        body = document.createElement("tbody");
        document.querySelector("table").appendChild(body);
    }
    return body;
}

// --- Cargar contenido dinámico ---
function load_content(url) {
    fetch(url)
        .then(res => res.text())
        .then(txt => {
            const panel = document.getElementById('panel-content');
            panel.innerHTML = txt;

            // Inicializamos formulario y tabla después de cargar contenido
            if (document.getElementById("formularioAlta")) initForm();

        });
}
