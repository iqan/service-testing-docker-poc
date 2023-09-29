USE master
GO

RESTORE DATABASE DummyDatabase 
FROM DISK = '/work/db.bak' WITH 
MOVE 'DummyDatabase' TO '/var/opt/mssql/data/DummyDatabase/DummyDatabase.mdf',
MOVE 'DummyDatabase_log' TO '/var/opt/mssql/data/DummyDatabase/DummyDatabase_log.ldf'
GO
