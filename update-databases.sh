#!/bin/bash

echo "Updating Europe..."
dotnet ef database update \
  --connection "Host=localhost;Port=5432;Database=ArticleDatabase;User Id=postgres;Password=postgres"

echo "Updating Asia..."
dotnet ef database update \
  --connection "Host=localhost;Port=5433;Database=ArticleDatabase;User Id=postgres;Password=postgres"

echo "Updating Africa..."
dotnet ef database update \
  --connection "Host=localhost;Port=5434;Database=ArticleDatabase;User Id=postgres;Password=postgres"

echo "Updating North America..."
dotnet ef database update \
  --connection "Host=localhost;Port=5435;Database=ArticleDatabase;User Id=postgres;Password=postgres"

echo "Updating South America..."
dotnet ef database update \
  --connection "Host=localhost;Port=5436;Database=ArticleDatabase;User Id=postgres;Password=postgres"

echo "Updating Australia..."
dotnet ef database update \
  --connection "Host=localhost;Port=5437;Database=ArticleDatabase;User Id=postgres;Password=postgres"

echo "Updating Antarctica..."
dotnet ef database update \
  --connection "Host=localhost;Port=5438;Database=ArticleDatabase;User Id=postgres;Password=postgres"

echo "Updating Global..."
dotnet ef database update \
  --connection "Host=localhost;Port=5439;Database=ArticleDatabase;User Id=postgres;Password=postgres"

echo "All databases updated."