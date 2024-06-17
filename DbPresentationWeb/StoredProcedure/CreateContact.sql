CREATE PROCEDURE [dbo].[CreateContact]
	@Nom VARCHAR(100),
	@Prenom VARCHAR(100),
	@Email VARCHAR(100)
AS
	INSERT INTO 
	Contact(Nom, Prenom, Email) 
	OUTPUT inserted.Id 
	VALUES 
	(@Nom, @Prenom, @Email)
RETURN 0
