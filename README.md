# 015 Task Based Async Pattern

## Lecture

[![# Task Based Async Pattern(Part 1)](https://img.youtube.com/vi/uWclR0QwWZs/0.jpg)](https://www.youtube.com/watch?v=uWclR0QwWZs)

[![# Task Based Async Pattern(Part 2)](https://img.youtube.com/vi/ngIsgdpnARU/0.jpg)](https://www.youtube.com/watch?v=ngIsgdpnARU)

## Instructions

In `HomeEnergyApi/Services/ZipLocationService.cs`
- Create a public class `ZipLocationService`
    - Create a property of `httpClient` of type `HttpClient`
    - Create a constructor
        - Should take one argument `httpClient` of type `HttpClient`
        - Call `ParseAdd()` on `httpClient.DefaultRequestHeaders.UserAgent`
            - You can supply the argument "HomeEnergyApi/1.0" to `ParseAdd()
        - Should Assign the value of the passed argument to the property `httpClient`
    - Create an async method `Report()`
        - Should take one argument of type `int`
        - Should await a JSON response from an external API
            - The API you will retreive JSON from at is `https://api.zippopotam.us/us/{zipCode}` where {zipCode} is a valid 5-digit US zip code. The zip code route parameter at the end of this url should be the argument passed into `Report()`.
            - You should parse this response as the type `ZipLocationResponse`
        - Should return the first `Place` from the list of `Places` on the `ZipLocationResponse`
    - The types `ZipLocationResponse` and `Place` are pre-defined for you to help you complete this assignment. You should NOT change these definitions.

In `HomeEnergyApi/Controllers/HomeAdminController.cs`
- Create a private property on `HomeAdminController` of type `ZipCodeLocationService` amd titled `zipCodeLocationService`
    - Add an argument to `HomeAdminController`'s constructor of type `ZipCodeLocationService` and assign its value to this proprety.
- Create a new HTTP Post method `ZipLocation()`
    - This method should be accessable from the relative route `Location/{zipCode}` (full route: `Homes/admin/Location/{zipCode}`)
        - `{zipCode}` should be a route parameter on the route `Location/` of type `int`
    - This method should await a result of type `Place` from calling `zipCodeLocationService.Report()`
        - You should pass the route parameter `{zipCode}` as an argument to `zipCodeLocationService.Report()`
    - Should return an `200: OK` HTTP response code along with the `Place` result from `zipCodeLocationService.Report()`

In `HomeEnergyApi/Program.cs`
- To the `builder` add a Transient service of type `ZipLocationService`
- To the `builder` add an HTTP Client service of type `ZipLocationService`
    
## Additional Information
- You can see the response given by `https://api.zippopotam.us/us/{zipCode}` by typing the URL into your browser or by using Fetch Client. (eg. `https://api.zippopotam.us/us/10002`).
    - The response for the given example would look like...
```json
{
  "post code": "10002",
  "country": "United States",
  "country abbreviation": "US",
  "places": [
    {
      "place name": "New York City",
      "longitude": "-73.9877",
      "state": "New York",
      "state abbreviation": "NY",
      "latitude": "40.7152"
    }
  ]
}
```
- The properties on the pre-defined types `ZipLocationResponse` and `Place` contain some, but not all of the properties in the response from the given API. This allows the serialization from the JSON response to objects in our code. Although you can omit properties, you must preserve the original structure and property names from the JSON you receive for properties you do include. 
    - Properties like `post code` and `place name` are not valid variable/property names in C#, which is why we add the tag specifying the name further (eg. `[JsonPropertyName("post code")]`)
- `using` statements for the assignment have been pre-written for you, however in the future you may need to add these yourself.

## Building toward CSTA Standards:
- Decompose problems into smaller components through systematic analysis, using constructs such as procedures, modules, and/or objects. (3A-AP-17) https://www.csteachers.org/page/standards
- Justify the selection of specific control structures when tradeoffs involve implementation, readability, and program performance, and explain the benefits and drawbacks of choices made. (3A-AP-15) https://www.csteachers.org/page/standards
- Construct solutions to problems using student-created components, such as procedures, modules and/or objects. (3B-AP-14) https://www.csteachers.org/page/standards
- Compare levels of abstraction and interactions between application software, system software, and hardware layers. (3A-CS-02) https://www.csteachers.org/page/standards
- Demonstrate code reuse by creating programming solutions using libraries and APIs. (3B-AP-16) https://www.csteachers.org/page/standards
- Use and adapt classic algorithms to solve computational problems. (3B-AP-10) https://www.csteachers.org/page/standards
- Evaluate algorithms in terms of their efficiency, correctness, and clarity. (3B-AP-11) https://www.csteachers.org/page/standards

## Resources
- https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/
- https://en.wikipedia.org/wiki/Thread_(computing)
- https://learn.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/task-based-asynchronous-pattern-tap

Copyright &copy; 2025 Knight Moves. All Rights Reserved.
