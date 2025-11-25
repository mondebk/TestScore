# Introduction

Welcome to Test Scorer - my submission for the technical assessment. In this solution, you will find two projects which
solve for the need for a user to be able to

## How to run the Test Scoring

### Pre-requirements

- .NET 8 SDK and runtime

In order to run the solution, the following steps must be followed:

- Restore NuGet packages
- Build the solution and run either the console app or api
- Select either TestScorer.Api or TestScorer.ConsoleApp as your startup project
- Run the project

> NB: On first execution, a SQLite database will be created in C:\Users\<user>
> \AppData\Local\TestScoring\testscoringapp.db.
> This initialisation process will trigger regardless of the startup project chosen.

___

# Project Details

## Technical Details

- Developed using C#
- Employed clean architecture, SOLID principles, CQRS and domain-driven design principles
- Made use of the Entity Framework, Swagger, SQLite and Microsoft's dependency injection NuGet packages.

## Project Structure

### TestScorer.Domain

The core of the Test Scorer solution. In this project, you will find the domain entities, interfaces for defining how test scores will be ingested, persisted and retrieved. All fundamental business logic related to test scoring will be housed in this project.

### TestScorer.Infrastructure

This project represents the layer responsible for implementing interfaces in the **domain** which speak to file handling and persistance. The database configuration and mapping of the database to domain entities are handled in this layer.

### TestScorer.Application

This layer serves as an abstraction between the Api and ConsoleApps, and the infrastructure and Domain layers. It is responsible for providing services to presentation-level layers, which will orchestrate taking in request data, processing, and mappings to presentation-layer-ready models to avoid leaking domain internals.

### TestScorer.ConsoleApp

A console application which allows a user to specify a file path to a csv or txt file containing test scores.

### TestScorer.Api

An API project containing various endpoints allowing users to upload test score files and retrieve data.

## Assumptions

### File

- The structure of the file will never change, i.e. a header row will always be present, and the columns will remain.
- The file may not always be a .csv

### Search Student Endpoint

- The GET endpoint for searching a student may find more than one student depending the user input.
- The search needs to be executed against both their first and last names
- More than one score for a student may exist in the database - return their top score
- Students must be returned in alphabetical order

### Get Top Score Endpoint

- All students who share the top score should be listed
- The students are listed in a collection in alphabetical order

### Student Ordering

- Ordering should occur on the first name and then the last name.

---

# Assignment Questions

## How would I secure the endpoints?

I would make use of a bearer token authorisation on each endpoint, as they provide a safe (short-lived, validated per
request, and can detect tampering) and flexible. A couple of examples for how we could generate the token are either by having a custom logic system which would authenticate the user, retrieve their claims and wrap them in the token, or we
could opt to use an OAuth provider.

## What would I use to build the front-end?

With the current requirements in mind, I would elect to use **React** as it is a modern, well documented and mature UI
library. Thanks to React's flexibility, it will also scale with the solution's increasing scope and complexity over time by allowing us to make use of libraries to service our growing needs.

React also has a steady learning curve thanks to TypeScript, which shares some core ideas with C# (strongly typed languages, OOP, generics) and its component-based nature.

## Which cloud components would I use to host the application?

For the main app hosting, I'd make use of Azure App Services. Because Azure App Services abstracts
hosting-related concerns (server patching, updates, and SSL renewals), making it ideal for lightweight apps like this one. With the assumption that a React app will be created to interface with the API written, App Services natively supports hosting front-end and back-end stacks, whilst also supporting deployments from various sources (Azure DevOps
pipeline, GitHub action, FTP, Docker).

Two crucial offerings provided by Azure App Services are:

- Monitoring: we would be able to see all incidents in a central location.
- Auto-scaling: scaling rules can be set up to either scale horizontally or vertically, depending on load and general usage patterns.

Should we elect a cloud-hosted database, I would recommend an Azure SQL Database, as, in practice, it is similar to a SQL Server database, but with the benefit of being a PaaS, we'd have access to monitoring, backups and geo-redundancies and
automatic scaling.