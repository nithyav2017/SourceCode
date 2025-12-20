**UsesCase**: File Uploads and Storage: Users upload images; store securely. Validate in C# with IFormFile, save to Azure Blob, 
              reference in SQL Server.

Steps to configure Azurite and Database (AdventureWorsDbContext) for local development.

- download and install `Azurite`, the local emulator for azure blob storage.
- Configure storage settings [Azurite `ConnectionString` and `ContainerName] in appsettings.json.
- Read this storage configuration in  Program.cs
- configure EF Core to use  AdventureDbContext in  program.cs 

#### Entities:

Created two entities UploadImages and BlogStorageOptions

- UploadImages for store metadata like Id, imageUrl, ModifiedDate in the `Production.ProductionPhoto` through EF Core DbContext
- BlogStorageOptions for Azurite ConnectionString and ContainerName.

#### Utility Class:

This utility class has static method to validate image MIME types and valid dimensions. This method  will called by service 
layer.

#### Service Layer:

- It validates the image file by calling utility mehtod and once the validation succussful , it creates the BLobClient object and
upload the image to azure blob by calling UploadAsync method.

- Once uploaded successfully  BlobClient returns ImageUrl ,Id and other meta data for that file for future reference.
- These metadata will be saved to `AdventureWorks2022.Production.ProductionPhoto` table through EFCore.

#### Retrieve Image:  

To retrieve image
- Fetch the metadata from database using the `imageId`
- Authenticate Storage Crendential : Use the  Azurite `AccountName`  and `AccountKey` 
- Create Authenticated BlobClient: Construct BlobClient using `ImageUrl` from database and `StorageSharedKeyCredential` built from 
  Azurite `AccountName` and `AccountKey`.
- Call `DownloadStreamingAsync()` . This streams the image data directly from blob storage
- If the downloads succeeds and the content type is valid , stream the image byte back to the browser.
 

