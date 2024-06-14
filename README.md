# Introduction - Saal Digital Project

**Website: https://color-palette-generator.chrisdevguide.com**

**API Swagger: https://api-color-palette-generator.azurewebsites.net/swagger/index.html**

This project consists of a Color Palette Generator. By inserting a HEX color, it is possible to obtain a color palette based on the color, examples of how the generated colors would look like in some UI components and the possibility to export the generated palette as CSS variables.

## Tech Stack

- **Front-End**: Angular 17
- **Back-End**: .NET Core 8 API
- **Database**: Locally SQL Server, Deployed Postgres
- **Cloud Provider**: Azure, aiven.io for Postgres database
- **Repository and CI/CD**: GitHub

## Features

### User authentication

- **Sign Up**: Through this app, it is possible to create an account for the generated color palettes.
- **Sign In**: Through this app, it is possible to access to the created account to store and access to all the user's data.
- **Password Hashing**: On the back-end, we are storing the password through a hashing algorithm to respect the security standards.
- **JWT authentication**: On the back-end, we are implementing JWT authentication to secure our API.

**Sign Up**

![image](https://github.com/chrisdiguida/colorPaletteGeneratorApp/assets/171926333/b7313361-d8dc-4482-809b-34afd20bf965)

**Sign In**

![image](https://github.com/chrisdiguida/colorPaletteGeneratorApp/assets/171926333/a492cd3c-ac8e-4a6c-8c93-310ea974a721)

### Palette Generation

- **Palette generation**: Through the input, it is possible to generate a palette to test how it would look like. Note that on the DB, we are checking if the user already has the inserted color.
- **Saving a Palette**: Through the "Save" button, it is possible to save the generated palette color and store in the DB. Before to store the inserted color, we first check on the DB if the user already has this palette.

**Palette Generation**

![image](https://github.com/chrisdiguida/colorPaletteGeneratorApp/assets/171926333/4a4681a2-13db-4fa9-9081-dfd96eafc0b6)

**Saving a Palette**

![image](https://github.com/chrisdiguida/colorPaletteGeneratorApp/assets/171926333/95c9f8fc-e7ba-47c8-9907-1ab06dee6e5a)

### Palette Storage

- **Retrieving Stored Palettes**: On the saved tab, the app will retrieve all the stored palettes in the DB. Also, it is important to note that it is possible to filter by 'name' or 'color' and also pagination is applied (10 palettes at a time).
- **Update Palette Name**: Through the 'Update Name' button, it is possible to update the name of the selected palette.
- **Delete a Palette**: Through the 'Delete' button, it is possible to delete the selected palette.

**Retrieving Stored Palettes**
  
![image](https://github.com/chrisdiguida/colorPaletteGeneratorApp/assets/171926333/d4fd5f4d-1065-432d-93bd-aaa6347886f6)

**Update Palette Name**
![image](https://github.com/chrisdiguida/colorPaletteGeneratorApp/assets/171926333/1db785b1-7be5-474f-9a4b-759728e9ced9)

**Delete a Palette**
![image](https://github.com/chrisdiguida/colorPaletteGeneratorApp/assets/171926333/ff859e38-9341-4d81-ac63-301895771848)

### UI

- **Built-in Components**: All components have been manually created, and no external library has been used.
- **Mobile Version**: Note that it is also available a mobile version of the app.

**Mobile Version**
  
![image](https://github.com/chrisdiguida/colorPaletteGeneratorApp/assets/171926333/823c311f-a490-4b19-8f26-9ee1474cf77a)

