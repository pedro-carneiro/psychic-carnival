# Toggle API
Toggle API is the web service responsible for managing feature toggles within
XPTO. This service maintains toggle values state, so that applications that
consume it can have their values change dynamically.

## API Documentation
The Toggle API is split into the two main resources it manages.

The first one is the toggle resource. A toggle is comprised of a name,
a default boolean value and a persistence identifier.

The next one is the application override. An application override is used when
a toggle is required to have a different value than the default, but only for a
specific application. It is comprised of an application reference, a toggle
identifier and a value for the toggle.

### Toggles

#### Get all toggles
```
GET api/toggle
```
##### Successful response

Code: `200 OK`

Content:
```json
[
  {
    "id": 1,
    "name": "myToggle",
    "defaultValue": true
  },
  {
    "id": 2,
    "name": "otherToggle",
    "defaultValue": false
  }
]
```

#### Get toggle
```
GET api/toggle/{id}
```
##### Successful response

Code: `200 OK`

Content:
```json
{
  "id": 1,
  "name": "myToggle",
  "defaultValue": true
}
```
##### Unsuccessful response

Code: `404 Not Found`

#### Create toggle
```
POST api/toggle
```
Content:
```json
{
  "name": "feature",
  "defaultValue": true
}
```
##### Successful response
Code: `201 Created`

Content:
```json
{
  "id": 1,
  "name": "myToggle",
  "defaultValue": true
}
```

#### Update toggle
```
PUT api/toggle/{id}
```
Content:
```json
{
  "id": 1,
  "name": "feature",
  "defaultValue": true
}
```
##### Successful response
Code: `204 No Content`
##### Unsuccessful response
Code: `404 Not Found`

Code: `400 Bad Request` (usually when `id` differs from content and URL)

#### Delete toggle
```
DELETE api/toggle/{id}
```
##### Successful response
Code: `204 No Content`
##### Unsuccessful response
Code: `404 Not Found`

### Application Overrides

#### Get all application overrides
```
GET api/application/{application}/toggle
```
##### Successful response

Code: `200 OK`

Content:
```json
[
  {
    "toggleId": 1,
    "toggleName": "feature1",
    "value": true
  },
  {
    "toggleId": 2,
    "toggleName": "myToggle",
    "value": true
  }
]
```

#### Get application override
```
GET api/application/{application}/toggle/{toggleId}
```
##### Successful response

Code: `200 OK`

Content:
```json
{
  "toggleId": 1,
  "toggleName": "myToggle",
  "value": true
}
```
##### Unsuccessful response

Code: `404 Not Found`

#### Create application override
```
POST api/application/{application}/toggle
```
Content:
```json
{
  "toggleId": 1,
  "toggleName": "myToggle",
  "value": true
}
```
##### Successful response
Code: `201 Created`

Content:
```json
{
  "toggleId": 1,
  "toggleName": "myToggle",
  "value": true
}
```

#### Update application override
```
PUT api/application/{application}/toggle/{toggleId}
```
Content:
```json
{
  "toggleId": 1,
  "toggleName": "myToggle",
  "value": true
}
```
##### Successful response
Code: `204 No Content`
##### Unsuccessful response
Code: `404 Not Found`

Code: `400 Bad Request`

#### Delete application override
```
DELETE api/application/{application}/toggle/{toggleId}
```
##### Successful response
Code: `204 No Content`
##### Unsuccessful response
Code: `404 Not Found`

## Development
Toggle API is a .NET Core 2.0 application.
### Requirements

- [.NET Core 2.0](https://www.microsoft.com/net/download/core)

### Getting started

- Clone/fork the repository
- Build from source using `dotnet build`
- Execute the tests using `dotnet test`
- Run the application locally `dotnet run --project src/ToggleApi/ToggleApi.csproj`
- Debug using your .NET IDE of choice
