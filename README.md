# A POC showcasing timely running service with SQL Database storage

A proof of concept showcasing .Net service running in container with SQL database and acceptance tests executing against it

# Article on Medium

- [How to create a timely running service in .NET 7](https://iqan.medium.com/how-to-create-a-timely-running-service-in-net-7-3af70d8494d1)

- [How to create a timely running service in .NET Core](https://iqan.medium.com/how-to-create-a-timely-running-service-in-net-core-757f445035ca)

## .netcore 2.1

If you are after outdated .netcore 2.1 implementation, you can refer to the tag [dotnetcore-2.1](https://github.com/iqan/service-testing-docker-poc/tree/dotnetcore-2.1)

## To run
- Go to src: `cd src/`
- Build docker containers `docker-compose build`
- Run tests in compose `docker-compose up --exit-code-from dummyservice.tests`

## Screenshots
![final-output](./final-output.PNG)
