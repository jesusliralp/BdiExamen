USE [BdiExamen]
GO

CREATE PROCEDURE spActualizar
	@ID INT,
	@Nombre VARCHAR(255),
	@Descripcion VARCHAR(255)
AS
BEGIN
	SET LANGUAGE SPANISH;
	SET NOCOUNT ON;
	BEGIN TRY
		UPDATE BdiExamen.dbo.tblExamen
		SET
			Nombre = @Nombre,
			Decripcion = @Descripcion
		WHERE
			idExamen = @ID;
		SELECT 0 AS ReturnCode, 'Registro actualizado satisfactoriamente' AS ErrorDescription
	END TRY
	BEGIN CATCH
		SELECT @@ERROR AS ReturnCode, ERROR_MESSAGE() AS ErrorDescription
	END CATCH
END
GO
