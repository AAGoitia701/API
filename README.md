# API Project

Welcome to the API Project! This application provides a RESTful API for providing stocks information, and the comments from the users of these stocks.

## Table of Contents

- [Features](#features)
- [Technologies Used](#technologies-used)
- [Usage](#usage)
- [API Endpoints](#api-endpoints)
- [License](#license)

## Features

- RESTful API design
- Supports CRUD operations, Search, Pagination, Sort.

## Technologies Used

- **Backend:** C#, .NET Core, Entity Framework Core. 
- **Database:** SQL. 

## Usage
Use tools like Postman or cURL to interact with the API.

## API Endpoints
Here are some example endpoints:

- GET /api/stock - Retrieves a list of stocks
- GET /api/stock?Symbol=<symbolStock> - Retrieves a list of stocks with that symbol
- GET /api/stock?CompanyName=<NameOfCompany> - Retrieves a list of stocks that has that name of company
- POST /api/stock - Creates a new stock
- GET /api/stock/id - Retrieves a specific stock by ID
- PUT /api/stock/id - Updates a specific stock by ID
- DELETE /api/stock/id - Deletes a specific stock by ID

- GET /api/comment - Retrieves a list of comments
- POST /api/comment - Creates a new comment
- GET /api/comment/id - Retrieves a specific comment by ID
- PUT /api/comment/id - Updates a specific comment by ID
- DELETE /api/comment/id - Deletes a specific comment by ID

## License
This project is licensed under the MIT License - see the LICENSE file for details.
