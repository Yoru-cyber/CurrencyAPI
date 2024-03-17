FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /App

#Stage 1
COPY ./app ./
RUN dotnet restore
RUN dotnet publish -c release -o out

#Stage 2
FROM mcr.Microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build /App/out .
EXPOSE 5135
CMD ["dotnet", "DolarAPI.dll"]
