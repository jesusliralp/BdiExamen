USE [BdiExamen]
GO

CREATE PROCEDURE spAgregar
	@ID INT,
	@Nombre VARCHAR(255),
	@Descripcion VARCHAR(255)
AS
BEGIN
	SET LANGUAGE SPANISH;
	SET NOCOUNT ON;
	BEGIN TRY
		INSERT INTO BdiExamen.dbo.tblExamen (idExamen, Nombre, Decripcion)
		VALUES (@ID, @Nombre, @Descripcion);
        SELECT 0 AS ReturnCode, "Registro insertado satisfactoriamente" AS ErrorDescription
	END TRY
	BEGIN CATCH
		SELECT @@ERROR AS ReturnCode, ERROR_MESSAGE() AS ErrorDescription
	END CATCH
END
GO
