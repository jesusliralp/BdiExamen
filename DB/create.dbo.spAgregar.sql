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
		INSERT INTO BdiExamen.dbo.tblExamen (idExamen, Nombre, Descripcion)
		VALUES (@ID, @Nombre, @Descripcion);
        SELECT 0 AS ReturnCode, 'Registro insertado satisfactoriamente' AS Description
	END TRY
	BEGIN CATCH
		SELECT @@ERROR AS ReturnCode, ERROR_MESSAGE() AS Description
	END CATCH
END
GO
