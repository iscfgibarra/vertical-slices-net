# Diamonds API

A RESTful API for managing diamonds using vertical slice architecture in .NET.

## Features

- Get all diamonds with pagination
- Get diamond by ID
- Update diamond
- Delete diamond

## API Endpoints

### Get All Diamonds

```http
GET /api/diamonds
```

Query Parameters:
- `page` (optional): Page number (default: 1)
- `size` (optional): Number of items per page (default: 10)

Response:
```json
{
    "items": [
        {
            "id": 1,
            "name": "Diamond 1",
            "color": "White",
            "price": 1000.00,
            "carat": 1.0
        }
    ],
    "totalCount": 100,
    "page": 1,
    "size": 10,
    "totalPages": 10
}
```

### Get Diamond by ID

```http
GET /api/diamonds/{id}
```

Path Parameters:
- `id`: Diamond ID

Response:
```json
{
    "id": 1,
    "name": "Diamond 1",
    "color": "White",
    "price": 1000.00,
    "carat": 1.0
}
```

Status Codes:
- 200: Diamond found
- 404: Diamond not found

### Update Diamond

```http
PUT /api/diamonds/{id}
```

Path Parameters:
- `id`: Diamond ID

Request Body:
```json
{
    "name": "Updated Diamond",
    "color": "Blue",
    "price": 1500.00,
    "carat": 1.5
}
```

Note: All fields are optional. Only provided fields will be updated.

Status Codes:
- 204: Diamond updated successfully
- 404: Diamond not found

### Delete Diamond

```http
DELETE /api/diamonds/{id}
```

Path Parameters:
- `id`: Diamond ID

Status Codes:
- 204: Diamond deleted successfully
- 404: Diamond not found

## Architecture

This project uses vertical slice architecture, where each feature is self-contained and includes:
- Endpoint definition
- Request/Response models
- Command/Query handlers
- Business logic

Each feature is implemented as a vertical slice, making the codebase more maintainable and easier to understand.

## Project Structure

```
DiamondsApi/
├── Features/
│   └── Diamonds/
│       ├── Commands/
│       │   ├── UpdateDiamond.cs
│       │   └── DeleteDiamond.cs
│       └── Queries/
│           ├── GetDiamonds.cs
│           └── GetDiamondById.cs
├── Infrastructure/
│   └── DiamondDbContext.cs
├── Models/
│   └── Diamond.cs
└── Shared/
    └── Slices/
        └── ISlice.cs
```

## Getting Started

1. Clone the repository
2. Restore NuGet packages
3. Update the connection string in `appsettings.json`
4. Run the application

## Dependencies

### Core Dependencies
- .NET 9.0 or later
- Entity Framework Core 9.0
- Microsoft.AspNetCore.Mvc 9.0
- Microsoft.EntityFrameworkCore 9.0
- Microsoft.EntityFrameworkCore.SqlServer 9.0
- Microsoft.EntityFrameworkCore.Tools 9.0

### Development Dependencies
- Swashbuckle.AspNetCore 6.5.0 (for Swagger/OpenAPI documentation)
- Microsoft.EntityFrameworkCore.Design 9.0
- Microsoft.VisualStudio.Web.CodeGeneration.Design 9.0

### Optional Dependencies
- Microsoft.EntityFrameworkCore.InMemory 9.0 (for testing)
- Microsoft.AspNetCore.Mvc.Testing 9.0 (for API testing)
- xUnit 2.6.6 (for unit testing)
- Moq 4.20.70 (for mocking in tests) 