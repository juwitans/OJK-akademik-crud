﻿CREATE PROCEDURE [dbo].[MKDeleteById]
	@id INT
AS
BEGIN
	SET NOCOUNT ON;

	DELETE matakuliah
	WHERE id=@id

END