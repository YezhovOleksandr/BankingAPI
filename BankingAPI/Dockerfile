FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Use the exact, real names from your repo (note the lowercase "i"!)
COPY BankingAPI.sln ./
COPY BankingAPI/BankingAPI.csproj BankingAPI/
COPY BankingAPI.Common/BankingAPI.Common.csproj BankingAPI.Common/
COPY BankingAPI.Domain/BankingAPI.Domain.csproj BankingAPI.Domain/
COPY BankingAPi.Infrastructure/BankingAPi.Infrastructure.csproj BankingAPi.Infrastructure/

RUN dotnet restore

# After restore, copy everything else
COPY . .
WORKDIR /src/BankingAPI
RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
EXPOSE 80
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "BankingAPI.dll"]
