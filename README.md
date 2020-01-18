# SmartTravels

## About
A Web application for comparing the prices and arrival times of various transportation services.
Currently works for Lyft and Uber ETAs for drivers, and cost of all services.

## How it Works (High level overview)
 1. Uses Google API endpoints to display autocomplete suggestions for Starting and Destination Addresses
 2. Converts addresses to Long/Lat coordinates
 3. Sends GET Requests to Uber & Lyft APIs for ETAs and services
 
 ##Languages
 - C# (Backend)
 - Razor Markup (Frontend)

## Libraries
- .NET Core
- Newtonsoft.JSON

## APIs Used
- Google (Geocoding, Places)
- Uber (ETAs, price)
- Lyft (ETAs, price)


