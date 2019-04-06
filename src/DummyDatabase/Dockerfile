FROM microsoft/mssql-server-linux

RUN mkdir /work 

COPY CreateDb.sql /work
COPY db.sh /work
COPY db.bak /work

WORKDIR /work

RUN chmod 755 db.sh

RUN ./db.sh CreateDb.sql