# JsonExample – C# / Newtonsoft.Json Assignment

A console application that demonstrates reading, writing, and deserialising JSON data in .NET using the **Newtonsoft.Json** NuGet package, with OOP inheritance.

---

## Setup

```bash
# 1. Restore NuGet packages
dotnet restore

# 2. Build
dotnet build

# 3. Run
dotnet run
```

---

## Project Structure

```
JsonExample/
├── JsonExample.csproj       # SDK project + Newtonsoft.Json reference
├── Program.cs               # Entry point – all four tasks
├── Models/
│   └── User.cs              # Base User class + Admin + RegularUser
└── data/
    ├── users.json           # Manually created JSON array (Tasks 1–3)
    └── user_types.json      # Typed users JSON (Task 4)
```

---

## Version History / Commit Guide

| Version | Commit message | What was done |
|---------|---------------|---------------|
| v1.0    | `Task 1: manual JSON file + single user deserialise` | Created `users.json` manually. Added `User` model class. Read file with `File.ReadAllText`, deserialised with `JsonConvert.DeserializeObject<List<User>>`, printed first entry. |
| v1.1    | `Task 2: add new entries to JSON list at runtime` | Used `JArray.Parse` to load the array as a dynamic model, appended two new `JObject` entries, serialised back to file with `Formatting.Indented`. |
| v1.2    | `Task 3: deserialise ALL entries with a loop` | Re-read the updated `users.json`, deserialised into `List<User>`, iterated with a `for` loop, printed each user via `DisplayInfo()`. |
| v1.3    | `Task 4: inheritance + user_types.json` | Added `Admin` and `RegularUser` subclasses (both inherit from `User`, both override `DisplayInfo()`). Created `user_types.json`. Used a `UserType` discriminator + `switch` expression to deserialise each entry to the correct concrete type. Demonstrated polymorphism via virtual dispatch. |

---

## Task Explanations

### Task 1 – Manual JSON file + XML reader note
- **`data/users.json`** was created by hand to match the `User` class shape.
- The theory mentions an "XML reader by example" – in .NET this would use `System.Xml.XmlReader` or `XDocument`; JSON is handled here via Newtonsoft.Json's `JsonConvert`.

### Task 2 – Add new entries
`JArray` / `JObject` (from `Newtonsoft.Json.Linq`) give a dynamic, schema-free view of the JSON document. New objects are appended and the file is overwritten.

### Task 3 – Deserialise all with a loop
`JsonConvert.DeserializeObject<List<User>>(json)` maps the entire JSON array to a strongly-typed C# list. A `for` loop then iterates over every element.

### Task 4 – Inheritance + user_types.json
```
User  (base)
 ├── Admin        (+AdminLevel, +Department)
 └── RegularUser  (+Subscription, +IsVerified)
```
Each subclass overrides `DisplayInfo()` (virtual dispatch / polymorphism). The `UserType` string field acts as a discriminator so the correct concrete type is chosen during deserialisation.

---

## Key C# Concepts

| Concept | Example in this project |
|---------|------------------------|
| Auto-property | `public string Name { get; set; }` |
| Inheritance | `public class Admin : User` |
| Method override | `public override void DisplayInfo()` |
| Base call | `base.DisplayInfo()` |
| Generic list | `List<User>` |
| Pattern matching | `userType switch { "Admin" => ..., ... }` |
| String interpolation | `$"Name: {user.Name}"` |
