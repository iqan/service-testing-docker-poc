FROM mcr.microsoft.com/mssql/server:2022-CU13-ubuntu-22.04

WORKDIR /work

COPY DummyService.Database/CreateDb.sql .
COPY DummyService.Database/db.sh .
COPY DummyService.Database/db.bak .

RUN ./db.sh CreateDb.sql
