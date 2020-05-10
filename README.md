This project demonstrates implementation of repository with cache on dotnet core which can work within TransactionScope.

# Test project in local environment

Start MSSQL database via docker-compose command line:
```shell
docker-compose up -d
```

Run tests:
```shell
dotnet test test/RepositoryTest
```

Cleanup MSSQL database:
```shell
docker-compose down
```
