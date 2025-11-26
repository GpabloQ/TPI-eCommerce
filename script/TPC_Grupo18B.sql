create database TPC_Grupo18B

use TPC_Grupo18B

go
create table CATEGORIAS (
	IdCategoria bigint not null primary key identity (1,1),
	Nombre varchar(100) not null,
	Estado bit null
);

go
create table MARCAS (
	IdMarca bigint not null primary key identity (1,1),
	Nombre varchar(100) not null,
	Estado bit null
);


go
create table TIPOUSUARIOS(
	IdTipoUsuario bigint not null primary key identity (1,1),
	Tipo varchar(100) not null
);

go
create table USUARIOS (
	IdUsuario bigint not null primary key identity (1,1),
	Nombre varchar(100) not null,
	Apellido varchar(100) not null,
	FechaNacimiento date not null,
	Mail varchar(150) unique,
	Contrasenia varchar(250) not null,
	Telefono varchar(50) not null,
	DNI varchar(20) not null,
	TipoUsuario bigint null foreign key references TIPOUSUARIOS(IdTipoUsuario),
	Estado bit null,
	IdDocimicilio bigint null foreign key references DOMICILIOS(IdDomicilio)
);


go
create table ARTICULOS (
	IdArticulo bigint not null primary key identity(1,1),
	Codigo varchar (50) not null,
	Cantidad bigint null,
	Precio money not null,
	IdCategoria bigint not null foreign key references CATEGORIAS(IdCategoria),
	IdMarca bigint not null foreign key references MARCAS (IdMarca), 
	Nombre varchar (150) not null,
	Descripcion varchar(3000) null,
	Estado bit null
);

go 
create table IMAGENES (
	IdImagen bigint not null primary key identity (1,1),
	IdArticulo bigint not null foreign key references ARTICULOS (IdArticulo),
	UrlImagen varchar (250) null
);

go
create table DOMICILIOS (
	IdDomicilio bigint not null primary key identity (1,1),
	Calle varchar(100) not null,
	Ciudad varchar(100) not null,
	Departamento varchar (100) null,
	Numero bigint not null,
	Piso bigint null, 
	Provincia varchar(100) not null,
	CodigoPostal bigint not null,
	Estado bit null
);

go
create table CARRITO(
	IdCarrito bigint not null primary key identity (1,1),
	IdUsuario bigint not null foreign key references USUARIOS (IdUsuario),
	FechaCreacion date,
	EstadoCarrito varchar(50),
	Estado varchar (20) null

);

go
create table ELEMENTOCARRITO (
	IdElemento bigint not null primary key identity (1,1),
	IdCarrito bigint not null foreign key references CARRITO (IdCarrito), 
	IdArticulo bigint not null foreign key references ARTICULOS (IdArticulo),
	Cantidad bigint,
	PrecioUnitario decimal(18,2)
);


