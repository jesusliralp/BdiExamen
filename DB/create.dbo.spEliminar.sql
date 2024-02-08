USE [BdiExamen]
GO

CREATE PROCEDURE spEliminar
	@ID INT
AS
BEGIN
	SET LANGUAGE SPANISH;
	SET NOCOUNT ON;
	BEGIN TRY
		DELETE FROM BdiExamen.dbo.tblExamen WHERE idExamen = @ID;
		SELECT 0 AS ReturnCode, 'Registro eliminado satisfactoriamente' AS ErrorDescription
	END TRY
	BEGIN CATCH
		SELECT @@ERROR AS ReturnCode, ERROR_MESSAGE() AS ErrorDescription
	END CATCH
END
GO
