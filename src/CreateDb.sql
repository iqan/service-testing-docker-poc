create database DummyDatabase;
Go;

create table MessageData
(
MessageDataId int IDENTITY(1,1) primary key,
MessageText varchar(200) not null
);
Go;