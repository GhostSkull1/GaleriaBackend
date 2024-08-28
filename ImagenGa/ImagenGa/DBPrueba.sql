create database DBPrueba
go
use DBPrueba

go

create table Usuario(
IdUsuario int primary key identity,
Nombre varchar(50),
Correo varchar(50),
Clave varchar(100)
)

go

create table Imagen(
IdImagen int primary key identity,
Nombre varchar(50),
Descripcion varchar(50),
FechaC Datetime,
FechaE Datetime,
ImagenUrl varchar(50)
)
go

create table Registro(
Fecha Datetime,
IdRegistro int primary key identity,
IdImagen int,
IdUsuario int,
accion varchar(10)

constraint FK_Imagen_Registro foreign key (IdImagen) references Imagen(IdImagen),
constraint FK_Usuario_Registro foreign key (IdUsuario) references Usuario(IdUsuario)
)

create table Rol(
IdRol int primary key identity,
Nombre varchar(50)
)

create table Accion(
IdAccion int primary key identity,
Nombre varchar(50)
)

alter table Registro 
Add constraint FK_RegistroOpcion foreign key (IdAccion) references Accion(IdAccion)

delete from Registro

alter table Usuario
add IdRol int 
constraint FK_UsuarioRol foreign key (IdRol) references Rol(IdRol)

insert into Rol(Nombre) values('Administrador')
insert into Rol(Nombre) values('Estandar')

select * from Registro

alter table Registro drop column accion

insert into Accion(Nombre)
values ('crear')

insert into Accion(Nombre)
values ('eliminar')

insert into Usuario(Nombre, Correo, Clave, IdRol) values('jonny', 'jackltd@hotmail.com', '123', 1)
insert into Usuario(Nombre, Correo, Clave, IdRol) values('alexander', 'jackltd@gmail.com', '123', 2)

select * from Registro

delete from Imagen where IdImagen = 3 or IdImagen = 4

SELECT NEWID()