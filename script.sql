create database dbPw;
use dbPw;

create table tbUser(
IdUser int primary key auto_increment,
Nome varchar(50) not null,
Email varchar(100) not null,
Senha varchar(20) not null
);

create table tbProduct(
IdProd int primary key auto_increment,
Nome varchar(50) not null,
Descricao varchar(150) not null,
Preco decimal(4,2) not null,
Quant int not null
);