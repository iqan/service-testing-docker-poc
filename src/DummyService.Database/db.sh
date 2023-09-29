echo "Setting Environment variables."
export ACCEPT_EULA=Y
export MSSQL_SA_PASSWORD=SuperSecretPassword#1
echo "Environment variables set."
echo "Starting SqlServr"
/opt/mssql/bin/sqlservr &
sleep 60 | echo "Waiting for 60s to start Sql Server"
echo "Restoring DB."
/opt/mssql-tools/bin/sqlcmd -U sa -P SuperSecretPassword#1 -i $1
echo "DB restored."