<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QuienesSomos.aspx.cs" Inherits="WebAppEcommerce.QuienesSomos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@400;600;700;800&display=swap" rel="stylesheet">

    <style>
        @import url('https://fonts.googleapis.com/css2?family=Montserrat:wght@400;600;700;800&display=swap');

        .conteiner {
            max-width: 900px;
            margin: 0 auto;
            padding: 40px 20px;
            font-family: 'Montserrat', sans-serif;
        }
        .CuadroTitulo {
            display: flex;
            align-items: center;
            justify-content: center;
            margin: 10px 0 40px 0;
        }


        .titulo {
            font-family: 'Montserrat', sans-serif;
            font-size: 26px;
            font-weight: 800;
            letter-spacing: 2px;
            margin: 0 20px;
            text-transform: uppercase;
            color: #222;

        }
        .linea {
            flex: 1;
            height: 1px;
            background: #e5e5e5;
        }


        .Seccion {
            margin-bottom: 50px;
        }

        .subtitulo {
            font-size: 28px;
            font-weight: 800;
            margin-bottom: 15px;
        }

        .Seccion p {
            font-size: 17px;
            line-height: 1.7;
            margin-bottom: 15px;
        }

        .puntosClaves {
            margin-top: 15px;
            padding-left: 20px;
        }

            .puntosClaves li {
                font-size: 17px;
                margin-bottom: 8px;
            }
    </style>



    <div class="conteiner">

        <div class="CuadroTitulo">
            <div class="linea"></div>
            <h1 class="titulo">QUIENES SOMOS </h1>
            <div class="linea"></div>
        </div>

        <div class="Seccion">
            <h2 class="subtitulo">CONOCE NUESTRO NEGOCIO</h2>
            <p>
                Bienvenidos a nuestra página “Quiénes Somos”, donde te invitamos a descubrir la esencia de nuestro emprendimiento. 
            Aquí compartimos no solo quiénes somos, sino también la pasión que nos impulsa a ofrecerte los mejores productos.
            </p>
        </div>
        <div class="Seccion">
            <h2 class="subtitulo">NUESTRA HISTORIA</h2>
            <p>
                Desde nuestros inicios, el objetivo fue claro: traer calidad y autenticidad a cada uno de nuestros productos.
            Nuestra historia comenzó hace más de un año, cuando un grupo de amigos, unidos por una pasión común, decidimos lanzar este proyecto.
            Cada producto que ofrecemos es el resultado de un profundo amor por lo que hacemos.
            </p>
        </div>

        <div class="Seccion">
            <h2 class="subtitulo">¿POR QUE ELEGIMOS ESTOS PRODUCTOS ?</h2>
            <p>
                En nuestra empresa, nos esforzamos por ofrecerte componentes electrónicos de la más alta calidad. 
            Trabajamos con proveedores confiables y reconocidos para garantizar que cada producto cumpla con los estándares que necesitas para tus proyectos, ya sean personales, educativos o industriales.

            Además, contamos con un amplio catálogo de componentes, desde los más básicos hasta los más especializados,
            porque entendemos la importancia de tener todo lo que necesitas en un solo lugar. 
            Nuestro objetivo es ser más que un punto de venta: buscamos ser tu mejor aliado, 
            brindándote soluciones rápidas y efectivas para que tus ideas nunca se detengan.
            </p>
        </div>

        <div class="Seccion">
            <h2 class="subtitulo">NUESTRO EQUIPO</h2>
            <p>
                En nuestro equipo, contamos con personas apasionadas y expertas en diferentes áreas. 
        Desde diseñadores hasta especialistas en atención al cliente, cada miembro aporta su talento y dedicación. Juntos, formamos una familia que trabaja incansablemente para brindarte la mejor experiencia de compra.
            </p>

            <ul class="puntosClaves">
                <li>Creatividad: Nuestros diseñadores están constantemente innovando. </li>
                <li>Calidad: Cada producto es seleccionado cuidadosamente </li>
                <li>Atención al cliente: Nuestro equipo está siempre disponible para resolver tus dudas.</li>
            </ul>

            <p>
                Te invitamos a ser parte de nuestra historia y a explorar nuestros productos ¡Gracias por acompañarnos en este viaje!
            </p>

        </div>
    </div>


</asp:Content>
