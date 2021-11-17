#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /appguilhermerocha
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY GuilhermeRocha.sln ./
COPY GuilhermeRocha.API/*.csproj ./GuilhermeRocha.API/
COPY GuilhermeRocha.Infrastructure/*.csproj ./GuilhermeRocha.Infrastructure/
COPY GuilhermeRocha.Test/*.csproj ./GuilhermeRocha.Test/

RUN dotnet restore 
COPY . .

WORKDIR "/src/GuilhermeRocha.API"
RUN dotnet build -c Release -o /appguilhermerocha

WORKDIR "/src/GuilhermeRocha.Infrastructure"
RUN dotnet build -c Release -o /appguilhermerocha

WORKDIR "/src/GuilhermeRocha.Test"
RUN dotnet build -c Release -o /appguilhermerocha

FROM build AS publish
RUN dotnet publish -c Release -o /appguilhermerocha 

FROM base AS final
WORKDIR /app
COPY --from=publish /appguilhermerocha .
ENTRYPOINT ["dotnet", "GuilhermeRocha.API.dll"]