# MoviesAPI
This is a simple movie API made with C# and ASP.NET Core 6.0.
- It contains DTOs and it uses MySql to persist data
It is also fully documented with swagger, so you can acess it with {url}/swagger.
Contains GET, POST, PUT, PATCH and DELETE methods, so it is a complete CRUD.
GET {url}/api/movie -> Returns all Movies.
GET {url}/api/movie/{id} -> Return one movie with the specified {id}.
POST {url}/api/movie -> Creates one movie.
PUT {url}/api/movie/{id} ->Updates the whole 'movie' object of the specified {id}.
PATCH {url}/api/movie/{id} -> Also updates one movie, but parcially.
DELETE {url}/api/movie/{id} -> Deletes the specified id.
Observations:
{url} -> The url of the host of the API
{id} -> Param that can be used find, update and delete a movie. It's basically an unique identifier for each movie
