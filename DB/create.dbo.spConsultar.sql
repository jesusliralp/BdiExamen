USE [BdiExamen]
GO

CREATE PROCEDURE spConsultar
	@Nombre VARCHAR(255) NULL,
	@Descripcion VARCHAR(255) NULL
AS
BEGIN
	SET LANGUAGE SPANISH;
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT
			idExamen,
			Nombre,
			Descripcion
		FROM
			BdiExamen.dbo.tblExamen
		WHERE
			(Nombre IS NULL OR Nombre LIKE CONCAT('%', @Nombre, '%'))
			AND (Descripcion IS NULL OR Descripcion LIKE CONCAT('%', @Descripcion, '%'));
	END TRY
	BEGIN CATCH
		SELECT @@ERROR AS ReturnCode, ERROR_MESSAGE() AS Description
	END CATCH
END
GO
