create database DBImagenGaleria
go
use DBImagenGaleria

go

create table ROL(
IdRol int primary key,
DescripcionRol varchar(50)
)
go

insert into ROL(IdRol, DescripcionRol) values(1,'Administrador')
insert into ROL(IdRol, DescripcionRol) values(2,'empleado')

select * from ROL

go

--create table Usuario(
--IdUsuario int primary key identity,
--Nombre varchar(50),
--Correo varchar(50),
--Clave varchar(100),
--IdRol int references ROL(IdRol)
--)

create table Usuario(
IdUsuario int primary key identity,
Nombre varchar(50),
Correo varchar(50),
Clave varchar(100)
)

go

create table Producto(
IdProducto int primary key identity,
Nombre varchar(50),
Descripcion varchar(50),
FechaE Datetime,
ImagenUrl varchar(50)
)
go

create table ImagenElimi(
IdImagenElimi int primary key identity,
FechaB datetime,
DescripcionB varchar(50)
)



insert into Usuario(Nombre, Correo, Clave, IdRol) values('jonny', 'jackltd@hotmail.com', '123', 1)
insert into Usuario(Nombre, Correo, Clave, IdRol) values('alexander', 'jackltd@gmail.com', '123', 2)

select * from Producto

select NEWID()

select * from Usuario