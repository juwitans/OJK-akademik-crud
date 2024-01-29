CREATE PROCEDURE [dbo].[MahasiswaDeleteById]
	@id INT
AS
BEGIN
	SET NOCOUNT ON;

	DELETE mahasiswa
	WHERE id=@id

END