//FRAN, BALTA Investigando encontre esta libreria de JS SweetAlert: muestra ventanas emergentes  y permite confirmar acciones


/* ---------- FUNCION PARA VENTANA EMERGENTE PARA AGREGAR UN DOMICILIO ---------- */

    function abrirModalDomicilio() {
        document.getElementById("modalDomicilio").style.display = "flex";
    }

    function cerrarModalDomicilio() {
        document.getElementById("modalDomicilio").style.display = "none";
    }

    function confirmarCompra() {

        event.preventDefault(); 

        Swal.fire({
            title: '¿Finalizar compra?',
            text: "Confirmá para completar el proceso.",
            icon: 'question',
            showCancelButton: true,
            confirmButtonText: 'Sí, continuar',
            cancelButtonText: 'Cancelar'
        }).then((result) => {

            if (result.isConfirmed) {
                __doPostBack('<%= btnFinalizarCompra.UniqueID %>', '');
            }

        });

        return false;
    }

/* ---------- Ventana de JS para mostrar el mensaje de confirmación antes de eliminar un usuario ---------- */
    function confirmarEliminacion(boton, id) {
            event.preventDefault();

        Swal.fire({
            title: "¿Eliminar usuario?",
        text: "Esta acción no se puede deshacer",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#d33",
        cancelButtonColor: "#3085d6",
        confirmButtonText: "Sí, eliminar",
        cancelButtonText: "Cancelar"
            }).then((result) => {
                if (result.isConfirmed) {

              
                    var href = boton.getAttribute("href");
        var match = href && href.match(/__doPostBack\('([^']+)'/);

        if (match && match[1]) {
                        var postBackID = match[1];
        
        __doPostBack(postBackID, "");
                    }
                }
            });
        return false;
    }

/* ---------- Ventana para ver el detalle de un pedido ---------- */
    function verDetallePedido(idCarrito) {

        // Llamamos al WebMethod en el code-behind
        PageMethods.ObtenerDetallePedido(idCarrito, function (response) {

            let html = "<ul>";
            response.forEach(item => {
                html += `
                <li>
                    <strong>ID Artículo:</strong> ${item.IdArticulo}<br>
                    <strong>Cantidad:</strong> ${item.Cantidad}<br>
                    <strong>Precio:</strong> $${item.PrecioUnitario}<br>
                    <strong>Subtotal:</strong> $${(item.Cantidad * item.PrecioUnitario).toFixed(2)}
                </li>
                <hr>
            `;
            });
            html += "</ul>";

            Swal.fire({
                title: 'Detalle del Pedido Nº ' + idCarrito,
                html: html,
                width: 600,
                confirmButtonText: "Cerrar"
            });

        }, function (err) {
            Swal.fire("Error", "No se pudo cargar el pedido", "error");
        });
    }

