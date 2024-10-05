# Epic 1 - Admin Panel with Services

## Before you started

The task requires [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  installed.
You can find project configuration requerarments in [this](AdditionalInfo/project-configuration.md) file

## General requirements


System should support the following features: 
* Get game by key.  
* Get games by genre.  
* Get games by platform.  
* Download a game file.  
* CRUD for games.  
* CRUD for genres.  
* CRUD for platforms.  
* Global error handling.   

Technical specifications:
* Use the latest stable version of ASP.NET CORE (MVC template).  
* N-layer architecture should be used.  
* Use a built-in service provider.  
* Use SOLID principles.  
* Use JSON as response/request format.  
* Use MS SQL Server.  

### Entities
**Game**:
* **Id**: Guid, required, unique  
* **Name**: String, required  
* **Key**: String, required, unique
* **Description**: String, optional

**Genre**:
* **Id**: Guid, required, unique
* **Name**: String, required, unique
* **ParentGenreId**: Guid, optional

**Platform**:
* **Id**: Guid, required, unique
* **Type**: String, required, unique

**GameGenre**:
* **GameId**: Guid, required.
* **GenreId**: Guid, required.

Game-Genre combinations are unique.

**GamePlatform**:
* **GameId**: Guid, required.
* **PlatformId**: Guid, required

Game-Platform combinations are unique.

## Additional requirements
**Game key**
         It is a short unique name of the game that can be used for creating user-friendly routes
         The key can be entered manually. 
         If the key is not entered manually – then it must be generated automatically based on the game name.
**Game File**
         It is an autogenerated .txt file with “<Game name>_<Timestamp>” prefix in the name and serialized game as content.
**Predefined Genres**
         The system should contain predefined genres and subgenres: 
         Strategy
                  RTS 
  	         TBS 
         RPG 
         Sports 
         Races
  	         Rally
  	         Arcade
  	         Formula
  	         Off-road 
         Action
  	         FPS
   	         TPS 
         Adventure 
         Puzzle & Skill 

### Predefined platforms
The system should contain predefined platforms:
* Mobile
* Browser
* Desktop
* Console


## Task Description

Please use the following CI with pipelines, quality gate by code style, and unit-tests coverage for your project:

CI.zip


### US1E1 - User story 1

Add a new Game endpoint
```{xml} 
Url: /games
Type: POST 
Request Example:
{
  "game": {
    "name": "Super game name",
    "key": "gameKey",
    "description": "Some text"
  },
  "genres": [
    "7c9c7b90-363f-487a-bc08-0e9a31e60d2e",
    "acd05e04-a8df-4d6f-9369-b6d6bd40e1ee"
  ],
  "platforms": [
    "34df8f15-ca69-4c44-949a-f53ca4929a72"
  ]
}
```

### US2E1 - User story 2
Get game by key endpoint
```{xml} 
Url: /games/{key}
Type: GET
Response example:
{
  "id": "15d9da6c-1f12-483d-b4ea-97f661c2ce0e",
  "description": "Some text",
  "key": "gameKey",
  "name": "Super game name"
}
```

Get game by id endpoint
```{xml} 
Url: /games/find/{id}
Type: GET
Response example:
{
  "id": "0146978c-cf50-45cd-8949-605938f920a9",
  "description": "string",
  "key": "string",
  "name": "string"
}
```

Get games by platform endpoint
```{xml} 
Url: /platforms/{id}/games
Type: GET
Response example:
[
  {
    "id": "777d5792-3602-423a-8e8d-272a9a22b0fc",
    "description": null,
    "key": "Game1",
    "name": "Game1"
  },
  {
    "id": "4883afcf-3c66-4d44-a622-b8d5d6ced8df",
    "description": null,
    "key": "Game2",
    "name": "Game2"
  }
]
```

Get games by genre endpoint
```{xml} 
Url: /genres/{id}/games
Type: GET
Response example:
[
  {
    "id": "777d5792-3602-423a-8e8d-272a9a22b0fc",
    "description": null,
    "key": "Game1",
    "name": "Game1"
  },
  {
    "id": "4883afcf-3c66-4d44-a622-b8d5d6ced8df",
    "description": null,
    "key": "Game2",
    "name": "Game2"
  }
]
```

### US3E1 - User story 3
Update a Game endpoint
```{xml} 
Url: /games
Type: PUT
Request example:
{
  "game": {
    "name": "New name",
    "key": "newkey",
    "description": "Updated desc",
    "id": "15d9da6c-1f12-483d-b4ea-97f661c2ce0e"
  },
  "genres": [
    "15d9da6c-1f12-483d-b4ea-97f661c2ce0e"
  ],
  "platforms": [
    "61bf29f3-d7f0-4afe-a283-f4c7d8865d96"
  ]
}
```


### US4E1 - User story 4

Delete a Game endpoint  
```{xml} 
Url: /games/{key}  
Type: DELETE  
```

### US5E1 - User story 5
Download a Game endpoint
```{xml} 
Url: /games/{key}/file
Type: GET
Result: file downloading is started
```

### US6E1 - User story 6
Get All games endpoint
```{xml} 
Url: /gamesType: GET
Response example:
[
  {
    "id": "0146978c-cf50-45cd-8949-605938f920a9",
    "description": "string",
    "key": "string",
    "name": "string"
  },
  {
    "id": "777d5792-3602-423a-8e8d-272a9a22b0fc",
    "description": null,
    "key": "Game1",
    "name": "Game1"
  },
  {
    "id": "4883afcf-3c66-4d44-a622-b8d5d6ced8df",
    "description": null,
    "key": "Game2",
    "name": "Game2"
  },
  {
    "id": "15d9da6c-1f12-483d-b4ea-97f661c2ce0e",
    "description": "Updated desc",
    "key": "newkey",
    "name": "New name"
  }
]
```

### US7E1 - User story 7
Add a new Genre endpoint
```{xml} 
Url: /genres
Type: POST
Request example:
{
  "genre": {
    "name": "Sub genre",
    "parentGenreId": "ed1ecab9-358a-4c19-9ab7-6031bda097a9"
  }
}
```


### US8E1 - User story 8
Get genre by id endpoint
```{xml} 
Url: /genres/{Id}
Type: GET
Response Example:
{
  "id": "f668ed18-b5f5-41df-b48d-a9ec6451d55d",
  "name": "genre name 2",
  "parentGenreId": null
}
```

Get all genres endpoint
```{xml} 
Url: /genres
Type: GET
Response Example:
[
  {
    "id": "ed1ecab9-358a-4c19-9ab7-6031bda097a9",
    "name": "string"
  },
  {
    "id": "334b000f-0f8b-417f-b7da-e4549f0f0518",
    "name": "genre name"
  },
  {
    "id": "f668ed18-b5f5-41df-b48d-a9ec6451d55d",
    "name": "genre name 2"
  },
  {
    "id": "2420ef87-c5ff-4354-91e7-b768a4ec3e8b",
    "name": "Sub genre"
  }
]
```

Get genres by game key endpoint
```{xml} 
Url: /games/{key}/genres
Type: GET
Response Example:
[
  {
    "id": "ed1ecab9-358a-4c19-9ab7-6031bda097a9",
    "name": "string"
  }
]
```

Get genres by parent id endpoint
```{xml} 
Url: /genres/{id}/genres
Type: GET
Response Example:
[
  {
    "id": "2420ef87-c5ff-4354-91e7-b768a4ec3e8b",
    "name": "Sub genre"
  }
]
```

### US9E1 - User story 9
Update a Genre endpoint 
```{xml} 
Url: /genres
Type: PUT
Request Example:
{
  "genre": {
    "id": "ed1ecab9-358a-4c19-9ab7-6031bda097a9",
    "name": "Updated Name",
    "parentGenreId": null
  }
}
```


### US10E1 - User story 10
Delete a Genre 
```{xml} 
Url: /genres/{Id}
Type: Delete
```


### US11E1 - User story 11
Create a new Platform endpoint
```{xml} 
Url: /platforms
Type: POST
Request example:
{
  "platform": {
    "type": "Platform Name"
  }
}
```

### US12E1 - User story 12
Get platform by id endpoint
```{xml} 
Url: /platforms/{Id}
Type: GET
Response Example:
{
  "id": "b763be02-652c-4d89-9a65-ec1f1c4106f2",
  "type": "Updated Name"
}
```

Get all platforms endpoint
```{xml} 
Url: /platforms
Type: GET
Response Example:
[
  {
    "id": "61bf29f3-d7f0-4afe-a283-f4c7d8865d96",
    "type": "string"
  },
  {
    "id": "fdd7dae2-76d5-4ee6-ac97-3d21c3d0b41c",
    "type": "Platform Name"
  },
  {
    "id": "b763be02-652c-4d89-9a65-ec1f1c4106f2",
    "type": "Updated Name"
  }
]
```

Get platforms by game key endpoint
```{xml} 
Url: /games/{key}/platforms
Type: GET
Response Example:
[
  {
    "id": "61bf29f3-d7f0-4afe-a283-f4c7d8865d96",
    "type": "string"
  }
]
```

### US13E1 - User story 13
Update a Platform endpoint 

```{xml}
Url: /platforms 
Type: PUT
Request example:
{
  "platform": {
    "id": "b763be02-652c-4d89-9a65-ec1f1c4106f2",
    "type": "Updated Name"
  }
}
```

### US14E1 - User story 14
Delete a Platform endpoint
```{xml} 
Url: /platforms/{Id}
Type: DELETE
```


### US15E1 - User story 15
 Implement global exception handling
 
 
## Non-functional requirement (Optional)

**NFR1E1**
       Repository and Unit of Work patterns should be implemented.  
**NFR2E1**
       Use the latest stable version of Microsoft Entity framework Core.   
**NFR3E1**
       Add GET endpoints responses caching for 1 minute.  



#### 3.2. Owner Service

Implement the [GetAccountOwnersTotalBalance](BankSystem.Services/Services/OwnerService.cs) method 

Please use the following CI with pipelines, quality gate by code style, and unit-tests coverage for your project:

CI.zip