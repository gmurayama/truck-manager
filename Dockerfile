FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /appbuild
COPY ["src/Api/Api.csproj", "src/Api/"]
COPY ["src/Application/Application.csproj", "src/Application/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/Persistence/Persistence.csproj", "src/Persistence/"]
RUN dotnet restore "src/Api/Api.csproj"
COPY . .

FROM build AS publish
WORKDIR /appbuild/src/Api
RUN dotnet publish --no-restore "Api.csproj" -c Release -o /app

FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TruckManager.Api.dll"]