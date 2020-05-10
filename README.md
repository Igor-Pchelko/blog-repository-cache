This project demonstrates implementation of repository with cache on dotnet core which can work within TransactionScope.

[dotnet database Repository with Cache blog post](https://pcholko.com/posts/2020-05-10/dotnet-database-repository-with-cache/)

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
