#!/bin/bash

# Add a new EF Core migration with the specified name and output directory
echo "Adding EF Core migration 'seedGeneres' to Data/Migrations..."
dotnet ef migrations add seedGeneres --output-dir Data/Migrations

# Check if the previous command was successful
if [ $? -eq 0 ]; then
    echo "Migration added successfully. Running the application..."
    # Apply the migrations to the database and run the application
    dotnet run
else
    echo "Failed to add migration. Aborting application run."
    exit 1
fi
