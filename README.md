# Introduction

## How to run the Test Scoring

### Requirements

In order to run the solution, the following steps must be followed:
- restore nuget packages
- verify that the database has been created
- build the solution and run either the console app or api

> NB: The database will be created in C:\Users\<user>\AppData\Local\TestScoring\testscoringapp.db

### Test Scoring Console App

### Test Scoring API (Swagger)

# Project Details

## Technical Details

- Developed using C#
- Employed clean architecture, CQRS and domain driven design principles
- .NET 8
- EntityFramework
- SQLite 

## Assumptions

### File

- The format of the file will never change i.e. a header row will always be present and the columns will remain, but the file may not always be a .csv.

### Search Student Endpoint
- The GET endpoint for searching a student may find more than one student depending the user input.  
- The search needs to be executed against both their first and last names
- More than one score for a student may exist in the database - return their top score

# Assignment Questions

## How would I secure the endpoints?

## What would I use to build the front-end?

## Which cloud components would I use to host the application?
