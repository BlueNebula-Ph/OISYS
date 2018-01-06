FROM microsoft/aspnetcore:1.1.2

COPY ./Oisys.Web /Oisys
WORKDIR /Oisys

ENV ASPNETCORE_ENVIRONMENT=Staging

CMD ASPNETCORE_URLS=http://*:$PORT dotnet Oisys.Web.dll