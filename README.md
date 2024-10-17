# Brunda-Minimal

A smaller console application that combines the power of me (Brian) and Funda to make "Brunda-Minimal".
"Brunda-Minimal" only exists out of one .NET project to reduce the verbosity and complexity of the application.

## What does it do?

This application will create a top 10 of all real estate agents in **Amsterdam**.
It will do this twice for the same area; one for **all** available properties and one time for all available properties with a garden.

In comparison to it's bigger brother [Brunda](https://github.com/Brivy/brunda), it will not save the results to the database. 
Instead it will only print it them to the console when it's done. 

## How do I run it?

Simply supply the `PartnerApi:ApiKey` and start te application.
The process can take up to two minutes depending on the offers in Amsterdam.

## Results

At the moment (17/10/24), "Brunda-Minimal" generates the following output:

```bash
TOP 10 REAL ESTATE AGENTS THAT SELL THE MOST PROPERTIES IN AMSTERDAM
---------------------------------------------------
1: 'Heeren Makelaars' with 120 properties for sale
2: 'Broersma Wonen' with 117 properties for sale
3: 'Ramón Mossel Makelaardij o.g. B.V.' with 84 properties for sale
4: 'Eefje Voogd Makelaardij' with 77 properties for sale
5: 'Carla van den Brink B.V.' with 72 properties for sale
6: 'Makelaar Van der Linden Amsterdam' with 61 properties for sale
7: 'MAKELAAR BERT' with 61 properties for sale
8: 'De Graaf & Groot Makelaars' with 60 properties for sale
9: 'Smit & Heinen Makelaars en Taxateurs o/z' with 60 properties for sale
10: 'Hallie & Van Klooster Makelaardij' with 59 properties for sale

TOP 10 REAL ESTATE AGENTS THAT SELL THE MOST PROPERTIES WITH A GARDEN IN AMSTERDAM
---------------------------------------------------
1: 'Broersma Wonen' with 38 properties for sale
2: 'Heeren Makelaars' with 32 properties for sale
3: 'Carla van den Brink B.V.' with 24 properties for sale
4: 'Ramón Mossel Makelaardij o.g. B.V.' with 20 properties for sale
5: 'Makelaarsland' with 17 properties for sale
6: 'KIJCK. makelaars Amsterdam' with 16 properties for sale
7: 'Hoekstra en Van Eck Amsterdam Noord' with 14 properties for sale
8: 'SEM makelaars' with 12 properties for sale
9: 'Smit & Heinen Makelaars en Taxateurs o/z' with 12 properties for sale
10: 'Linger OG Makelaars en Taxateurs' with 12 properties for sale
```
