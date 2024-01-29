CREATE PROCEDURE [dbo].[MKAddorEdit]
	@id INT,
	@kodeMatkul VARCHAR(6),
	@namaMatkul VARCHAR(20),
	@sks INT
AS
BEGIN
	SET NOCOUNT ON;

	IF @id=0
	BEGIN
		INSERT INTO matakuliah(kode_mk,nama_mk,sks)
		VALUES(@kodeMatkul,@namaMatkul,@sks)
	END
	ELSE
	BEGIN
		UPDATE matakuliah
		SET
			kode_mk=@kodeMatkul,
			nama_mk=@namaMatkul,
			sks=@sks
		WHERE id = @id
	END
END