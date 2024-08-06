# contact-management-api

The contact-management-api provides all the necessary CRUD (Create, Read, Update, Delete) endpoints for managing contacts. This API is built using .NET 8.0 and serves as the backend for the Contact Management application.

## Get started

### Clone the repo

```shell
git clone https://github.com/neltonf/contact-management-api.git
cd contact-management-api
```

followed by

```sh
$ dotnet restore
```

## Build

Build any .NET Core sample using the .NET Core CLI, which is installed with [the .NET Core SDK](https://www.microsoft.com/net/download). Then run
these commands from the CLI in the directory of any sample:

```console
dotnet build
dotnet run
```

## Running the Angular Frontend with the .NET Backend
Clone and Run the [Contact Managment Angular UI](https://github.com/neltonf/contact-management-ui)
## Database

The contact records for this application are stored in a data.json file. This file serves as the primary data source for managing contact details. The data.json file is used to persist contact information in a JSON format. It is created automatically if it does not exist and is updated each time contact details are saved.