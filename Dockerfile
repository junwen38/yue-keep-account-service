FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /source
COPY ./ .
RUN dotnet publish -o publish -c release

FROM mcr.microsoft.com/dotnet/aspnet:3.1
EXPOSE 5000
ENV AC_DB_HOST=database.cloud
ENV AC_DB_NAME=accountbook_test
ENV AC_DB_USERNAME=junwen38
ENV AC_DB_PASSWORD=81356450
ENV TZ=Asia/Shanghai
COPY --from=build /source/publish/ /app
WORKDIR /app
ENTRYPOINT ["dotnet", "yue-keep-account-service.dll"]
