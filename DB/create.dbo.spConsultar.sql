USE [BdiExamen]
GO
/****** Object:  StoredProcedure [dbo].[spConsultar]    Script Date: 2/9/2024 1:21:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[spConsultar]
	@Id VARCHAR(255) NULL,
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
			(idExamen IS NULL OR idExamen LIKE CONCAT('%', @id, '%'))
			AND (Nombre IS NULL OR Nombre LIKE CONCAT('%', @Nombre, '%'))
			AND (Descripcion IS NULL OR Descripcion LIKE CONCAT('%', @Descripcion, '%'));
	END TRY
	BEGIN CATCH
		SELECT @@ERROR AS ReturnCode, ERROR_MESSAGE() AS Description
	END CATCH
END
