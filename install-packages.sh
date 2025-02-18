#!/bin/bash

dotnet add package Azure.Maps.Search --prerelease
dotnet add package DnsClient

# due to https://github.com/advisories/GHSA-8g4q-xg66-9fp4
dotnet add package System.Text.Json	--version 8.0.5